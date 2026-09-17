using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

#nullable enable

namespace SchematicHQ.Client.Leases;

/// <summary>
/// Redis-backed reservation table. Each hold is a hash, indexed by expiry in a
/// sorted set so the sweeper can pop expired entries in O(log n), and by
/// (company, credit type) so a balance display can sum a tenant's open holds.
/// All mutations use single-key operations, or single-key Lua, so the store is
/// correct on standalone and clustered Redis alike: the unspent-slice refund is
/// delegated to the lease store rather than reaching across to the lease hash
/// inside a multi-key script.
/// </summary>
public sealed class RedisReservationStore : IReservationStore
{
    private const string ReservationKeyNamespace = "credit-reservation:";

    /// <summary>
    /// Sorted set scoring open reservations by expiry so the sweeper can pop
    /// expired entries in O(log n). Members encode the full (company, credit,
    /// id) tuple, so the sweeper can clean the per-tenant index even after the
    /// reservation hash has been evicted.
    /// </summary>
    private const string ReservationIndexKey = "credit-reservations:byExpiry";

    /// <summary>
    /// Per-(company, credit) index of open reservations, a single hash of
    /// reservation id to credits reserved. The sum is read with one HGETALL
    /// (single key, Cluster-safe) instead of a per-reservation fan-out, and the
    /// hash is the source of truth for it: a field exists exactly while its
    /// reservation is open and unrefunded.
    /// </summary>
    private const string ReservationByCreditNamespace = "credit-reservations:byCredit:";

    /// <summary>
    /// Buffer past the expiry before Redis evicts the row, so the sweeper has a
    /// window to refund.
    /// </summary>
    private const long ReservationTtlGraceMs = 30_000;

    /// <summary>
    /// Atomic claim: read the reservation hash and delete it in one step,
    /// returning its fields, or nil if it was already gone. Touching a single
    /// key keeps it safe under Redis Cluster. The atomic read-then-delete is
    /// what makes a consume exactly-once: of two racing callers only one gets
    /// the fields back and proceeds to refund.
    /// </summary>
    internal const string ClaimScript =
        @"
local raw = redis.call('HGETALL', KEYS[1])
if #raw == 0 then return nil end
redis.call('DEL', KEYS[1])
return raw
";

    /// <summary>
    /// Page size for the sweeper's range read. Without a limit, a backlog of
    /// expired holds would come back as one giant reply on every pod's next
    /// tick.
    /// </summary>
    private const int SweepBatchSize = 256;

    /// <summary>
    /// Upper bound on pages per tick, so one sweep's work stays bounded and a
    /// persistently failing removal cannot loop forever. Anything left over is
    /// picked up next tick.
    /// </summary>
    private const int MaxSweepBatches = 16;

    private readonly ILeaseRedis _client;
    private readonly ILeaseStore _leaseStore;
    private readonly TimeSpan _sweepInterval;
    private readonly string _keyPrefix;
    private readonly LeaseClock _clock;
    private readonly object _gate = new();

    private CancellationTokenSource? _sweepCancellation;
    private bool _stopped;

