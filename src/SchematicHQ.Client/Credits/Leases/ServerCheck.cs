using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

#nullable enable

namespace SchematicHQ.Client.Leases;

/// <summary>
/// The two server-mode calls a credit-aware check makes. Narrow on purpose: it
/// keeps the flow independent of the generated clients and lets tests script
/// the server.
/// </summary>
public interface IServerReservationClient
{
    Task<CheckAndReserveFlagResponseData> CheckAndReserveAsync(
        string flagKey,
        CheckAndReserveFlagRequestBody body,
        RequestOptions? options = null
    );

    Task ReleaseReservationAsync(string reservationId);
}

/// <summary>
/// Adapts the generated features and credits clients to
/// <see cref="IServerReservationClient"/>.
/// </summary>
public sealed class ApiServerReservationClient : IServerReservationClient
{
    private readonly FeaturesClient _features;
    private readonly CreditsClient _credits;

    public ApiServerReservationClient(FeaturesClient features, CreditsClient credits)
    {
        _features = features ?? throw new ArgumentNullException(nameof(features));
        _credits = credits ?? throw new ArgumentNullException(nameof(credits));
    }

    public async Task<CheckAndReserveFlagResponseData> CheckAndReserveAsync(
        string flagKey,
        CheckAndReserveFlagRequestBody body,
        RequestOptions? options = null
    )
    {
        var response = await _features
            .CheckAndReserveFlagAsync(flagKey, body, options)
            .ConfigureAwait(false);
        return response.Data;
    }

    public async Task ReleaseReservationAsync(string reservationId)
    {
        await _credits.ReleaseCreditReservationAsync(reservationId).ConfigureAwait(false);
    }
}

/// <summary>
/// Everything a server-mode check draws on.
/// </summary>
public sealed class ServerCheckDeps
{
    public required IServerReservationClient Client { get; init; }

    public required ILogger Logger { get; init; }

    /// <summary>
    /// How far out to set the hold's expiry.
    /// </summary>
    public required TimeSpan ReservationTTL { get; init; }

    /// <summary>
    /// Resolves the caller's default for this flag: the check's own default when
    /// set, otherwise the client-level flag default. The fail-open branch uses
    /// it, having no local engine to re-run.
    /// </summary>
    public required Func<bool> GetDefault { get; init; }

    public LeaseClock? Clock { get; init; }

    internal DateTime Now() => Clock == null ? DateTime.UtcNow : Clock();
}

public static class ServerCheck
{
    /// <summary>
    /// Mirrors the reason the API returns on a 200 with a false value for the
    /// same denial, so a caller matching on the reason has one string to match
    /// either way.
    /// </summary>
    public const string InsufficientCreditsReason = "Insufficient credits";

