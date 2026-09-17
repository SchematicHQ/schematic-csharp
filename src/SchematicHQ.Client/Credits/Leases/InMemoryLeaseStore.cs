using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable enable

namespace SchematicHQ.Client.Leases;

/// <summary>
/// Keeps lease slots in this process only, so it gates a single pod. Swap in
/// <see cref="RedisLeaseStore"/> to gate across pods; both implement
/// <see cref="ILeaseStore"/>.
///
/// <para>Per-slot atomicity comes from one monitor over the whole table rather
/// than a lock per slot: every mutation below is a handful of field updates
/// with no await inside it, so a single lock serializes them without the
/// bookkeeping a per-slot lock map needs.</para>
/// </summary>
public sealed class InMemoryLeaseStore : ILeaseStore, ILeaseLister
{
    private readonly object _gate = new();
    private readonly Dictionary<string, LeaseState> _leases = new();
    private readonly LeaseClock _clock;

    public InMemoryLeaseStore(LeaseClock? clock = null)
    {
        _clock = clock ?? (() => DateTime.UtcNow);
    }

    public Task<LeaseState?> GetAsync(string companyId, string creditTypeId)
    {
        var key = LeaseKeys.Slot(companyId, creditTypeId);
        lock (_gate)
        {
            return Task.FromResult(_leases.TryGetValue(key, out var entry) ? entry.Copy() : null);
        }
    }

    public Task<bool> ReplaceAsync(LeaseGrant grant)
    {
        var key = LeaseKeys.Slot(grant.CompanyId, grant.CreditTypeId);
        lock (_gate)
        {
            if (_leases.TryGetValue(key, out var existing))
            {
                if (existing.ExpiresAt > _clock())
                {
                    // A live lease already holds this slot. Preserve its
                    // already-debited balance instead of clobbering it.
                    return Task.FromResult(false);
                }

                if (existing.LeaseId == grant.LeaseId)
                {
                    // The same lease coming back over its own expired row: a
                    // stale acquire response for a lease the idempotent server
                    // also handed a racing sibling, which may since have
                    // extended it. Rewriting would reset the balance and erase
                    // debits whose reservations are still open, so reconcile
                    // like an extend instead.
                    var add = grant.GrantedAmount - existing.GrantedAmount;
                    if (add > 0)
                    {
                        existing.GrantedAmount = grant.GrantedAmount;
                        existing.LocalRemainingCredits += add;
                    }
                    if (grant.ExpiresAt > existing.ExpiresAt)
                    {
                        existing.ExpiresAt = grant.ExpiresAt;
                    }
                    return Task.FromResult(false);
                }
            }

            _leases[key] = new LeaseState
            {
                LeaseId = grant.LeaseId,
                CompanyId = grant.CompanyId,
                CreditTypeId = grant.CreditTypeId,
                GrantedAmount = grant.GrantedAmount,
                LocalRemainingCredits = grant.GrantedAmount,
                ExpiresAt = grant.ExpiresAt,
            };
            return Task.FromResult(true);
        }
    }

    public Task<ReserveResult?> TryReserveAsync(
        string companyId,
        string creditTypeId,
        double credits
    )
    {
        // Reject non-finite and negative debits outright: NaN passes every
        // comparison below and a NaN balance would approve every later reserve.
        if (!LeaseQuantity.IsValid(credits))
        {
            return Task.FromResult<ReserveResult?>(null);
        }

        var key = LeaseKeys.Slot(companyId, creditTypeId);
        lock (_gate)
        {
            if (!_leases.TryGetValue(key, out var entry))
            {
                return Task.FromResult<ReserveResult?>(null);
            }
            // Never reserve against an expired lease: the server treats it as
            // released and refunds the grant to the company balance, so the
            // local balance is stale.
            if (entry.ExpiresAt <= _clock())
            {
                return Task.FromResult<ReserveResult?>(null);
            }
            if (entry.LocalRemainingCredits < credits)
            {
                return Task.FromResult<ReserveResult?>(null);
            }

            entry.LocalRemainingCredits -= credits;
            // The lease id is read under the same lock as the debit: the caller
            // pins its reservation to it, so a read afterwards could name a
            // lease that replaced this one in between.
            return Task.FromResult<ReserveResult?>(
                new ReserveResult(entry.LocalRemainingCredits, entry.LeaseId)
            );
        }
    }

    public Task RefundAsync(
        string companyId,
        string creditTypeId,
        double credits,
        string? pinLeaseId = null
    )
    {
        if (credits <= 0)
        {
            return Task.CompletedTask;
        }

        var key = LeaseKeys.Slot(companyId, creditTypeId);
        lock (_gate)
        {
            if (!_leases.TryGetValue(key, out var entry))
            {
                return Task.CompletedTask;
            }
            if (!string.IsNullOrEmpty(pinLeaseId) && entry.LeaseId != pinLeaseId)
            {
                return Task.CompletedTask;
            }
            entry.LocalRemainingCredits = Math.Min(
                entry.LocalRemainingCredits + credits,
                entry.GrantedAmount
            );
            return Task.CompletedTask;
        }
    }

    public Task ExtendAsync(
        string companyId,
        string creditTypeId,
        double grantedTotal,
        DateTime? newExpiresAt = null,
        string? pinLeaseId = null
    )
    {
        var key = LeaseKeys.Slot(companyId, creditTypeId);
        lock (_gate)
        {
            if (!_leases.TryGetValue(key, out var entry))
            {
                return Task.CompletedTask;
            }
            if (!string.IsNullOrEmpty(pinLeaseId) && entry.LeaseId != pinLeaseId)
            {
                return Task.CompletedTask;
            }

            var add = grantedTotal - entry.GrantedAmount;
            if (add > 0)
            {
                entry.GrantedAmount = grantedTotal;
                entry.LocalRemainingCredits += add;
            }
            // Expiry only moves forward: an out-of-order apply must not shorten
            // a lease a concurrent extend already pushed out.
            if (newExpiresAt.HasValue && newExpiresAt.Value > entry.ExpiresAt)
            {
                entry.ExpiresAt = newExpiresAt.Value;
            }
            return Task.CompletedTask;
        }
    }

    public Task DropAsync(string companyId, string creditTypeId)
    {
        var key = LeaseKeys.Slot(companyId, creditTypeId);
        lock (_gate)
        {
            _leases.Remove(key);
            return Task.CompletedTask;
        }
    }

    public Task<IReadOnlyList<LeaseState>> ListAsync()
    {
        lock (_gate)
        {
            var snapshot = new List<LeaseState>(_leases.Count);
            foreach (var entry in _leases.Values)
            {
                snapshot.Add(entry.Copy());
            }
            return Task.FromResult<IReadOnlyList<LeaseState>>(snapshot);
        }
    }
}