    public RedisReservationStore(
        ILeaseRedis client,
        ILeaseStore leaseStore,
        TimeSpan? sweepInterval = null,
        string? keyPrefix = null,
        LeaseClock? clock = null
    )
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _leaseStore = leaseStore ?? throw new ArgumentNullException(nameof(leaseStore));
        _sweepInterval = sweepInterval ?? LeaseDefaults.SweepInterval;
        _keyPrefix = keyPrefix ?? LeaseDefaults.KeyPrefix;
        _clock = clock ?? (() => DateTime.UtcNow);
    }

    public async Task AddAsync(ReservationRecord reservation)
    {
        var expiresMs = LeaseTime.ToUnixMilliseconds(reservation.ExpiresAt);
        var hashKey = HashKey(reservation.Id);

        // Write the hash first so the reservation exists before anything
        // references it. These are independent single-key operations rather
        // than one multi-key script: a partial failure at worst leaves an
        // un-indexed reservation that the TTL reaps, never a double spend.
        await _client
            .HashSetAsync(
                hashKey,
                new[]
                {
                    new KeyValuePair<string, string>("id", reservation.Id),
                    new KeyValuePair<string, string>("leaseId", reservation.LeaseId),
                    new KeyValuePair<string, string>("companyId", reservation.CompanyId),
                    new KeyValuePair<string, string>("creditTypeId", reservation.CreditTypeId),
                    new KeyValuePair<string, string>("eventSubtype", reservation.EventSubtype),
                    new KeyValuePair<string, string>(
                        "quantityReserved",
                        RedisLeaseStore.Text(reservation.QuantityReserved)
                    ),
                    new KeyValuePair<string, string>(
                        "creditsReserved",
                        RedisLeaseStore.Text(reservation.CreditsReserved)
                    ),
                    new KeyValuePair<string, string>(
                        "consumptionRate",
                        RedisLeaseStore.Text(reservation.ConsumptionRate)
                    ),
                    new KeyValuePair<string, string>("expiresAt", RedisLeaseStore.Text(expiresMs)),
                    new KeyValuePair<string, string>("evalCtx", EncodeEvalCtx(reservation)),
                }
            )
            .ConfigureAwait(false);

        // The TTL and the two indexes only depend on the hash existing, not on
        // each other, so they go out together: one round-trip wave instead of
        // three. This sits on every allowed check, so the latency matters.
        await Task.WhenAll(
                _client.KeyExpireAtAsync(hashKey, expiresMs + ReservationTtlGraceMs),
                _client.SortedSetAddAsync(IndexKey(), EncodeMember(reservation), expiresMs),
                _client.HashSetAsync(
                    ByCreditKey(reservation.CompanyId, reservation.CreditTypeId),
                    new[]
                    {
                        new KeyValuePair<string, string>(
                            reservation.Id,
                            RedisLeaseStore.Text(reservation.CreditsReserved)
                        ),
                    }
                )
            )
            .ConfigureAwait(false);
    }

    public async Task<ReservationRecord?> GetAsync(string id)
    {
        var raw = await _client.HashGetAllAsync(HashKey(id)).ConfigureAwait(false);
        if (raw == null || !raw.ContainsKey("id"))
        {
            return null;
        }
        return Decode(raw);
    }

    /// <summary>
    /// Sums open holds for a slot with a single HGETALL on the per-tenant index
    /// hash: one round trip, one key, no per-reservation fan-out.
    /// </summary>
    public async Task<double> ReservedCreditsAsync(string companyId, string creditTypeId)
    {
        IReadOnlyDictionary<string, string> byCredit;
        try
        {
            byCredit = await _client.HashGetAllAsync(ByCreditKey(companyId, creditTypeId))
                .ConfigureAwait(false);
        }
        catch
        {
            return 0;
        }

        double total = 0;
        foreach (var value in byCredit.Values)
        {
            total += RedisLeaseStore.ParseNumber(value);
        }
        return total;
    }

    public async Task<double?> ConsumeAsync(string id, double creditsConsumed)
    {
        // Atomically claim (read and delete) the reservation hash. Only one
        // caller wins; a duplicate or racing consume gets nil.
        var claimed = await _client
            .EvalAsync(ClaimScript, new[] { HashKey(id) }, Array.Empty<string>())
            .ConfigureAwait(false);

        var raw = DecodeFlatArray(claimed);
        if (raw == null || !raw.ContainsKey("id"))
        {
            return null;
        }

        var companyId = Field(raw, "companyId");
        var creditTypeId = Field(raw, "creditTypeId");
        var reserved = RedisLeaseStore.ParseNumber(Field(raw, "creditsReserved"));

        // Index cleanup, both single-key. The credits leave the per-tenant hash
        // BEFORE the refund below so the lease (local remaining plus this hash)
        // never transiently double-counts the slice.
        await Swallow(
                _client.SortedSetRemoveAsync(
                    IndexKey(),
                    EncodeMember(companyId, creditTypeId, id)
                )
            )
            .ConfigureAwait(false);
        await Swallow(_client.HashDeleteAsync(ByCreditKey(companyId, creditTypeId), id))
            .ConfigureAwait(false);

        var consumed = creditsConsumed;
        if (consumed < 0)
        {
            consumed = 0;
        }
        if (consumed > reserved)
        {
            consumed = reserved;
        }
        var refund = reserved - consumed;
        if (refund > 0)
        {
            // Delegate the clamped refund to the lease store, which owns the
            // lease hash, keeping the cross-key write out of a Lua script.
            // Pinned to the reservation's lease id so a hold carved out of an
            // expired lease cannot inflate a successor lease's balance.
            await _leaseStore
                .RefundAsync(companyId, creditTypeId, refund, Field(raw, "leaseId"))
                .ConfigureAwait(false);
        }
        return consumed;
    }

    public async Task<int> SweepExpiredAsync(DateTime? now = null)
    {
        var cutoff = LeaseTime.ToUnixMilliseconds(now ?? _clock());
        var swept = 0;

        // Page through expired members rather than fetching them all at once.
        // Each processed member is removed below, so re-reading from offset 0
        // advances through the backlog.
        for (var batch = 0; batch < MaxSweepBatches; batch++)
        {
            var expired = await _client
                .SortedSetRangeByScoreAsync(IndexKey(), 0, cutoff, 0, SweepBatchSize)
                .ConfigureAwait(false);
            if (expired.Count == 0)
            {
                break;
            }

            foreach (var member in expired)
            {
                var decoded = DecodeMember(member);
                if (decoded == null)
                {
                    // Nothing but Add writes these, so this is belt and braces:
                    // drop the member so it cannot wedge the sweeper.
                    await Swallow(_client.SortedSetRemoveAsync(IndexKey(), member))
                        .ConfigureAwait(false);
                    continue;
                }

                var refunded = await ConsumeAsync(decoded.Value.Id, 0).ConfigureAwait(false);
                // Always drop the member we read. On the success path the
                // consume already removed it, so this is an idempotent no-op;
                // it also covers the evicted-hash path below.
                await Swallow(_client.SortedSetRemoveAsync(IndexKey(), member))
                    .ConfigureAwait(false);
                if (refunded != null)
                {
                    swept++;
                    continue;
                }

                // The consume found no reservation hash: either a racing track
                // already consumed it (and reconciled the byCredit field, so
                // the delete below is a no-op), or the hash was evicted before
                // the sweeper reached it, orphaning the field. Reconcile it so
                // the reserved-credits sum cannot keep counting an evicted
                // hold. The unspent slice is deliberately NOT refunded here:
                // without the hash, the claim cannot arbitrate exactly-once
                // across racing sweepers, so the slice is reclaimed when the
                // lease itself expires server-side instead.
                await Swallow(
                        _client.HashDeleteAsync(
                            ByCreditKey(decoded.Value.CompanyId, decoded.Value.CreditTypeId),
                            decoded.Value.Id
                        )
                    )
                    .ConfigureAwait(false);
            }

            if (expired.Count < SweepBatchSize)
            {
                break;
            }
        }

        return swept;
    }

    public void StartSweep()
    {
        lock (_gate)
        {
            if (_sweepCancellation != null || _stopped)
            {
                return;
            }
            _sweepCancellation = new CancellationTokenSource();
        }

        var token = _sweepCancellation.Token;
        _ = Task.Run(
            async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        await Task.Delay(_sweepInterval, token).ConfigureAwait(false);
                        await SweepExpiredAsync().ConfigureAwait(false);
                    }
                    catch (OperationCanceledException)
                    {
                        return;
                    }
                    catch
                    {
                        // Swallow so one bad sweep does not end the loop.
                    }
                }
            },
            token
        );
    }

    public void Stop()
    {
        CancellationTokenSource? cancellation;
        lock (_gate)
        {
            _stopped = true;
            cancellation = _sweepCancellation;
            _sweepCancellation = null;
        }
        cancellation?.Cancel();
        cancellation?.Dispose();
    }

    public async Task<long> CountAsync()
    {
        try
        {
            return await _client.SortedSetLengthAsync(IndexKey()).ConfigureAwait(false);
        }
        catch
        {
            return 0;
        }
    }

    public void Dispose() => Stop();

    private string HashKey(string id) => _keyPrefix + ReservationKeyNamespace + id;

    private string IndexKey() => _keyPrefix + ReservationIndexKey;

    private string ByCreditKey(string companyId, string creditTypeId) =>
        _keyPrefix + ReservationByCreditNamespace + LeaseKeys.Slot(companyId, creditTypeId);

    // Members encode the (company, credit, id) tuple so the sweeper can clean
    // the per-tenant byCredit hash even after the reservation hash has been
    // evicted, at which point the claim returns nil and cannot report the
    // company or credit; otherwise the orphaned field would permanently inflate
    // the reserved-credits sum. The delimiter is absent from Schematic ids and
    // from the reservation's own id.
    private static string EncodeMember(ReservationRecord reservation) =>
        EncodeMember(reservation.CompanyId, reservation.CreditTypeId, reservation.Id);

    private static string EncodeMember(string companyId, string creditTypeId, string id) =>
        companyId + "|" + creditTypeId + "|" + id;

    private static (string CompanyId, string CreditTypeId, string Id)? DecodeMember(string member)
    {
        var parts = member.Split('|');
        if (parts.Length != 3)
        {
            return null;
        }
        return (parts[0], parts[1], parts[2]);
    }

    private static string EncodeEvalCtx(ReservationRecord reservation) =>
        JsonSerializer.Serialize(
            new Dictionary<string, Dictionary<string, string>?>
            {
                ["company"] = reservation.Company,
                ["user"] = reservation.User,
            }
        );

    private static ReservationRecord Decode(IReadOnlyDictionary<string, string> raw)
    {
        var record = new ReservationRecord
        {
            Id = Field(raw, "id"),
            LeaseId = Field(raw, "leaseId"),
            CompanyId = Field(raw, "companyId"),
            CreditTypeId = Field(raw, "creditTypeId"),
            EventSubtype = Field(raw, "eventSubtype"),
            QuantityReserved = RedisLeaseStore.ParseNumber(Field(raw, "quantityReserved")),
            CreditsReserved = RedisLeaseStore.ParseNumber(Field(raw, "creditsReserved")),
            ConsumptionRate = RedisLeaseStore.ParseNumber(Field(raw, "consumptionRate")),
            ExpiresAt = LeaseTime.FromUnixMilliseconds(
                (long)RedisLeaseStore.ParseNumber(Field(raw, "expiresAt"))
            ),
        };

        var evalCtx = Field(raw, "evalCtx");
        if (evalCtx.Length > 0)
        {
            try
            {
                var decoded = JsonSerializer.Deserialize<
                    Dictionary<string, Dictionary<string, string>?>
                >(evalCtx);
                if (decoded != null)
                {
                    record.Company = decoded.TryGetValue("company", out var company)
                        ? company
                        : null;
                    record.User = decoded.TryGetValue("user", out var user) ? user : null;
                }
            }
            catch (JsonException)
            {
                // A hold whose context cannot be decoded still settles: the
                // track event simply carries no company or user keys.
            }
        }

        return record;
    }

    /// <summary>
    /// Decodes a flat field, value, field, value array, the shape the claim
    /// script returns.
    /// </summary>
    private static Dictionary<string, string>? DecodeFlatArray(object? raw)
    {
        if (raw is not string[] flat || flat.Length == 0)
        {
            return null;
        }
        var result = new Dictionary<string, string>();
        for (var i = 0; i + 1 < flat.Length; i += 2)
        {
            result[flat[i]] = flat[i + 1];
        }
        return result;
    }

    private static string Field(IReadOnlyDictionary<string, string> raw, string name) =>
        raw.TryGetValue(name, out var value) ? value : string.Empty;

    private static async Task Swallow(Task task)
    {
        try
        {
            await task.ConfigureAwait(false);
        }
        catch
        {
            // Index bookkeeping is best effort: a failure here leaves a stale
            // index entry the next sweep reconciles, never a lost refund.
        }
    }
}
