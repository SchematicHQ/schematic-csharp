using System;
using System.Collections.Generic;

#nullable enable

namespace SchematicHQ.Client.Leases;

/// <summary>
/// Where a credit hold lives for a check that passes a usage quantity.
/// </summary>
public enum CreditLeaseMode
{
    /// <summary>
    /// Client mode when datastream is enabled, server mode otherwise.
    /// </summary>
    Auto,

    /// <summary>
    /// Local leases over datastream: the SDK draws a tranche of credits up
    /// front and carves reservations out of it locally. Requires datastream (or
    /// replicator mode), and a shared Redis backend to gate across pods.
    /// </summary>
    Client,

    /// <summary>
    /// One check-and-reserve API call per check: the server evaluates the flag
    /// and takes the hold in the same round trip. No datastream, no Redis, no
    /// local stores.
    /// </summary>
    Server,
}

/// <summary>
/// What a check does when it cannot gate: the lease could not be acquired, the
/// store is unreachable, or the lease is exhausted.
/// </summary>
public enum OnAcquireFailure
{
    /// <summary>
    /// Deny when the gate cannot gate. The default.
    /// </summary>
    FailClosed,

    /// <summary>
    /// Err on the side of assuming the credits are there. In client mode the
    /// rules engine still evaluates the flag with the credit balance
    /// substituted to an effectively unlimited value, so plan targeting,
    /// overrides, and every non-credit condition still apply and only the
    /// credit gate is bypassed. No reservation is issued.
    /// </summary>
    FailOpen,
}

public static class LeaseDefaults
{
    /// <summary>
    /// Lease lifetime requested at acquire and extend: expiresAt = now +
    /// duration.
    /// </summary>
    public static readonly TimeSpan LeaseDuration = TimeSpan.FromMinutes(5);

    /// <summary>
    /// Reservation lifetime, and the sweep deadline. Size it above the longest
    /// expected gap between a check and its settle: a settle arriving after the
    /// TTL still bills the server but no longer re-debits the lease, so the
    /// local balance reads high until the lease rolls over.
    /// </summary>
    public static readonly TimeSpan ReservationTTL = TimeSpan.FromSeconds(60);

    /// <summary>
    /// The furthest out the API will hold credits, so a larger server-mode
    /// reservation TTL would fail every check.
    /// </summary>
    public static readonly TimeSpan MaxReservationTTL = TimeSpan.FromHours(1);

    /// <summary>
    /// Held back from <see cref="MaxReservationTTL"/> because the API measures
    /// that hour against its own clock while the SDK computes expiresAt against
    /// the caller's: a client running ahead would otherwise be rejected at
    /// exactly the cap.
    /// </summary>
    public static readonly TimeSpan ReservationTTLSkewAllowance = TimeSpan.FromSeconds(60);

    /// <summary>
    /// Credits requested per acquire, and the minimum extend tranche.
    /// </summary>
    public const double LeaseSize = 10000;

    /// <summary>
    /// The remaining-over-granted ratio at or below which a background extend
    /// is kicked off.
    /// </summary>
    public const double LowWaterMark = 0.25;

    /// <summary>
    /// The cadence of the expired-reservation sweep.
    /// </summary>
    public static readonly TimeSpan SweepInterval = TimeSpan.FromSeconds(1);

    /// <summary>
    /// How long a prewarm waits for a freshly identified company to surface in
    /// the datastream cache before giving up. Long enough to cover the
    /// buffer-flush, server-ingest and datastream-push round trip for a new
    /// company; short enough that a misconfigured caller does not hang.
    /// </summary>
    public static readonly TimeSpan PrewarmResolveTimeout = TimeSpan.FromSeconds(5);

    /// <summary>
    /// How often a prewarm re-asks the datastream for a company it is waiting
    /// on.
    /// </summary>
    public static readonly TimeSpan PrewarmPollInterval = TimeSpan.FromMilliseconds(100);

    /// <summary>
    /// How long a shutdown waits for in-flight lease work to land before giving
    /// up on it. Bounded on purpose: a shutdown that hangs is worse than a hold
    /// the server expires at <see cref="LeaseDuration"/>.
    /// </summary>
    public static readonly TimeSpan ShutdownDrainTimeout = TimeSpan.FromSeconds(5);

    /// <summary>
    /// Namespaces every lease and reservation key. Matches the other SDKs, so
    /// mixed-language fleets share one Redis.
    /// </summary>
    public const string KeyPrefix = "schematic:";
}

/// <summary>
/// The knobs for a single credit type, after overrides and defaults.
/// </summary>
public sealed class ResolvedLeaseConfig
{
    public TimeSpan LeaseDuration { get; set; } = LeaseDefaults.LeaseDuration;

    public TimeSpan ReservationTTL { get; set; } = LeaseDefaults.ReservationTTL;

    public double LeaseSize { get; set; } = LeaseDefaults.LeaseSize;

    public double LowWaterMark { get; set; } = LeaseDefaults.LowWaterMark;
}

/// <summary>
/// Overrides the four resolvable knobs for one credit type. A null field leaves
/// the client-wide value in place.
/// </summary>
public sealed class LeaseOverride
{
    public TimeSpan? DefaultLeaseDuration { get; set; }

