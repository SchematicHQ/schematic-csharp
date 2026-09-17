using System;
using System.Collections.Generic;

#nullable enable

namespace SchematicHQ.Client.Leases;

/// <summary>
/// What a credit-aware check accepts.
/// </summary>
public sealed class CheckOptions
{
    /// <summary>
    /// Units of the metered event the operation is about to record. The check
    /// holds usage times the entitlement's consumption rate. One check issues at
    /// most one hold, against a single event subtype. Leave it null for a plain
    /// flag check with no hold.
    /// </summary>
    public double? Usage { get; set; }

    /// <summary>
    /// The event the usage applies to, for example "inference_tokens". It
    /// disambiguates which credit condition to gate on when a flag meters more
    /// than one event, and can be left unset when the flag's credit condition is
    /// unambiguous.
    /// </summary>
    public string? EventSubtype { get; set; }

    /// <summary>
    /// What to do when the check cannot gate. Defaults to
    /// <see cref="Leases.OnAcquireFailure.FailClosed"/>.
    /// </summary>
    public OnAcquireFailure OnAcquireFailure { get; set; } = OnAcquireFailure.FailClosed;

    /// <summary>
    /// Value to fall back to on error, overriding the client-wide flag default.
    /// </summary>
    public bool? DefaultValue { get; set; }

    /// <summary>
    /// Per-call timeout for the API calls this check makes: the lease acquire
    /// and extend in client mode, the check-and-reserve in server mode.
    /// </summary>
    public TimeSpan? Timeout { get; set; }
}

/// <summary>
/// What a credit-aware check resolved.
/// </summary>
public sealed class CheckResult
{
    /// <summary>
    /// Whether the caller may proceed.
    /// </summary>
    public bool Allowed { get; set; }

    /// <summary>
    /// The flag's boolean value. <see cref="Allowed"/> mirrors it on every path
    /// that takes no hold.
    /// </summary>
    public bool Value { get; set; }

    /// <summary>
    /// The hold this check carved out, when it took one. Pass it to
    /// <c>TrackWithReservation</c> once the work completes.
    /// </summary>
    public ReservationRecord? Reservation { get; set; }

    /// <summary>
    /// Why, from the rules engine or from the SDK.
    /// </summary>
    public string Reason { get; set; } = string.Empty;

    /// <summary>
    /// The entitlement behind the verdict.
    ///
    /// <para>For a credit-metered feature it carries the lease-aware three-way
    /// split the server computes: CreditRemaining (drawable now, open lease holds
    /// excluded), CreditReserved (the open lease's unspent hold), and
    /// CreditSettled (remaining plus reserved, the balance net of actual
    /// consumption). Bind a user-facing credits-remaining counter to
    /// CreditSettled: it is invariant to in-flight holds, so it does not dip
    /// mid-call the way the raw server balance does.</para>
    ///
    /// <para>These come from the company's resolved entitlement, so the lease
    /// balance a gating check substitutes does not distort them: the same values
    /// surface whether or not a usage was passed.</para>
    /// </summary>
    public RulesengineFeatureEntitlement? Entitlement { get; set; }

    public string FlagKey { get; set; } = string.Empty;

    public string? FlagId { get; set; }

    /// <summary>
    /// Set when the SDK resolved the check itself rather than on a verdict: the
    /// lease could not be acquired, the store was unreachable, the engine failed.
    /// </summary>
    public string? Error { get; set; }
}

/// <summary>
/// Extras a settle accepts.
/// </summary>
public sealed class TrackWithReservationOptions
{
    /// <summary>
    /// Traits to attach to the emitted track event.
    /// </summary>
    public Dictionary<string, object?>? Traits { get; set; }
}

public static class LeasePreflight
{
    /// <summary>
    /// Builds the preflight the caller's usage implies, for a client-side
    /// evaluation. With an event subtype the usage goes out scoped to it so the
    /// engine matches the subtype's condition; without one it goes out as the
    /// generic knob.
    ///
    /// <para>Shared with the plain-check fallback, so any client-side evaluation
    /// honors the caller's preflight even when the lease path cannot run.</para>
    /// </summary>
    public static PreflightRequestBody? Build(CheckOptions? options)
    {
        if (options?.Usage == null || !LeaseQuantity.IsValid(options.Usage.Value))
        {
            return null;
        }

        var quantity = PreflightQuantity(options.Usage.Value);
        if (!string.IsNullOrEmpty(options.EventSubtype))
        {
            return new PreflightRequestBody
            {
                EventUsage = new PreflightEventUsageRequestBody
                {
                    EventSubtype = options.EventSubtype!,
                    Quantity = quantity,
                },
            };
        }
        return new PreflightRequestBody { Usage = quantity };
    }

    /// <summary>
    /// Casts a usage onto the integer the engine's preflight carries. A hold can
    /// be sized from a fractional usage, but a preflight asks an upper-bound
    /// question, so a fraction rounds up: the check must not pass on less usage
    /// than the operation is about to record.
    /// </summary>
    public static long PreflightQuantity(double usage) => (long)Math.Ceiling(usage);
}
