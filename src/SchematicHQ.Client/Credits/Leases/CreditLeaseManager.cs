using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

#nullable enable

namespace SchematicHQ.Client.Leases;

/// <summary>
/// Owns lease rows for one client: acquire on first use or after expiry, extend
/// when the local view dips below the water mark, release on close.
///
/// <para>Acquire and extend each get their own best-effort single-flight map
/// keyed by slot. Best-effort because callers racing ahead of the registration
/// can still issue duplicate wire calls, which is safe: the server is
/// idempotent for an active slot, replace keeps the first live lease, and
/// extend reconciles to a total.</para>
///
/// <para>Every path here resolves rather than throws: callers route a missing
/// lease through their fail-open or fail-closed handling, and several calls are
/// made fire-and-forget, where an exception has nowhere to go.</para>
/// </summary>
public sealed class CreditLeaseManager
{
    private readonly ILeaseWireClient _wire;
    private readonly ILeaseStore _leaseStore;
    private readonly ILogger _logger;
    private readonly CreditLeaseConfig _config;
    private readonly LeaseClock _clock;

    private readonly object _gate = new();

    // Kept separate so an in-flight extend can never satisfy an acquire, or the
    // other way round.
    private readonly Dictionary<string, Task<LeaseState?>> _inflightAcquire = new();
    private readonly Dictionary<string, ExtendFlight> _inflightExtend = new();

    // Lease work nobody awaits: the redundant release a lost acquire race
    // issues, and the background extends callers fire and forget. Drain waits
    // these out so a close releases what they installed.
    private readonly HashSet<Task> _background = new();

    private bool _stopped;

    public CreditLeaseManager(
        ILeaseWireClient wire,
        ILeaseStore leaseStore,
        CreditLeaseConfig config,
        ILogger logger,
        LeaseClock? clock = null
    )
    {
        _wire = wire ?? throw new ArgumentNullException(nameof(wire));
        _leaseStore = leaseStore ?? throw new ArgumentNullException(nameof(leaseStore));
        _config = config ?? throw new ArgumentNullException(nameof(config));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _clock = clock ?? (() => DateTime.UtcNow);
    }

    public ResolvedLeaseConfig ResolveConfig(string creditTypeId) => _config.Resolve(creditTypeId);

    /// <summary>
    /// Returns the current lease, acquiring one (or replacing an expired one)
    /// if none is live. Best-effort single-flight: callers arriving after a
    /// request is registered share it, and the first caller's request options
    /// win. Never throws: a store or wire failure is logged and reported as
    /// null, so callers route it through their fail-open or fail-closed
    /// handling.
    /// </summary>
    public async Task<LeaseState?> AcquireIfNeededAsync(
        string companyId,
        string creditTypeId,
        RequestOptions? options = null
    )
    {
        if (_stopped)
        {
            // Past Stop the drain has run or is running, so a lease acquired
            // now is one nothing is left to release.
            _logger.LogDebug(
                "Lease manager is stopped; skipping acquire for {CompanyId}/{CreditTypeId}",
                companyId,
                creditTypeId
            );
            return null;
        }

        LeaseState? existing;
        try
        {
            existing = await _leaseStore.GetAsync(companyId, creditTypeId).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to read lease store for {CompanyId}/{CreditTypeId}",
                companyId,
                creditTypeId
            );
            return null;
        }

        if (existing != null && existing.ExpiresAt > _clock())
        {
            return existing;
        }

        // An expired or absent slot is left for Replace to overwrite: it guards
        // on expiry and does the delete and write atomically. Dropping the
        // stale entry here first would be a separate, non-atomic operation that
        // can interleave between a sibling pod's read and its replace,
        // clobbering a lease that pod just installed. Reading a stale entry in
        // the gap is harmless, since every path that acts on a lease re-guards
        // on expiry.

        // Check again: Stop may have landed during the store read, and a drain
        // that ran in that gap saw nothing in flight.
        if (_stopped)
        {
            _logger.LogDebug(
                "Lease manager is stopped; skipping acquire for {CompanyId}/{CreditTypeId}",
                companyId,
                creditTypeId
            );
            return null;
        }

