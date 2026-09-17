using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

#nullable enable

namespace SchematicHQ.Client.Leases;

/// <summary>
/// Redis-backed lease store. One hash per (company, credit type) slot, with
/// atomic mutations via single-key Lua scripts so it stays correct on both
/// standalone and clustered Redis.
///
/// <para>The key layout, hash fields and script bodies are identical to the
/// Node, Go and Python SDKs', which is what lets a mixed-language fleet share
/// one lease.</para>
/// </summary>
public sealed class RedisLeaseStore : ILeaseStore
{
    private const string LeaseKeyNamespace = "credit-lease:";

    /// <summary>
    /// How long after the declared expiry the row survives before Redis evicts
    /// it. It gives the sweeper a window to refund expired reservations before
    /// the lease state underneath them disappears.
    /// </summary>
    private const long LeaseTtlGraceMs = 60_000;

    // Every Lua script below touches exactly ONE key (the lease hash), keeping
    // them safe under Redis Cluster, where a multi-key script spanning slots
    // raises CROSSSLOT. Only the lease hash needs atomic mutation; cross-key
    // bookkeeping uses ordinary single-key commands.
    //
    // Expiry is decided against the Redis server's clock (redis.call('TIME')),
    // not the calling pod's: with many pods sharing one lease, local clock skew
    // would let pods disagree on whether the lease is live. The snippet below
    // converts TIME to integer milliseconds (matching the stored expiresAt);
    // replicate_commands() comes first so the non-deterministic TIME read is
    // allowed alongside writes on Redis 5 and 6.
    private const string LeaseNowMs =
        @"
redis.replicate_commands()
local t = redis.call('TIME')
local now = (tonumber(t[1]) * 1000) + math.floor(tonumber(t[2]) / 1000)
";

    /// <summary>
    /// Atomic replace. Writes the lease hash only when the slot is empty or the
    /// existing lease has expired. Returns 1 on write, 0 if a live lease
    /// already occupies the slot, even one with a different leaseId, for
    /// instance one installed by a sibling instance that raced this acquire.
    /// Keeping the existing live lease preserves its already-debited balance.
    /// An expired row with the SAME leaseId is reconciled like an extend
    /// instead of rewritten, which would reset the balance and erase debits
    /// whose reservations are still open.
    /// </summary>
    private const string ReplaceScript =
        LeaseNowMs
        + @"
local existing_id = redis.call('HGET', KEYS[1], 'leaseId')
local existing_expiry = tonumber(redis.call('HGET', KEYS[1], 'expiresAt') or '0')
local new_id = ARGV[1]
local new_granted = ARGV[2]
local new_expiry = tonumber(ARGV[3])
local grace = tonumber(ARGV[4])

if existing_id and existing_expiry > now then
    return 0
end

if existing_id == new_id then
    local granted = tonumber(redis.call('HGET', KEYS[1], 'grantedAmount') or '0')
    local add = tonumber(new_granted) - granted
    if add > 0 then
        local remaining = tonumber(redis.call('HGET', KEYS[1], 'localRemainingCredits') or '0')
        redis.call('HSET', KEYS[1],
            'grantedAmount', new_granted,
            'localRemainingCredits', tostring(remaining + add))
    end
    if new_expiry > existing_expiry then
        redis.call('HSET', KEYS[1], 'expiresAt', ARGV[3])
        redis.call('PEXPIREAT', KEYS[1], new_expiry + grace)
    end
    return 0
end

redis.call('DEL', KEYS[1])
redis.call('HSET', KEYS[1],
    'leaseId', new_id,
    'companyId', ARGV[5],
    'creditTypeId', ARGV[6],
    'grantedAmount', new_granted,
    'localRemainingCredits', new_granted,
    'expiresAt', ARGV[3])
redis.call('PEXPIREAT', KEYS[1], new_expiry + grace)
return 1
";

    /// <summary>
    /// Atomic check-and-decrement on localRemainingCredits. Returns the
    /// post-debit balance as a string (a Lua number reply truncates to integer,
    /// which would corrupt fractional credit costs) alongside the leaseId it
    /// came out of; nil if there is no lease, the lease has expired, or there
    /// is insufficient remaining. Returning the lease id read inside the same
    /// script is what lets the caller pin its reservation to the lease the
    /// debit actually landed on.
    /// </summary>
    private const string TryReserveScript =
        LeaseNowMs
        + @"
local raw = redis.call('HGET', KEYS[1], 'localRemainingCredits')
if not raw then return false end
local lease_id = redis.call('HGET', KEYS[1], 'leaseId')
if not lease_id then return false end
local expiry = tonumber(redis.call('HGET', KEYS[1], 'expiresAt') or '0')
if expiry <= now then return false end
local remaining = tonumber(raw)
local requested = tonumber(ARGV[1])
if remaining < requested then return false end
local new_remaining = remaining - requested
redis.call('HSET', KEYS[1], 'localRemainingCredits', tostring(new_remaining))
return { tostring(new_remaining), lease_id }
";

