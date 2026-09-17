using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SchematicHQ.Client.RulesEngine;

#nullable enable

namespace SchematicHQ.Client.Leases;

/// <summary>
/// Everything a lease-bearing check draws on, gathered by the client.
/// </summary>
public sealed class CheckDeps
{
    public required ILeaseStore LeaseStore { get; init; }

    public required IReservationStore Reservations { get; init; }

    public required CreditLeaseManager Manager { get; init; }

    public required ILogger Logger { get; init; }

    /// <summary>
    /// Null when the client has no datastream, which the flow treats as a
    /// resolution miss and defers to the plain check.
    /// </summary>
    public ICheckDataStream? DataStream { get; init; }

    /// <summary>
    /// Sizes reservation expiry. Defaults to the wall clock.
    /// </summary>
    public LeaseClock? Clock { get; init; }

    /// <summary>
    /// Reports a flag_check event for a check this flow resolved itself. The
    /// plain check paths enqueue one per check, so without it a lease-gated
    /// check would be invisible to flag-check analytics and to company
    /// last-seen. Fallback exits do not call it: the plain check they defer to
    /// reports its own.
    /// </summary>
    public Action<EventBodyFlagCheck>? EmitFlagCheck { get; init; }

    /// <summary>
    /// Mints hold ids. Defaults to a UUID.
    /// </summary>
    public Func<string>? NewReservationId { get; init; }

    internal DateTime Now() => Clock == null ? DateTime.UtcNow : Clock();

    internal string NewId() => NewReservationId == null ? Guid.NewGuid().ToString() : NewReservationId();
}

/// <summary>
/// Gates one check against a local lease, returning a hold when it allows.
/// </summary>
public static class LeaseCheck
{
    /// <summary>
    /// The balance a fail-open evaluation substitutes for the metered credit:
    /// large enough that the credit gate always passes, and still exact as a
    /// JSON number, so the engine reads back what the SDK sent. It is
    /// JavaScript's MAX_SAFE_INTEGER, which the conformance vectors name
    /// "max_safe_integer".
    /// </summary>
    public const double FailOpenBalance = 9007199254740991;

