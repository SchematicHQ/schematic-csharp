using System;
using System.Collections.Generic;

#nullable enable

namespace SchematicHQ.Client.Leases;

/// <summary>
/// Reads the current UTC time. Every store and the lease manager take one so
/// tests and the conformance runner can drive a virtual clock instead of wall
/// time.
/// </summary>
public delegate DateTime LeaseClock();

/// <summary>
/// The local view of the one lease a (company, credit type) slot holds.
/// </summary>
public sealed class LeaseState
{
    public string LeaseId { get; set; } = string.Empty;

    public string CompanyId { get; set; } = string.Empty;

    public string CreditTypeId { get; set; } = string.Empty;

    /// <summary>
    /// Server-authoritative total granted to this lease. It grows on extend.
    /// </summary>
    public double GrantedAmount { get; set; }

    /// <summary>
    /// Granted minus outstanding holds and consumption. It starts at the full
    /// grant when the lease is installed.
    /// </summary>
    public double LocalRemainingCredits { get; set; }

    /// <summary>
    /// The instant past which the lease is dead: the server has refunded the
    /// remainder to the company balance, so the local balance is stale and must
    /// never serve another reserve.
    /// </summary>
    public DateTime ExpiresAt { get; set; }

    internal LeaseState Copy() => (LeaseState)MemberwiseClone();
}

/// <summary>
/// What the server says a lease is, after an acquire or an extend. It is also
/// what installs a lease into a store: the local balance is derived, never
/// supplied.
/// </summary>
public sealed class LeaseGrant
{
    public string LeaseId { get; set; } = string.Empty;

    public string CompanyId { get; set; } = string.Empty;

    public string CreditTypeId { get; set; } = string.Empty;

    /// <summary>
    /// The server-authoritative TOTAL, not the increment an extend asked for.
    /// </summary>
    public double GrantedAmount { get; set; }

    public DateTime ExpiresAt { get; set; }
}

/// <summary>
/// One credit hold carved out of a lease by a check.
/// </summary>
public sealed class ReservationRecord
{
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The lease the hold was carved from. It pins refunds, so a hold from an
    /// expired lease can never inflate its successor.
    /// </summary>
    public string LeaseId { get; set; } = string.Empty;

    public string CompanyId { get; set; } = string.Empty;

    public string CreditTypeId { get; set; } = string.Empty;

    /// <summary>
    /// Where the hold lives. A client-mode hold is carved out of a local lease
    /// and is what the stores keep; a server-mode hold lives on the server and
    /// never enters a store, so this field is not persisted by either backend.
    /// It decides which id the settling track event carries.
    /// </summary>
    public CreditLeaseMode Mode { get; set; } = CreditLeaseMode.Client;

    /// <summary>
    /// What the settling track event is billed as.
    /// </summary>
    public string EventSubtype { get; set; } = string.Empty;

    /// <summary>
    /// The caller-declared usage, in event units.
    /// </summary>
    public double QuantityReserved { get; set; }

    /// <summary>
    /// <see cref="QuantityReserved"/> times <see cref="ConsumptionRate"/>.
    /// </summary>
    public double CreditsReserved { get; set; }

    public double ConsumptionRate { get; set; }

    /// <summary>
    /// The instant past which the sweeper refunds the full hold.
    /// </summary>
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// The evaluation context the hold was issued for, threaded onto the track
    /// event so the server attributes usage to the same company and user.
    /// </summary>
    public Dictionary<string, string>? Company { get; set; }

    public Dictionary<string, string>? User { get; set; }

    internal ReservationRecord Copy()
    {
        var copy = (ReservationRecord)MemberwiseClone();
        copy.Company = Company == null ? null : new Dictionary<string, string>(Company);
        copy.User = User == null ? null : new Dictionary<string, string>(User);
        return copy;
    }
}

/// <summary>
/// The outcome of a successful reserve: the post-debit balance plus the id of
/// the lease the credits actually came out of.
/// </summary>
public readonly struct ReserveResult
{
    public ReserveResult(double balance, string leaseId)
    {
        Balance = balance;
        LeaseId = leaseId;
    }

    /// <summary>
    /// The balance after the debit. The caller derives the pre-debit figure as
    /// <c>Balance + credits</c> without a racy follow-up read.
    /// </summary>
    public double Balance { get; }

    /// <summary>
    /// The lease the debit landed on, read in the same atomic step as the
    /// debit. The slot's lease can be replaced between a caller's acquire and
    /// its reserve, so a hold pinned to the lease the caller last saw would
    /// send its refunds to a lease that never held the credits.
    /// </summary>
    public string LeaseId { get; }
}

public static class LeaseKeys
{
    /// <summary>
    /// The (company, credit type) slot key. Shared by both backends so slots
    /// are namespaced alike.
    /// </summary>
    public static string Slot(string companyId, string creditTypeId) =>
        companyId + ":" + creditTypeId;
}

public static class LeaseTime
{
    /// <summary>
    /// Epoch milliseconds, the wire form both Redis backends store expiries in
    /// so every SDK reads the same row the same way.
    /// </summary>
    public static long ToUnixMilliseconds(DateTime value)
    {
        // An unspecified kind is treated as UTC rather than local: every
        // DateTime the lease paths handle comes from the API or from UtcNow, so
        // converting an unspecified one as local would shift the expiry by the
        // host's offset.
        var utc =
            value.Kind == DateTimeKind.Local
                ? value.ToUniversalTime()
                : DateTime.SpecifyKind(value, DateTimeKind.Utc);
        return new DateTimeOffset(utc).ToUnixTimeMilliseconds();
    }

    public static DateTime FromUnixMilliseconds(long value) =>
        DateTimeOffset.FromUnixTimeMilliseconds(value).UtcDateTime;
}

public static class LeaseQuantity
{
    /// <summary>
    /// The largest quantity that survives the trip to an event's integer
    /// quantity. Past this a double no longer holds consecutive integers, and
    /// the cast that rounds a quantity up stops meaning anything: on .NET Core
    /// it saturates to long.MaxValue and on .NET Framework it wraps to a
    /// negative number, so a caller who passed something absurd would bill one
    /// of those rather than be refused.
    /// </summary>
    public const double MaxQuantity = 9007199254740991;

    /// <summary>
    /// Reports whether a caller-supplied quantity can size a credit hold. NaN
    /// is the dangerous case: it slips through every numeric comparison, and a
    /// NaN balance would approve every later reserve on a possibly shared
    /// lease.
    /// </summary>
    public static bool IsValid(double value) =>
        !double.IsNaN(value)
        && !double.IsInfinity(value)
        && value >= 0
        && value <= MaxQuantity;
}