        var key = LeaseKeys.Slot(companyId, creditTypeId);
        Task<LeaseState?>? joined = null;
        Task<LeaseState?>? started = null;
        lock (_gate)
        {
            if (_inflightAcquire.TryGetValue(key, out var inflight))
            {
                joined = inflight;
            }
            else
            {
                started = AcquireAsync(companyId, creditTypeId, options);
                _inflightAcquire[key] = started;
            }
        }

        if (joined != null)
        {
            return await joined.ConfigureAwait(false);
        }

        try
        {
            return await started!.ConfigureAwait(false);
        }
        finally
        {
            lock (_gate)
            {
                if (_inflightAcquire.TryGetValue(key, out var current) && current == started)
                {
                    _inflightAcquire.Remove(key);
                }
            }
        }
    }

    private async Task<LeaseState?> AcquireAsync(
        string companyId,
        string creditTypeId,
        RequestOptions? options
    )
    {
        var resolved = ResolveConfig(creditTypeId);
        try
        {
            var grant = await _wire
                .AcquireAsync(
                    companyId,
                    creditTypeId,
                    resolved.LeaseSize,
                    _clock().Add(resolved.LeaseDuration),
                    options
                )
                .ConfigureAwait(false);

            var wrote = await _leaseStore.ReplaceAsync(grant).ConfigureAwait(false);
            if (!wrote)
            {
                // Another instance sharing the backend installed a live lease
                // for this slot first, and Replace kept theirs to preserve its
                // already-debited balance. The server is idempotent for an
                // active slot, so a racing acquire is normally handed back the
                // SAME lease the sibling installed, in which case there is
                // nothing to release: releasing would mark the shared lease
                // released server-side and refund its remainder while every pod
                // keeps reserving against it locally. Only when the server
                // minted a different lease is ours a redundant hold nobody will
                // draw on, so release that one rather than leave it orphaned
                // against the company's balance until its server-side expiry.
                var current = await _leaseStore
                    .GetAsync(companyId, creditTypeId)
                    .ConfigureAwait(false);
                if (current != null && current.LeaseId != grant.LeaseId)
                {
                    _logger.LogDebug(
                        "Lost acquire race for {CompanyId}/{CreditTypeId}; releasing redundant lease {LeaseId}",
                        companyId,
                        creditTypeId,
                        grant.LeaseId
                    );
                    Track(ReleaseQuietlyAsync(grant.LeaseId));
                }
                else
                {
                    _logger.LogDebug(
                        "Lost acquire race for {CompanyId}/{CreditTypeId}; server returned the installed lease {LeaseId}, nothing to release",
                        companyId,
                        creditTypeId,
                        grant.LeaseId
                    );
                }
                return current;
            }

            _logger.LogDebug(
                "Acquired credit lease {LeaseId} for {CompanyId}/{CreditTypeId} (granted={Granted}, expires={ExpiresAt})",
                grant.LeaseId,
                companyId,
                creditTypeId,
                grant.GrantedAmount,
                grant.ExpiresAt
            );
            return await _leaseStore.GetAsync(companyId, creditTypeId).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to acquire credit lease for {CompanyId}/{CreditTypeId}",
                companyId,
                creditTypeId
            );
            return null;
        }
    }

    /// <summary>
    /// Kicks off a background extend when one is warranted, which is when
    /// either the local remaining is at or below the low-water-mark ratio
    /// (steady-state refresh), or the caller passes
    /// <paramref name="requiredCredits"/> and the local remaining is below that
    /// figure (a check just failed a reserve of that size).
    ///
    /// <para>A caller arriving while an extend is in flight joins it. If its own
    /// shortfall is larger than what that extend asked for, it waits the flight
    /// out and then issues exactly one follow-up extend for the remaining
    /// difference; otherwise it would inherit a tranche-sized ask and fail its
    /// post-extend retry with credits still sitting on the server.</para>
    ///
    /// <para>Returns the in-flight task so callers can await it or fire and
    /// forget. It never throws.</para>
    /// </summary>
    public Task<LeaseState?> MaybeExtendInBackgroundAsync(
        string companyId,
        string creditTypeId,
        double? requiredCredits = null,
        RequestOptions? options = null
    )
    {
        if (_stopped)
        {
            // Extending past Stop re-holds credits on a lease the close is
            // about to release, or has already released.
            _logger.LogDebug(
                "Lease manager is stopped; skipping extend for {CompanyId}/{CreditTypeId}",
                companyId,
                creditTypeId
            );
            return Task.FromResult<LeaseState?>(null);
        }

        // Tracked whole, not just the wire call inside it: callers drop this
        // task, so between the store read and the extend there would otherwise
        // be a window where a drain sees nothing pending.
        return Track(ExtendIfNeededAsync(companyId, creditTypeId, requiredCredits, options, true));
    }

    private async Task<LeaseState?> ExtendIfNeededAsync(
        string companyId,
        string creditTypeId,
        double? requiredCredits,
        RequestOptions? options,
        bool allowFollowUp
    )
    {
        LeaseState? entry;
        try
        {
            entry = await _leaseStore.GetAsync(companyId, creditTypeId).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(
                ex,
                "Failed to read lease store for {CompanyId}/{CreditTypeId}",
                companyId,
                creditTypeId
            );
            return null;
        }

        if (entry == null)
        {
            return null;
        }
        // Never extend an expired lease: the server treats it as released, with
        // its remainder already refunded to the company balance, so the right
        // move is the fresh acquire the next check performs.
        if (entry.ExpiresAt <= _clock())
        {
            return null;
        }

        var resolved = ResolveConfig(creditTypeId);
        var ratio = entry.LocalRemainingCredits / Math.Max(entry.GrantedAmount, 1);
        var belowWatermark = ratio <= resolved.LowWaterMark;
        var belowRequired =
            requiredCredits.HasValue && entry.LocalRemainingCredits < requiredCredits.Value;
        if (!belowWatermark && !belowRequired)
        {
            return entry;
        }

        // Size the extend to cover the request that triggered it: a single
        // check needing more than the remaining plus a tranche would otherwise
        // fail its post-extend retry forever, even with ample server balance.
        // The watermark-driven steady-state path keeps requesting the
        // configured tranche. Sized here, one level above the wire call, so the
        // flight registered below and the request body provably carry the same
        // number for a joiner to compare against.
        var shortfall = requiredCredits.HasValue
            ? requiredCredits.Value - entry.LocalRemainingCredits
            : 0;
        var additionalAmount = Math.Max(resolved.LeaseSize, shortfall);

        var key = LeaseKeys.Slot(companyId, creditTypeId);
        ExtendFlight? inflight;
        lock (_gate)
        {
            _inflightExtend.TryGetValue(key, out inflight);
        }

        if (inflight != null)
        {
            var joined = await inflight.Task.ConfigureAwait(false);
            // The flight already asked for at least what we need: every
            // watermark-driven joiner, and any check the tranche covers. One
            // wire call serves all of them, which is the point of single-flight.
            if (additionalAmount <= inflight.RequestedAdditional || !allowFollowUp)
            {
                return joined;
            }
            // Our shortfall outran the flight's ask. We waited it out rather
            // than racing a second extend onto the same lease; now top up the
            // difference with exactly one more, re-reading the slot the flight
            // just moved. Disallowing a further follow-up keeps this from
            // chaining: when the server cannot cover the request, a chain would
            // spin.
            return await ExtendIfNeededAsync(
                    companyId,
                    creditTypeId,
                    requiredCredits,
                    options,
                    false
                )
                .ConfigureAwait(false);
        }

        return await StartExtendAsync(key, entry, resolved, additionalAmount, options)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Registers and runs an extend as the slot's in-flight one. The cleanup is
    /// identity-guarded rather than an unconditional removal: a joiner whose
    /// shortfall outran this flight registers a follow-up for the same key, and
    /// this flight's cleanup must not evict it.
    /// </summary>
    private Task<LeaseState?> StartExtendAsync(
        string key,
        LeaseState entry,
        ResolvedLeaseConfig resolved,
        double additionalAmount,
        RequestOptions? options
    )
    {
        Task<LeaseState?> task = null!;
        ExtendFlight flight = null!;
        lock (_gate)
        {
            if (_inflightExtend.TryGetValue(key, out var existing))
            {
                return existing.Task;
            }
            task = ExtendAsync(entry, resolved, additionalAmount, options);
            flight = new ExtendFlight(additionalAmount, task);
            _inflightExtend[key] = flight;
        }

        return AwaitAndClear(key, flight);
    }

    private async Task<LeaseState?> AwaitAndClear(string key, ExtendFlight flight)
    {
        try
        {
            return await flight.Task.ConfigureAwait(false);
        }
        finally
        {
            lock (_gate)
            {
                if (_inflightExtend.TryGetValue(key, out var current) && current == flight)
                {
                    _inflightExtend.Remove(key);
                }
            }
        }
    }

    private async Task<LeaseState?> ExtendAsync(
        LeaseState entry,
        ResolvedLeaseConfig resolved,
        double additionalAmount,
        RequestOptions? options
    )
    {
        try
        {
            var grant = await _wire
                .ExtendAsync(
                    entry.LeaseId,
                    additionalAmount,
                    _clock().Add(resolved.LeaseDuration),
                    options
                )
                .ConfigureAwait(false);

            // Reconcile the local row to the server's authoritative TOTAL. The
            // store computes the credit delta atomically against its current
            // total, not against the pre-wire-call read above: per-process
            // single-flight does not cover sibling pods, so two pods extending
            // the same shared lease concurrently would each apply a stale-read
            // delta and mint phantom credits. A total a sibling already applied
            // lands as a no-op, so the applies converge in any order. Pinned to
            // the lease the server extended: if it expired during the wire call
            // and a successor took the slot, the local extend is dropped rather
            // than minting the delta onto the successor.
            await _leaseStore
                .ExtendAsync(
                    entry.CompanyId,
                    entry.CreditTypeId,
                    grant.GrantedAmount,
                    grant.ExpiresAt,
                    entry.LeaseId
                )
                .ConfigureAwait(false);

            _logger.LogDebug(
                "Extended credit lease {LeaseId} to {Granted} (was {Previous} at last read, expires {ExpiresAt})",
                entry.LeaseId,
                grant.GrantedAmount,
                entry.GrantedAmount,
                grant.ExpiresAt
            );

            return await _leaseStore
                .GetAsync(entry.CompanyId, entry.CreditTypeId)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to extend credit lease {LeaseId}", entry.LeaseId);
            return null;
        }
    }

    /// <summary>
    /// Releases every live lease held in the store. Only safe when the store is
    /// per-process: those leases are exclusively this process's, so releasing
    /// them on close returns their unspent remainder to the company balance
    /// immediately instead of waiting out the lease expiry. A shared store must
    /// never do this, since sibling pods are still drawing on the same leases,
    /// and is excluded by the <see cref="ILeaseLister"/> check below. Best
    /// effort: failures are logged and the lease falls back to server-side
    /// expiry.
    /// </summary>
    public async Task ReleaseAllLocalLeasesAsync()
    {
        if (_leaseStore is not ILeaseLister lister)
        {
            return;
        }

        var entries = await lister.ListAsync().ConfigureAwait(false);
        if (entries.Count == 0)
        {
            return;
        }

        await Task.WhenAll(
                entries.Select(async entry =>
                {
                    // Skip expired leases: the server already swept and
                    // refunded them.
                    if (entry.ExpiresAt <= _clock())
                    {
                        return;
                    }
                    try
                    {
                        await _wire.ReleaseAsync(entry.LeaseId).ConfigureAwait(false);
                        await _leaseStore
                            .DropAsync(entry.CompanyId, entry.CreditTypeId)
                            .ConfigureAwait(false);
                        _logger.LogDebug(
                            "Released credit lease {LeaseId} on close",
                            entry.LeaseId
                        );
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(
                            ex,
                            "Failed to release credit lease {LeaseId} on close; it will expire server-side",
                            entry.LeaseId
                        );
                    }
                })
            )
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Refuses new lease work. Idempotent, and paired with
    /// <see cref="DrainAsync"/>: stopping first is what makes the drain
    /// terminate, since nothing can enqueue behind it.
    /// </summary>
    public void Stop() => _stopped = true;

    /// <summary>
    /// Waits out lease work already on the wire, so a close releases what that
    /// work installs instead of orphaning it. Bounded: whatever has not landed
    /// by the timeout is abandoned rather than stalling the caller's shutdown,
    /// and the credits it holds fall back to server-side expiry.
    /// </summary>
    public async Task DrainAsync(TimeSpan? timeout = null)
    {
        var budget = timeout ?? LeaseDefaults.ShutdownDrainTimeout;
        var deadline = DateTime.UtcNow.Add(budget);
        while (true)
        {
            List<Task> pending;
            lock (_gate)
            {
                pending = _inflightAcquire
                    .Values.Cast<Task>()
                    .Concat(_inflightExtend.Values.Select(flight => (Task)flight.Task))
                    .Concat(_background)
                    .ToList();
            }

            if (pending.Count == 0)
            {
                return;
            }

            // Settling one round can enqueue another (an acquire that loses its
            // race fires a release), so keep going until the set empties.
            if (!await SettleWithinAsync(pending, deadline - DateTime.UtcNow).ConfigureAwait(false))
            {
                _logger.LogWarning(
                    "Timed out after {Timeout}ms draining in-flight credit lease work; any credits it holds will be released by server-side expiry",
                    budget.TotalMilliseconds
                );
                return;
            }
        }
    }

    /// <summary>
    /// Awaits everything in <paramref name="tasks"/>, giving up after
    /// <paramref name="timeout"/>. Reports whether everything landed, so a
    /// caller winding down can say what it is abandoning. Abandoned work is not
    /// cancelled, it just stops being waited on.
    /// </summary>
    internal static async Task<bool> SettleWithinAsync(
        IReadOnlyCollection<Task> tasks,
        TimeSpan timeout
    )
    {
        if (tasks.Count == 0)
        {
            return true;
        }
        if (timeout <= TimeSpan.Zero)
        {
            return false;
        }

        // Faults are swallowed here: the caller wants to know whether the work
        // finished, not why it failed, and every task below already logs.
        var settled = Task.WhenAll(
            tasks.Select(task =>
                task.ContinueWith(
                    _ => { },
                    TaskContinuationOptions.ExecuteSynchronously
                )
            )
        );
        var winner = await Task.WhenAny(settled, Task.Delay(timeout)).ConfigureAwait(false);
        return winner == settled;
    }

    private async Task ReleaseQuietlyAsync(string leaseId)
    {
        try
        {
            await _wire.ReleaseAsync(leaseId).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to release redundant credit lease {LeaseId}", leaseId);
        }
    }

    /// <summary>
    /// Holds a reference to unawaited work so <see cref="DrainAsync"/> can wait
    /// it out.
    /// </summary>
    private Task<T> Track<T>(Task<T> task)
    {
        lock (_gate)
        {
            _background.Add(task);
        }
        _ = task.ContinueWith(
            completed =>
            {
                lock (_gate)
                {
                    _background.Remove(completed);
                }
            },
            TaskContinuationOptions.ExecuteSynchronously
        );
        return task;
    }

    private void Track(Task task)
    {
        lock (_gate)
        {
            _background.Add(task);
        }
        _ = task.ContinueWith(
            completed =>
            {
                lock (_gate)
                {
                    _background.Remove(completed);
                }
            },
            TaskContinuationOptions.ExecuteSynchronously
        );
    }

    /// <summary>
    /// An in-flight extend plus the additional amount its wire call asked for,
    /// the figure a joiner compares its own shortfall against.
    /// </summary>
    private sealed class ExtendFlight
    {
        public ExtendFlight(double requestedAdditional, Task<LeaseState?> task)
        {
            RequestedAdditional = requestedAdditional;
            Task = task;
        }

        public double RequestedAdditional { get; }

        public Task<LeaseState?> Task { get; }
    }
}
