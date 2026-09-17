using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable enable

namespace SchematicHQ.Client.Leases;

/// <summary>
/// Holds at most one lease per (company, credit type) slot. Every mutation is
/// atomic per slot: <see cref="InMemoryLeaseStore"/> gets that from a per-slot
/// lock, <see cref="RedisLeaseStore"/> from single-key Lua.
/// </summary>
public interface ILeaseStore
{
    /// <summary>
    /// Returns a snapshot of the slot, expired or not, or null when the slot is
    /// empty. Callers re-guard on expiry.
    /// </summary>
    Task<LeaseState?> GetAsync(string companyId, string creditTypeId);

    /// <summary>
    /// Installs a fresh lease at its full grant, if the slot is free to take,
    /// and reports whether it wrote.
    ///
    /// <para>A live lease holds the slot even when it carries a different id (a
    /// sibling pod won the acquire race): its already-debited balance wins and
    /// this reports false. An expired row carrying the SAME id is not rewritten
    /// either, since that would reset the balance and erase debits whose
    /// reservations are still open; it is reconciled like an extend (granted to
    /// the incoming total, expiry forward only, balance untouched) and also
    /// reports false. Only a fresh write reports true, which is what tells the
    /// manager whether the lease it just acquired is redundant.</para>
    /// </summary>
    Task<bool> ReplaceAsync(LeaseGrant grant);

    /// <summary>
    /// Atomically checks and debits, returning the post-debit balance and the
    /// lease the credits came out of. Returns null, touching nothing, when
    /// there is no lease, the lease has expired, the balance is short, or
    /// <paramref name="credits"/> is not a finite non-negative number.
    ///
    /// <para>The debit is not keyed by lease id: it charges whichever lease
    /// occupies the slot at that moment, which need not be the one the caller's
    /// acquire handed back. The caller must pin its reservation to the returned
    /// lease id, since the settle refund, the sweep refund, and the track
    /// event's lease id all have to name the lease that was actually
    /// charged.</para>
    /// </summary>
    Task<ReserveResult?> TryReserveAsync(string companyId, string creditTypeId, double credits);

    /// <summary>
    /// Returns credits to the slot's balance, clamped at the granted amount.
    /// With a non-empty <paramref name="pinLeaseId"/> the refund applies only
    /// while the slot still holds that lease: a hold carved out of an expired
    /// lease must never inflate its successor, whose grant the server already
    /// issued whole.
    /// </summary>
    Task RefundAsync(
        string companyId,
        string creditTypeId,
        double credits,
        string? pinLeaseId = null
    );

    /// <summary>
    /// Reconciles the slot to the server-authoritative total. The delta is
    /// computed inside the store against the currently stored total, never from
    /// a caller-held pre-wire-call read: two pods extending concurrently from
    /// the same stale read would each apply a delta and mint phantom credits. A
    /// total a sibling already applied is a no-op, so applies converge in any
    /// order. Expiry only ever moves forward. A non-empty
    /// <paramref name="pinLeaseId"/> drops the whole extend when the slot holds
    /// a different lease.
    /// </summary>
    Task ExtendAsync(
        string companyId,
        string creditTypeId,
        double grantedTotal,
        DateTime? newExpiresAt = null,
        string? pinLeaseId = null
    );

    /// <summary>
    /// Removes the slot entry, after a remote release.
    /// </summary>
    Task DropAsync(string companyId, string creditTypeId);
}

/// <summary>
/// Implemented only by a per-process store, whose leases are exclusively this
/// process's, so releasing them on close is safe. A shared backend must never
/// enumerate and release: sibling pods still draw on those leases.
/// </summary>
public interface ILeaseLister
{
    Task<IReadOnlyList<LeaseState>> ListAsync();
}