    /// <summary>
    /// Drives a single credit-aware check in client mode.
    ///
    /// <para>The fallback is the plain flag check. Every step that cannot
    /// resolve a credit to meter defers to it, since the plain check has its own
    /// degradation story and issues no hold. A step that can resolve the credit
    /// but cannot gate on it goes through the caller's fail-open or fail-closed
    /// contract instead.</para>
    ///
    /// <para>One check runs the rules engine twice. The first run is a probe
    /// against the company's real balance that names the credit being metered;
    /// the second gates the call against the lease's local balance, after the
    /// credits have already been debited. conformance/SPEC.md explains why each
    /// step is ordered the way it is, and the vectors pin it.</para>
    /// </summary>
    public static async Task<CheckResult> CheckWithLeaseAsync(
        CheckDeps deps,
        string flagKey,
        Dictionary<string, string>? company,
        Dictionary<string, string>? user,
        CheckOptions options,
        Func<Task<CheckResult>> fallback
    )
    {
        var logger = deps.Logger;
        var failOpen = options.OnAcquireFailure == OnAcquireFailure.FailOpen;

        // A malformed usage must never reach the stores: NaN slips through every
        // numeric comparison, and a NaN balance on a possibly shared lease would
        // approve every later reserve. The caller asked for a contract covering
        // exactly this, so resolve it through that rather than letting it
        // surface as an opaque reserve failure.
        if (options.Usage == null || !LeaseQuantity.IsValid(options.Usage.Value))
        {
            logger.LogError(
                "Lease check: invalid usage {Usage} for flag {FlagKey}; must be a finite, non-negative number",
                options.Usage,
                flagKey
            );
            return Emit(
                deps,
                company,
                user,
                StaticFailureResult(failOpen, flagKey, "invalid_usage", null),
                default
            );
        }

        var usage = options.Usage.Value;

        // Nothing to reserve. The plain check still carries the preflight, so
        // every rule evaluates normally; a zero-credit hold would only be a
        // no-op.
        if (usage == 0)
        {
            logger.LogDebug(
                "Lease check: usage is 0 for flag {FlagKey}, nothing to reserve, using a plain check",
                flagKey
            );
            return await fallback().ConfigureAwait(false);
        }

        var datastream = deps.DataStream;
        if (datastream == null)
        {
            logger.LogDebug("Lease check: no datastream, using a plain check");
            return await fallback().ConfigureAwait(false);
        }

        RulesengineFlag? flag = null;
        try
        {
            flag = await datastream.GetFlagAsync(flagKey).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Lease check: failed to load flag {FlagKey}", flagKey);
        }
        if (flag == null)
        {
            logger.LogDebug(
                "Lease check: no cached flag for {FlagKey}, using a plain check",
                flagKey
            );
            return await fallback().ConfigureAwait(false);
        }

        if (company == null || company.Count == 0)
        {
            logger.LogDebug("Lease check: no company keys, using a plain check");
            return await fallback().ConfigureAwait(false);
        }

        // Resolve company and user the way a plain datastream check does: cache
        // first, then a live fetch. Evaluating without an entity the caller
        // named would silently skip its targeted rules and overrides, so a miss
        // defers to the plain check instead.
        RulesengineCompany? resolvedCompany = null;
        try
        {
            resolvedCompany = await datastream.GetCompanyAsync(company).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            logger.LogDebug(ex, "Lease check: company fetch failed, using a plain check");
        }
        if (resolvedCompany == null)
        {
            return await fallback().ConfigureAwait(false);
        }

        RulesengineUser? resolvedUser = null;
        if (user != null && user.Count > 0)
        {
            try
            {
                resolvedUser = await datastream.GetUserAsync(user).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger.LogDebug(ex, "Lease check: user fetch failed, using a plain check");
            }
            if (resolvedUser == null)
            {
                return await fallback().ConfigureAwait(false);
            }
        }

        // Entitlement-first resolution. The probe runs against the real balance
        // with no preflight: applying a credit cost to a lease-depleted server
        // balance could fail the credit condition, drop the engine to a
        // lower-priority rule, and hide the entitlement being looked for.
        CheckFlagResult? probe = null;
        try
        {
            probe = await datastream
                .EvaluateAsync(flag, resolvedCompany, resolvedUser, null)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            // A probe failure is a resolution miss, not the gate, and no hold
            // exists yet to cancel.
            logger.LogWarning(
                ex,
                "Lease check: entitlement probe failed for flag {FlagKey}, using a plain check",
                flagKey
            );
        }
        if (probe == null)
        {
            return await fallback().ConfigureAwait(false);
        }

        var entitlement = probe.Entitlement;
        if (entitlement == null || entitlement.ValueType != RulesengineEntitlementValueType.Credit)
        {
            // A boolean or override grant, a numeric allocation, unlimited, or
            // simply not entitled. The feature resolves without drawing a
            // credit, so skip the lease and the reserve round trip entirely.
            logger.LogDebug(
                "Lease check: flag {FlagKey} matched a non-credit entitlement (value_type={ValueType}), using a plain check, no reservation",
                flagKey,
                entitlement?.ValueType.Value ?? "<none>"
            );
            return await fallback().ConfigureAwait(false);
        }

        var creditId = entitlement.CreditId ?? string.Empty;
        var consumptionRate = entitlement.ConsumptionRate ?? 0;
        // The caller's subtype wins; otherwise the entitlement names the metered
        // event. A credit entitlement with neither a resolvable subtype nor a
        // positive rate can never be billed, so it is not gateable.
        var eventSubtype = string.IsNullOrEmpty(options.EventSubtype)
            ? entitlement.EventSubtype ?? string.Empty
            : options.EventSubtype!;
        if (creditId.Length == 0 || consumptionRate <= 0 || eventSubtype.Length == 0)
        {
            logger.LogDebug(
                "Lease check: flag {FlagKey} has an incomplete credit entitlement (credit_id={CreditId}, consumption_rate={ConsumptionRate}, event_subtype={EventSubtype}), using a plain check",
                flagKey,
                creditId,
                consumptionRate,
                eventSubtype
            );
            return await fallback().ConfigureAwait(false);
        }

        var creditCost = usage * consumptionRate;
        var companyId = resolvedCompany.Id;
        var ids = new FlagCheckIds { CompanyId = companyId, UserId = resolvedUser?.Id };

        // Thread the caller's per-check timeout to the lease wire calls the same
        // way the fallback path threads it to the plain check.
        var requestOptions =
            options.Timeout == null ? null : new RequestOptions { Timeout = options.Timeout };

        // Every can't-gate outcome funnels through here, so the fail-open and
        // fail-closed contract holds even when the backing infrastructure is
        // down.
        async Task<CheckResult> Failure(string reason)
        {
            var outcome = await HandleLeaseFailureAsync(
                    deps,
                    datastream!,
                    options,
                    failOpen,
                    flagKey,
                    reason,
                    flag!,
                    resolvedCompany!,
                    resolvedUser,
                    creditId
                )
                .ConfigureAwait(false);
            return Emit(deps, company, user, outcome, ids);
        }

        var lease = await deps
            .Manager.AcquireIfNeededAsync(companyId, creditId, requestOptions)
            .ConfigureAwait(false);
        if (lease == null)
        {
            return await Failure("lease_acquire_failed").ConfigureAwait(false);
        }

        // TryReserve is the atomic gate: check and debit in one step, returning
        // the post-debit balance so the pre-debit figure needs no second read,
        // and the lease it debited.
        ReserveResult? reserve;
        try
        {
            reserve = await deps
                .LeaseStore.TryReserveAsync(companyId, creditId, creditCost)
                .ConfigureAwait(false);
            if (reserve == null)
            {
                // Pass the cost as required credits so a single large request
                // extends even while the ratio still sits above the water mark.
                await deps
                    .Manager.MaybeExtendInBackgroundAsync(
                        companyId,
                        creditId,
                        creditCost,
                        requestOptions
                    )
                    .ConfigureAwait(false);
                reserve = await deps
                    .LeaseStore.TryReserveAsync(companyId, creditId, creditCost)
                    .ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Lease check: reserve against {CompanyId}/{CreditTypeId} failed",
                companyId,
                creditId
            );
            return await Failure("lease_store_error").ConfigureAwait(false);
        }
        if (reserve == null)
        {
            return await Failure("insufficient_lease_balance").ConfigureAwait(false);
        }

        // Record the hold after the debit and before the gate. A crash between
        // the debit and this add leaks at most this one hold, reclaimed when the
        // lease expires server-side; recording first would instead leave a
        // record with no debit, which a later consume would refund into a
        // double-spend.
        var resolvedConfig = deps.Manager.ResolveConfig(creditId);
        var record = new ReservationRecord
        {
            Id = deps.NewId(),
            // The lease the debit came out of, which the slot may have taken on
            // since the acquire above: the window between them spans the
            // extend's network call. A record pinned to the lease the acquire
            // returned would have its refunds dropped and would bill the wrong
            // lease.
            LeaseId = string.IsNullOrEmpty(reserve.Value.LeaseId)
                ? lease.LeaseId
                : reserve.Value.LeaseId,
            CompanyId = companyId,
            CreditTypeId = creditId,
            EventSubtype = eventSubtype,
            QuantityReserved = usage,
            CreditsReserved = creditCost,
            ConsumptionRate = consumptionRate,
            ExpiresAt = deps.Now() + resolvedConfig.ReservationTTL,
            Company = company,
            User = user,
        };

        try
        {
            await deps.Reservations.AddAsync(record).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Lease check: failed to persist reservation {ReservationId}",
                record.Id
            );
            await UndoDebitAsync(deps, record).ConfigureAwait(false);
            return await Failure("lease_store_error").ConfigureAwait(false);
        }

