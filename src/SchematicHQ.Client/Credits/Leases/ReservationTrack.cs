using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable enable

namespace SchematicHQ.Client.Leases;

/// <summary>
/// What a settle did locally, and what it owes the server.
/// </summary>
public sealed class SettleOutcome
{
    /// <summary>
    /// The billing event to emit.
    /// </summary>
    public required EventBodyTrack Track { get; init; }

    /// <summary>
    /// True when the hold was still open and this call debited the consumed
    /// slice and refunded the rest. False when it had already been swept at its
    /// TTL, already settled, or the store was unreachable: the lease balance was
    /// not touched here, so it reads high until the lease rolls over, and the
    /// track event is a recovery emit.
    /// </summary>
    public bool SettledLocally { get; init; }
}

public static class ReservationTrack
{
    /// <summary>
    /// Consumes a client-mode hold against its lease and builds the event that
    /// bills it.
    ///
    /// <para>The event comes from the caller-held record rather than the store,
    /// so the usage is still billed once the hold has been swept. Only the local
    /// bookkeeping clamps to the reserved amount; the event carries the
    /// unclamped actual.</para>
    /// </summary>
    public static async Task<SettleOutcome> ConsumeAndBuildEventAsync(
        IReservationStore reservations,
        ReservationRecord reservation,
        double actualQuantity,
        TrackWithReservationOptions? options = null
    )
    {
        // Rounded up for the same reason the hold is: the debit has to move
        // the local ledger by exactly what the track event bills.
        var claimed = await reservations
            .ConsumeAsync(
                reservation.Id,
                Math.Ceiling(actualQuantity) * reservation.ConsumptionRate
            )
            .ConfigureAwait(false);
        return new SettleOutcome
        {
            Track = BuildTrackEvent(reservation, actualQuantity, options),
            SettledLocally = claimed != null,
        };
    }

    /// <summary>
    /// Builds the event that settles a hold, from the record alone.
    ///
    /// <para>Kept free of store access so the client can still bill the usage
    /// when the local settle fails against an unreachable store: the server is
    /// the source of truth for real consumption.</para>
    /// </summary>
    public static EventBodyTrack BuildTrackEvent(
        ReservationRecord reservation,
        double actualQuantity,
        TrackWithReservationOptions? options = null
    )
    {
        var body = new EventBodyTrack
        {
            Event = reservation.EventSubtype,
            Quantity = SettleQuantity(actualQuantity),
            Company = reservation.Company,
            User = reservation.User,
            Traits = options?.Traits,
        };

        if (reservation.Mode == CreditLeaseMode.Server)
        {
            // The hold lives on the server, so the event settles it by id. Never
            // send a lease id too: the server prefers it when both are set, and
            // there is no lease here for it to route through.
            body.ReservationId = reservation.Id;
        }
        else
        {
            // Routes the server-side credit consumption through the lease's
            // sub-ledger instead of decrementing the grant again, which the
            // acquire already pre-debited. Without it the grant double-debits
            // and eventually starves redemptions mid-session.
            body.LeaseId = reservation.LeaseId;
        }

        return body;
    }

    /// <summary>
    /// Casts a settled usage onto the integer a track event records. A hold can
    /// be sized from a fractional usage, but the event's quantity is an integer,
    /// so a partial unit settles as a whole one rather than as none.
    /// </summary>
    public static long SettleQuantity(double actualQuantity) =>
        (long)Math.Ceiling(actualQuantity);
}