    /// <summary>
    /// Drives a single credit-aware check in server mode.
    ///
    /// <para>One check-and-reserve call does everything the client path spreads
    /// across a lease acquire, a local reserve, and a rules evaluation: the
    /// server evaluates the flag against the company's real balance, applies the
    /// preflight cost, and takes the hold, all in the same round trip. There is
    /// no lease, no local store, and no rules engine involved.</para>
    ///
    /// <para>The failure contract differs from client mode in one place.
    /// Fail-open there means re-run the engine with the credit balance assumed
    /// sufficient, so plan targeting and every non-credit condition still apply.
    /// Server mode has no local engine to re-run, since the call that would have
    /// answered is the one that failed, so fail-open returns the caller's default
    /// value instead. Fail-closed denies, same as client mode.</para>
    ///
    /// <para>No flag_check event is enqueued here: the server logs the flag check
    /// for check-and-reserve itself, the same way the REST check path does.</para>
    /// </summary>
    public static async Task<CheckResult> CheckWithServerReservationAsync(
        ServerCheckDeps deps,
        string flagKey,
        Dictionary<string, string>? company,
        Dictionary<string, string>? user,
        CheckOptions options,
        Func<Task<CheckResult>> fallback
    )
    {
        var logger = deps.Logger;
        var failOpen = options.OnAcquireFailure == OnAcquireFailure.FailOpen;

        // Same guard as the client path: a malformed usage must never reach the
        // wire. NaN slips through every numeric comparison, so the server would
        // size a hold off a value no comparison can reject.
        if (options.Usage == null || !LeaseQuantity.IsValid(options.Usage.Value))
        {
            logger.LogError(
                "Server reservation: invalid usage {Usage} for flag {FlagKey}; must be a finite, non-negative number",
                options.Usage,
                flagKey
            );
            return FailureResult(deps, failOpen, flagKey, "invalid_usage");
        }

        // Nothing to hold. The plain check still carries the preflight; a
        // zero-credit hold would only be a no-op.
        if (options.Usage.Value == 0)
        {
            logger.LogDebug(
                "Server reservation: usage is 0 for flag {FlagKey}, nothing to reserve, using a plain check",
                flagKey
            );
            return await fallback().ConfigureAwait(false);
        }

        var body = new CheckAndReserveFlagRequestBody
        {
            Company = company,
            User = user,
            Quantity = options.Usage.Value,
            ExpiresAt = deps.Now() + deps.ReservationTTL,
            Preflight = LeasePreflight.Build(options),
            // A hold is a side effect, so a retry without a key would take a
            // second one and park the first until its TTL. The key is minted
            // once per check and the retry loop resends this body, so every
            // attempt of this check collapses to one hold.
            IdempotencyKey = Guid.NewGuid().ToString(),
        };

        var requestOptions = options.Timeout == null
            ? null
            : new RequestOptions { Timeout = options.Timeout };

        CheckAndReserveFlagResponseData data;
        try
        {
            data = await deps
                .Client.CheckAndReserveAsync(flagKey, body, requestOptions)
                .ConfigureAwait(false);
        }
        catch (SchematicApiException ex) when (ex.StatusCode == 402)
        {
            // A 402 is the server's definitive answer, not a can't-gate: it
            // knows the credits are not there. Deny regardless of the failure
            // mode, since failing open here would hand out credit the balance
            // cannot cover. Check-and-reserve itself answers 200 with a false
            // value for insufficient credits; this is defensive.
            return new CheckResult
            {
                Reason = InsufficientCreditsReason,
                FlagKey = flagKey,
                Error = PaymentRequiredMessage(ex),
            };
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Server reservation: check-and-reserve for flag {FlagKey} failed",
                flagKey
            );
            return FailureResult(deps, failOpen, flagKey, "server_reservation_failed");
        }

        var entitlement = LeaseEntitlement.FromApi(data.Entitlement);
        var resolvedFlagKey = string.IsNullOrEmpty(data.Flag) ? flagKey : data.Flag;

        // No hold comes back when the flag denied, the credits were insufficient
        // (a 200 with a false value), or the feature is not credit-metered.
        // Nothing was held, so there is nothing to release.
        if (!data.Value || data.Reservation == null)
        {
            return new CheckResult
            {
                Allowed = data.Value,
                Value = data.Value,
                Reason = data.Reason,
                Entitlement = entitlement,
                FlagKey = resolvedFlagKey,
                FlagId = data.FlagId,
                Error = data.Error,
            };
        }

        var held = data.Reservation;
        // The settling track event is named by the event subtype; the caller's
        // explicit one wins, otherwise the server names it on the hold. With
        // neither, the hold can never be settled, so release it now rather than
        // leaving credits parked until the TTL.
        var eventSubtype = string.IsNullOrEmpty(options.EventSubtype)
            ? held.EventSubtype ?? string.Empty
            : options.EventSubtype!;
        if (eventSubtype.Length == 0)
        {
            logger.LogError(
                "Server reservation: reservation {ReservationId} for flag {FlagKey} has no event subtype; releasing, it could never be settled",
                held.Id,
                flagKey
            );
            try
            {
                await deps.Client.ReleaseReservationAsync(held.Id).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger.LogWarning(
                    ex,
                    "Server reservation: failed to release {ReservationId}; its hold is refunded when it expires",
                    held.Id
                );
            }

            if (!failOpen)
            {
                return FailureResult(deps, false, flagKey, "missing_event_subtype");
            }
            // Fail-open means assume the credits are there, and the server has
            // already evaluated the flag and allowed this check. Only the settle
            // is impossible, so keep the server's verdict rather than falling
            // back to the caller's default, which could deny what the server
            // allowed.
            return new CheckResult
            {
                Allowed = data.Value,
                Value = data.Value,
                Reason = data.Reason,
                Entitlement = entitlement,
                FlagKey = resolvedFlagKey,
                FlagId = data.FlagId,
                Error = "missing_event_subtype",
            };
        }