        // Gate against the lease's local view rather than the server's balance.
        // The substituted figure is the pre-reservation balance (what TryReserve
        // returned plus what it debited, exact as of the debit), and the credit
        // cost tells the engine what this call costs, so it evaluates the same
        // arithmetic TryReserve just enforced, plus every non-credit rule.
        var substituted = SubstituteCreditBalance(
            resolvedCompany,
            creditId,
            reserve.Value.Balance + creditCost
        );
        var gate = new PreflightRequestBody
        {
            CreditCost = new Dictionary<string, double> { [creditId] = creditCost },
        };

        CheckFlagResult? result = null;
        Exception? gateError = null;
        try
        {
            result = await datastream
                .EvaluateAsync(flag, substituted, resolvedUser, gate)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            gateError = ex;
        }
        if (result == null)
        {
            logger.LogError(
                gateError,
                "Lease check: rules evaluation failed for flag {FlagKey}",
                flagKey
            );
            // The engine itself is down, so there is no fail-open re-evaluation
            // to run: resolve the mode statically.
            await CancelReservationAsync(deps, record).ConfigureAwait(false);
            return Emit(
                deps,
                company,
                user,
                StaticFailureResult(
                    failOpen,
                    flagKey,
                    $"wasm_error: {gateError?.Message ?? "no result"}",
                    flag
                ),
                ids
            );
        }