    /// <summary>
    /// Refund credits, clamped at grantedAmount. ARGV[2], when non-empty, pins
    /// the refund to a specific leaseId: if the slot now holds a different
    /// lease, the refund is dropped, because the expired lease's unspent
    /// remainder was already returned to the company balance server-side, so
    /// crediting the successor would mint phantom credits.
    /// </summary>
    private const string RefundScript =
        @"
local raw_remaining = redis.call('HGET', KEYS[1], 'localRemainingCredits')
if not raw_remaining then return 0 end
local required_lease = ARGV[2]
if required_lease and required_lease ~= '' then
    local current_lease = redis.call('HGET', KEYS[1], 'leaseId')
    if current_lease ~= required_lease then return 0 end
end
local remaining = tonumber(raw_remaining)
local granted = tonumber(redis.call('HGET', KEYS[1], 'grantedAmount') or '0')
local refund = tonumber(ARGV[1])
local new_balance = remaining + refund
if new_balance > granted then new_balance = granted end
redis.call('HSET', KEYS[1], 'localRemainingCredits', tostring(new_balance))
return 1
";

    /// <summary>
    /// Reconcile the lease to the server-authoritative grantedAmount total
    /// (ARGV[1]), crediting the difference to localRemainingCredits. The delta
    /// is computed here, atomically against the hash's current total, never by
    /// the caller from a pre-wire-call read: per-process single-flight does not
    /// cover sibling pods, so two pods extending the same shared lease
    /// concurrently would each apply a delta against the same stale read and
    /// mint phantom credits. Expiry only ever moves forward. ARGV[4], when
    /// non-empty, pins the extend to a specific leaseId.
    /// </summary>
    private const string ExtendScript =
        @"
local raw_granted = redis.call('HGET', KEYS[1], 'grantedAmount')
if not raw_granted then return 0 end
local required_lease = ARGV[4]
if required_lease and required_lease ~= '' then
    local current_lease = redis.call('HGET', KEYS[1], 'leaseId')
    if current_lease ~= required_lease then return 0 end
end
local granted = tonumber(raw_granted)
local target = tonumber(ARGV[1])
local add = target - granted
if add > 0 then
    local remaining = tonumber(redis.call('HGET', KEYS[1], 'localRemainingCredits') or '0')
    redis.call('HSET', KEYS[1],
        'grantedAmount', tostring(target),
        'localRemainingCredits', tostring(remaining + add))
end
local new_expiry = tonumber(ARGV[2])
local grace = tonumber(ARGV[3])
local current_expiry = tonumber(redis.call('HGET', KEYS[1], 'expiresAt') or '0')
if new_expiry > current_expiry then
    redis.call('HSET', KEYS[1], 'expiresAt', ARGV[2])
    redis.call('PEXPIREAT', KEYS[1], new_expiry + grace)
end
return 1
";

    private readonly ILeaseRedis _client;
    private readonly string _keyPrefix;
    private readonly TimeSpan _defaultLeaseDuration;
    private readonly LeaseClock _clock;