    public TimeSpan? DefaultReservationTTL { get; set; }

    public double? DefaultLeaseSize { get; set; }

    public double? LowWaterMark { get; set; }
}

/// <summary>
/// Enables credit reservation behavior on <c>Schematic.Check</c> and
/// <c>Schematic.TrackWithReservation</c>. Omit to keep the client
/// credit-unaware.
/// </summary>
public sealed class CreditLeaseConfig
{
    /// <summary>
    /// Where the credit hold lives. Defaults to <see cref="CreditLeaseMode.Auto"/>.
    /// </summary>
    public CreditLeaseMode Mode { get; set; } = CreditLeaseMode.Auto;

    /// <summary>
    /// Default lease duration. Defaults to <see cref="LeaseDefaults.LeaseDuration"/>.
    /// </summary>
    public TimeSpan? DefaultLeaseDuration { get; set; }

    /// <summary>
    /// Default reservation TTL. Defaults to <see cref="LeaseDefaults.ReservationTTL"/>.
    /// In server mode it is capped at <see cref="LeaseDefaults.MaxReservationTTL"/>
    /// less <see cref="LeaseDefaults.ReservationTTLSkewAllowance"/>, since an
    /// hour out is the furthest the API will hold credits and it measures that
    /// against its own clock.
    /// </summary>
    public TimeSpan? DefaultReservationTTL { get; set; }

    /// <summary>
    /// Default lease size (credit amount requested). Defaults to
    /// <see cref="LeaseDefaults.LeaseSize"/>.
    /// </summary>
    public double? DefaultLeaseSize { get; set; }

    /// <summary>
    /// Fraction of remaining lease balance below which the SDK kicks off a
    /// background extend. Defaults to <see cref="LeaseDefaults.LowWaterMark"/>.
    /// </summary>
    public double? LowWaterMark { get; set; }

    /// <summary>
    /// Sweep cadence for expired reservations. Defaults to
    /// <see cref="LeaseDefaults.SweepInterval"/>.
    /// </summary>
    public TimeSpan? SweepInterval { get; set; }

    /// <summary>
    /// Max time a prewarm waits for a freshly identified company to surface in
    /// the datastream cache when only secondary keys are passed. Set to
    /// <see cref="TimeSpan.Zero"/> to skip waiting entirely, so prewarm bails
    /// immediately unless the company is already cached. Defaults to
    /// <see cref="LeaseDefaults.PrewarmResolveTimeout"/>.
    /// </summary>
    public TimeSpan? PrewarmResolveTimeout { get; set; }

    /// <summary>
    /// Pre-connected Redis backend for lease and reservation state. Optional:
    /// when omitted the SDK reuses the datastream cache's Redis connection if
    /// one is configured, so an existing Redis setup backs leases
    /// automatically. Set this only to point lease state at a different Redis
    /// than the datastream cache.
    ///
    /// <para>When neither this nor a datastream Redis cache is configured the
    /// SDK falls back to per-process in-memory stores, which is single-pod only
    /// since cross-pod gating is lost (a warning is logged).</para>
    /// </summary>
    public ILeaseRedis? RedisClient { get; set; }

    /// <summary>
    /// Redis connection settings used to build a backend when
    /// <see cref="RedisClient"/> is not supplied. Falls back to the client's
    /// configured Redis cache settings when unset.
    /// </summary>
    public Datastream.RedisCacheConfig? RedisConfig { get; set; }

    /// <summary>
    /// Optional Redis key prefix. Falls back to the datastream cache's prefix
    /// when unset, then to <see cref="LeaseDefaults.KeyPrefix"/>.
    /// </summary>
    public string? RedisKeyPrefix { get; set; }

    /// <summary>
    /// Per-credit-type overrides, keyed by credit type id.
    /// </summary>
    public Dictionary<string, LeaseOverride>? Overrides { get; set; }

    /// <summary>
    /// Resolves the knobs for one credit type: the credit type's override wins,
    /// then the client-wide value, then the package default.
    /// </summary>
    public ResolvedLeaseConfig Resolve(string creditTypeId)
    {
        var resolved = new ResolvedLeaseConfig
        {
            LeaseDuration = DefaultLeaseDuration ?? LeaseDefaults.LeaseDuration,
            ReservationTTL = DefaultReservationTTL ?? LeaseDefaults.ReservationTTL,
            LeaseSize = DefaultLeaseSize ?? LeaseDefaults.LeaseSize,
            LowWaterMark = LowWaterMark ?? LeaseDefaults.LowWaterMark,
        };

        if (Overrides == null || !Overrides.TryGetValue(creditTypeId, out var over) || over == null)
        {
            return resolved;
        }

        if (over.DefaultLeaseDuration.HasValue)
        {
            resolved.LeaseDuration = over.DefaultLeaseDuration.Value;
        }
        if (over.DefaultReservationTTL.HasValue)
        {
            resolved.ReservationTTL = over.DefaultReservationTTL.Value;
        }
        if (over.DefaultLeaseSize.HasValue)
        {
            resolved.LeaseSize = over.DefaultLeaseSize.Value;
        }
        if (over.LowWaterMark.HasValue)
        {
            resolved.LowWaterMark = over.LowWaterMark.Value;
        }

        return resolved;
    }
}