        // Engine-evaluated exits report the engine's resolved ids, mirroring the
        // plain datastream path's flag_check event.
        ids = new FlagCheckIds
        {
            CompanyId = FirstString(result.CompanyId, companyId),
            UserId = FirstString(result.UserId, resolvedUser?.Id),
            RuleId = result.RuleId,
        };

        if (!result.Value)
        {
            await CancelReservationAsync(deps, record).ConfigureAwait(false);
            return Emit(
                deps,
                company,
                user,
                new CheckResult
                {
                    Reason = FallbackString(result.Reason, "denied_by_engine"),
                    FlagKey = FallbackString(result.FlagKey, flagKey),
                    FlagId = result.FlagId,
                    Entitlement = result.Entitlement,
                },
                ids
            );
        }

        // Allowed against the substituted balance, so the hold stands. Top the
        // lease up in the background now that it has been drawn down: the check
        // that drew it down should not pay for the top-up.
        _ = deps.Manager.MaybeExtendInBackgroundAsync(companyId, creditId);

        return Emit(
            deps,
            company,
            user,
            new CheckResult
            {
                Allowed = true,
                Value = true,
                Reservation = record,
                Reason = FallbackString(result.Reason, "lease_reserved"),
                FlagKey = FallbackString(result.FlagKey, flagKey),
                FlagId = result.FlagId,
                Entitlement = result.Entitlement,
            },
            ids
        );
    }

    /// <summary>
    /// Resolves a check that could not gate: acquire failed, store unreachable,
    /// or the lease is exhausted.
    ///
    /// <para>Fail-closed denies. Fail-open means assume the credits are there,
    /// not skip the evaluation: the rules still run with the balance substituted
    /// to an effectively unlimited value, so plan targeting, overrides, and every
    /// non-credit condition still apply, and a company that is not entitled stays
    /// denied with the lease backend down. Only an error in that evaluation drops
    /// to a blanket allow.</para>
    /// </summary>
    private static async Task<CheckResult> HandleLeaseFailureAsync(
        CheckDeps deps,
        ICheckDataStream datastream,
        CheckOptions options,
        bool failOpen,
        string flagKey,
        string reason,
        RulesengineFlag flag,
        RulesengineCompany company,
        RulesengineUser? user,
        string creditId
    )
    {
        if (!failOpen)
        {
            return StaticFailureResult(false, flagKey, reason, flag);
        }

        try
        {
            var substituted = SubstituteCreditBalance(company, creditId, FailOpenBalance);
            var result = await datastream
                .EvaluateAsync(flag, substituted, user, LeasePreflight.Build(options))
                .ConfigureAwait(false);
            if (result == null)
            {
                return StaticFailureResult(true, flagKey, reason, flag);
            }
            return new CheckResult
            {
                Allowed = result.Value,
                Value = result.Value,
                Reason = $"{FallbackString(result.Reason, "evaluated")} ({reason}_fail_open)",
                FlagKey = FallbackString(result.FlagKey, flagKey),
                FlagId = FirstString(result.FlagId, flag.Id),
                Entitlement = result.Entitlement,
                Error = reason,
            };
        }
        catch (Exception ex)
        {
            deps.Logger.LogWarning(ex, "Lease check: the fail-open evaluation failed; allowing");
            return StaticFailureResult(true, flagKey, reason, flag);
        }
    }

    /// <summary>
    /// Resolves a mode with no evaluation behind it: deny for fail-closed,
    /// blanket allow for fail-open. Used when the engine is the thing that
    /// failed, and when the fail-open evaluation itself errors.
    /// </summary>
    private static CheckResult StaticFailureResult(
        bool failOpen,
        string flagKey,
        string reason,
        RulesengineFlag? flag
    ) =>
        new()
        {
            Allowed = failOpen,
            Value = failOpen,
            Reason = failOpen ? reason + "_fail_open" : reason,
            FlagKey = flagKey,
            FlagId = flag?.Id,
            Error = reason,
        };

    /// <summary>
    /// Returns a debit whose reservation record never landed, rather than
    /// stranding it until lease expiry. Consume claims whatever slice of the add
    /// made it to the store and refunds it; nothing claimed means nothing
    /// landed, so the debit is refunded directly. Both are pinned to the lease
    /// the debit came out of. If the undo itself fails, accept the bounded leak:
    /// the slice comes back at lease expiry, which beats risking a double refund.
    /// </summary>
    private static async Task UndoDebitAsync(CheckDeps deps, ReservationRecord record)
    {
        try
        {
            var claimed = await deps.Reservations.ConsumeAsync(record.Id, 0).ConfigureAwait(false);
            if (claimed == null)
            {
                await deps
                    .LeaseStore.RefundAsync(
                        record.CompanyId,
                        record.CreditTypeId,
                        record.CreditsReserved,
                        record.LeaseId
                    )
                    .ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {
            deps.Logger.LogWarning(
                ex,
                "Lease check: could not undo the local debit for {ReservationId}; the slice is reclaimed at lease expiry",
                record.Id
            );
        }
    }

    /// <summary>
    /// Claims the hold and refunds all of it. Best effort: a failure leaves the
    /// hold for the sweeper or for lease expiry.
    /// </summary>
    private static async Task CancelReservationAsync(CheckDeps deps, ReservationRecord record)
    {
        try
        {
            await deps.Reservations.ConsumeAsync(record.Id, 0).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            deps.Logger.LogWarning(
                ex,
                "Lease check: failed to cancel reservation {ReservationId}; its hold is reclaimed by the sweeper or at lease expiry",
                record.Id
            );
        }
    }

    /// <summary>
    /// Copies the company with one credit balance replaced, leaving the cached
    /// entity untouched.
    /// </summary>
    private static RulesengineCompany SubstituteCreditBalance(
        RulesengineCompany company,
        string creditId,
        double balance
    )
    {
        var balances = new Dictionary<string, double>(company.CreditBalances)
        {
            [creditId] = balance,
        };
        return company with { CreditBalances = balances };
    }

    /// <summary>
    /// Reports a lease-path resolution and passes the outcome straight through.
    /// Analytics must never change a verdict the caller is already acting on, so
    /// this only ever adds an event.
    /// </summary>
    private static CheckResult Emit(
        CheckDeps deps,
        Dictionary<string, string>? company,
        Dictionary<string, string>? user,
        CheckResult outcome,
        FlagCheckIds ids
    )
    {
        deps.EmitFlagCheck?.Invoke(
            new EventBodyFlagCheck
            {
                FlagKey = outcome.FlagKey,
                Value = outcome.Value,
                Reason = outcome.Reason,
                Error = outcome.Error,
                FlagId = outcome.FlagId,
                CompanyId = ids.CompanyId,
                UserId = ids.UserId,
                RuleId = ids.RuleId,
                ReqCompany = company,
                ReqUser = user,
            }
        );
        return outcome;
    }

    private static string FallbackString(string? value, string fallback) =>
        string.IsNullOrEmpty(value) ? fallback : value!;

    private static string? FirstString(string? value, string? fallback) =>
        string.IsNullOrEmpty(value) ? fallback : value;

    /// <summary>
    /// The resolved entity ids a flag_check event carries.
    /// </summary>
    private readonly struct FlagCheckIds
    {
        public string? CompanyId { get; init; }

        public string? UserId { get; init; }

        public string? RuleId { get; init; }
    }
}