        return new CheckResult
        {
            Allowed = true,
            Value = true,
            Reservation = new ReservationRecord
            {
                Id = held.Id,
                // No lease exists in server mode; mirror the id so the field
                // stays populated and a handle round-trips through code that
                // reads it.
                LeaseId = held.Id,
                Mode = CreditLeaseMode.Server,
                CompanyId = held.CompanyId,
                CreditTypeId = held.CreditTypeId,
                EventSubtype = eventSubtype,
                QuantityReserved = held.QuantityReserved,
                CreditsReserved = held.CreditsReserved,
                ConsumptionRate = held.ConsumptionRate,
                ExpiresAt = held.ExpiresAt,
                Company = company,
                User = user,
            },
            Reason = data.Reason,
            Entitlement = entitlement,
            FlagKey = resolvedFlagKey,
            FlagId = data.FlagId,
        };
    }

    /// <summary>
    /// Resolves a can't-gate outcome. Fail-closed denies; fail-open returns the
    /// caller's default value, there being no local engine to re-evaluate with an
    /// assumed-sufficient balance the way client mode does.
    /// </summary>
    private static CheckResult FailureResult(
        ServerCheckDeps deps,
        bool failOpen,
        string flagKey,
        string reason
    )
    {
        if (!failOpen)
        {
            return new CheckResult
            {
                Reason = reason,
                FlagKey = flagKey,
                Error = reason,
            };
        }

        var value = deps.GetDefault();
        return new CheckResult
        {
            Allowed = value,
            Value = value,
            Reason = reason + "_fail_open",
            FlagKey = flagKey,
            Error = reason,
        };
    }

    private static string PaymentRequiredMessage(SchematicApiException error) =>
        error.Body is ApiError body ? body.Error : error.Message;
}

public static class LeaseEntitlement
{
    /// <summary>
    /// Maps the API's entitlement onto the rules engine's, which is what every
    /// check result carries.
    ///
    /// <para>Every field crosses, including the lease-aware credit split
    /// (remaining, reserved, settled) and the consumption rate a credit-metered
    /// feature is billed at: a server-mode check has no other source for them.</para>
    /// </summary>
    public static RulesengineFeatureEntitlement? FromApi(FeatureEntitlement? entitlement)
    {
        if (entitlement == null)
        {
            return null;
        }

        return new RulesengineFeatureEntitlement
        {
            Allocation = entitlement.Allocation,
            ConsumptionRate = entitlement.ConsumptionRate,
            CreditId = entitlement.CreditId,
            CreditRemaining = entitlement.CreditRemaining,
            CreditReserved = entitlement.CreditReserved,
            CreditSettled = entitlement.CreditSettled,
            CreditTotal = entitlement.CreditTotal,
            CreditUsed = entitlement.CreditUsed,
            EventName = entitlement.EventName,
            EventSubtype = entitlement.EventSubtype,
            FeatureId = entitlement.FeatureId,
            FeatureKey = entitlement.FeatureKey,
            MetricPeriod = entitlement.MetricPeriod.HasValue
                ? new RulesengineMetricPeriod(entitlement.MetricPeriod.Value.Value)
                : null,
            MetricResetAt = entitlement.MetricResetAt,
            MonthReset = entitlement.MonthReset.HasValue
                ? new RulesengineMetricPeriodMonthReset(entitlement.MonthReset.Value.Value)
                : null,
            SoftLimit = entitlement.SoftLimit,
            Usage = entitlement.Usage,
            ValueType = new RulesengineEntitlementValueType(entitlement.ValueType.Value),
        };
    }
}