    public RedisLeaseStore(
        ILeaseRedis client,
        string? keyPrefix = null,
        TimeSpan? defaultLeaseDuration = null,
        LeaseClock? clock = null
    )
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _keyPrefix = keyPrefix ?? LeaseDefaults.KeyPrefix;
        _defaultLeaseDuration = defaultLeaseDuration ?? LeaseDefaults.LeaseDuration;
        _clock = clock ?? (() => DateTime.UtcNow);
    }

    /// <summary>
    /// Public so the reservation store can target the same lease hash for
    /// refunds.
    /// </summary>
    public string HashKey(string companyId, string creditTypeId) =>
        _keyPrefix + LeaseKeyNamespace + LeaseKeys.Slot(companyId, creditTypeId);

    public async Task<LeaseState?> GetAsync(string companyId, string creditTypeId)
    {
        var raw = await _client.HashGetAllAsync(HashKey(companyId, creditTypeId))
            .ConfigureAwait(false);
        if (raw == null || !raw.TryGetValue("leaseId", out var leaseId) || leaseId.Length == 0)
        {
            return null;
        }

        return new LeaseState
        {
            LeaseId = leaseId,
            CompanyId = Field(raw, "companyId"),
            CreditTypeId = Field(raw, "creditTypeId"),
            GrantedAmount = Number(raw, "grantedAmount"),
            LocalRemainingCredits = Number(raw, "localRemainingCredits"),
            ExpiresAt = LeaseTime.FromUnixMilliseconds((long)Number(raw, "expiresAt")),
        };
    }

    public async Task<bool> ReplaceAsync(LeaseGrant grant)
    {
        var result = await _client
            .EvalAsync(
                ReplaceScript,
                new[] { HashKey(grant.CompanyId, grant.CreditTypeId) },
                // No client clock here: the script reads now from the Redis
                // server via TIME so all pods agree on expiry.
                new[]
                {
                    grant.LeaseId,
                    Text(grant.GrantedAmount),
                    Text(LeaseTime.ToUnixMilliseconds(grant.ExpiresAt)),
                    Text(LeaseTtlGraceMs),
                    grant.CompanyId,
                    grant.CreditTypeId,
                }
            )
            .ConfigureAwait(false);

        return result is long written && written == 1;
    }

    public async Task<ReserveResult?> TryReserveAsync(
        string companyId,
        string creditTypeId,
        double credits
    )
    {
        // Reject non-finite and negative debits before they reach the script: a
        // NaN rendered into the argument parses back to nan in Lua, slips
        // through the comparison, and would poison the shared balance for every
        // pod.
        if (!LeaseQuantity.IsValid(credits))
        {
            return null;
        }

        var result = await _client
            .EvalAsync(
                TryReserveScript,
                new[] { HashKey(companyId, creditTypeId) },
                // Only the requested amount: now comes from the Redis server clock.
                new[] { Text(credits) }
            )
            .ConfigureAwait(false);

        // A nil reply (could not reserve) surfaces as null; success is a
        // two-element multi-bulk of the post-debit balance and the charged
        // leaseId, both as strings.
        if (result is not string[] reply || reply.Length < 2)
        {
            return null;
        }
        return new ReserveResult(ParseNumber(reply[0]), reply[1]);
    }

    public async Task RefundAsync(
        string companyId,
        string creditTypeId,
        double credits,
        string? pinLeaseId = null
    )
    {
        if (credits <= 0)
        {
            return;
        }

        await _client
            .EvalAsync(
                RefundScript,
                new[] { HashKey(companyId, creditTypeId) },
                // An empty string disables the lease pin: Lua has no nil ARGV.
                new[] { Text(credits), pinLeaseId ?? string.Empty }
            )
            .ConfigureAwait(false);
    }

    public async Task ExtendAsync(
        string companyId,
        string creditTypeId,
        double grantedTotal,
        DateTime? newExpiresAt = null,
        string? pinLeaseId = null
    )
    {
        var expiry = newExpiresAt ?? _clock().Add(_defaultLeaseDuration);
        await _client
            .EvalAsync(
                ExtendScript,
                new[] { HashKey(companyId, creditTypeId) },
                // grantedTotal is the server-authoritative TOTAL; the script
                // computes the credit delta atomically against the stored total.
                new[]
                {
                    Text(grantedTotal),
                    Text(LeaseTime.ToUnixMilliseconds(expiry)),
                    Text(LeaseTtlGraceMs),
                    pinLeaseId ?? string.Empty,
                }
            )
            .ConfigureAwait(false);
    }

    public Task DropAsync(string companyId, string creditTypeId) =>
        // A plain single-key delete: there is no secondary index to keep in sync.
        _client.KeyDeleteAsync(HashKey(companyId, creditTypeId));

    private static string Field(IReadOnlyDictionary<string, string> raw, string name) =>
        raw.TryGetValue(name, out var value) ? value : string.Empty;

    private static double Number(IReadOnlyDictionary<string, string> raw, string name) =>
        raw.TryGetValue(name, out var value) ? ParseNumber(value) : 0;

    internal static double ParseNumber(string? value) =>
        double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var parsed)
            ? parsed
            : 0;

    /// <summary>
    /// Renders a number for Lua. Invariant culture on purpose: a comma decimal
    /// separator would reach tonumber() as nil and silently zero a balance.
    /// </summary>
    internal static string Text(double value) =>
        value.ToString("R", CultureInfo.InvariantCulture);

    internal static string Text(long value) => value.ToString(CultureInfo.InvariantCulture);
}
