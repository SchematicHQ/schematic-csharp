using System;
using System.Threading.Tasks;

#nullable enable

namespace SchematicHQ.Client.Leases;

/// <summary>
/// The table of open credit holds, paired with a sweep loop that returns
/// expired holds to their underlying lease.
/// </summary>
public interface IReservationStore : IDisposable
{
    /// <summary>
    /// Registers a hold. Idempotent on id. It does NOT debit the lease: the
    /// debit already happened in the store's reserve.
    /// </summary>
    Task AddAsync(ReservationRecord reservation);

    Task<ReservationRecord?> GetAsync(string id);

    /// <summary>
    /// Claims a hold exactly once: atomically removes the reservation, then
    /// clamps <paramref name="creditsConsumed"/> to the reserved amount,
    /// refunds the unspent slice to the lease (pinned to the reservation's
    /// lease id) and returns the clamped figure. Returns null, touching
    /// nothing, when the reservation was already gone (swept, or claimed by a
    /// racing caller).
    ///
    /// <para>The claim and the refund are two steps, and the claim is the
    /// arbiter: a crash between them loses the refund rather than allowing a
    /// double refund.</para>
    /// </summary>
    Task<double?> ConsumeAsync(string id, double creditsConsumed);

    /// <summary>
    /// Sum of the credits held by open reservations for a (company, credit
    /// type) slot. A reservation counts while it is still in the table, so the
    /// lease's local remaining plus this sum stays exact between operations.
    /// </summary>
    Task<double> ReservedCreditsAsync(string companyId, string creditTypeId);

    /// <summary>
    /// Removes every reservation expired at <paramref name="now"/> and refunds
    /// its full hold to its lease, pinned to its lease id so a stale-lease hold
    /// is dropped rather than refunded. Returns how many were swept.
    /// </summary>
    Task<int> SweepExpiredAsync(DateTime? now = null);

    /// <summary>
    /// Starts the background sweep loop. Safe to call repeatedly.
    /// </summary>
    void StartSweep();

    /// <summary>
    /// Stops the background sweep loop.
    /// </summary>
    void Stop();

    /// <summary>
    /// Current count of open reservations, for tests and diagnostics.
    /// </summary>
    Task<long> CountAsync();
}
