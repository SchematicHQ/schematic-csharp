using System.Text.Json;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using SchematicHQ.Client.Leases;
using SchematicHQ.Client.RulesEngine;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// The flow half of the conformance runner: the vectors' check and track
/// operations, driven against the real check and settle flows with a scripted
/// rules engine standing in for the WASM. The engine is an oracle here, as
/// conformance/SPEC.md says: the vectors pin the orchestration around it, not
/// the engine, which is shared across the SDKs and has its own tests.
/// </summary>
public sealed partial class ConformanceRunner
{
    private readonly Dictionary<string, ReservationRecord> _records = new();

    private async Task ExecuteCheckAsync(JsonElement op)
    {
        var flagKey = StrOpt(op, "flag_key") ?? "flag";
        var companySpec = op.TryGetProperty("company", out var raw) ? raw : default;
        var companyId = companySpec.ValueKind == JsonValueKind.Object
            ? StrOpt(companySpec, "id") ?? "co_1"
            : "co_1";

        ScriptFlowServer(op);

        var datastream = new ScriptedCheckDataStream(
            flagKey,
            companyId,
            CreditBalances(companySpec),
            EngineResults(op)
        );

        var fellBack = false;
        Task<CheckResult> Fallback()
        {
            fellBack = true;
            return Task.FromResult(
                new CheckResult
                {
                    Allowed = true,
                    Value = true,
                    Reason = "fallback",
                    FlagKey = flagKey,
                }
            );
        }

        var result = await LeaseCheck
            .CheckWithLeaseAsync(
                new CheckDeps
                {
                    LeaseStore = _leases,
                    Reservations = _reservations,
                    Manager = _manager,
                    DataStream = datastream,
                    Logger = NullLogger.Instance,
                    Clock = () => _now,
                },
                flagKey,
                new Dictionary<string, string> { ["id"] = companyId },
                null,
                new CheckOptions
                {
                    Usage = NumOpt(op, "usage"),
                    EventSubtype = StrOpt(op, "event_subtype"),
                    OnAcquireFailure = StrOpt(op, "on_acquire_failure") == "fail-open"
                        ? OnAcquireFailure.FailOpen
                        : OnAcquireFailure.FailClosed,
                },
                Fallback
            )
            .ConfigureAwait(false);

        // The low-water-mark refresh a successful check fires is unawaited, so
        // settle the manager's background work before reading the wire counts.
        await _manager.DrainAsync(TimeSpan.FromSeconds(5)).ConfigureAwait(false);

        AssertCheckOutcome(op, result, fellBack);
        AssertEngineCalls(op, datastream.Calls, companySpec);

        if (Expect(op) is { } expect)
        {
            if (expect.TryGetProperty("wire_extends", out var extends))
            {
                Assert.That(_wire.ExtendCount, Is.EqualTo(extends.GetInt32()));
            }
            if (expect.TryGetProperty("last_extend_additional_amount", out var additional))
            {
                Assert.That(
                    _wire.LastExtendAdditionalAmount,
                    Is.EqualTo(additional.GetDouble()).Within(Tolerance)
                );
            }
        }

        if (StrOpt(op, "save_reservation_as") is { } handle && result.Reservation != null)
        {
            _records[handle] = result.Reservation;
            _handles[handle] = result.Reservation.Id;
        }
    }

    private async Task ExecuteTrackAsync(JsonElement op)
    {
        var handle = Str(op, "handle");
        Assert.That(
            _records.ContainsKey(handle),
            Is.True,
            $"no reservation was saved under the handle '{handle}'"
        );

        var outcome = await ReservationTrack
            .ConsumeAndBuildEventAsync(_reservations, _records[handle], Num(op, "actual_quantity"))
            .ConfigureAwait(false);

        if (Expect(op) is not { } expect)
        {
            return;
        }
        if (expect.TryGetProperty("settled_locally", out var settled))
        {
            Assert.That(outcome.SettledLocally, Is.EqualTo(settled.GetBoolean()));
        }
        if (!expect.TryGetProperty("track", out var track))
        {
            return;
        }
        if (track.TryGetProperty("event", out var eventName))
        {
            Assert.That(outcome.Track.Event, Is.EqualTo(eventName.GetString()));
        }
        if (track.TryGetProperty("quantity", out var quantity))
        {
            Assert.That(outcome.Track.Quantity, Is.EqualTo(quantity.GetInt64()));
        }
        if (track.TryGetProperty("lease_id", out var leaseId))
        {
            Assert.That(outcome.Track.LeaseId, Is.EqualTo(leaseId.GetString()));
        }
    }

    /// <summary>
    /// A check operation's server block names the wire call it scripts rather
    /// than scripting one directly the way a manager operation does.
    /// </summary>
    private void ScriptFlowServer(JsonElement op)
    {
        if (!op.TryGetProperty("server", out var server))
        {
            return;
        }
        if (server.TryGetProperty("acquire", out var acquire))
        {
            ScriptServer(Wrap(acquire), acquire: true);
        }
        if (server.TryGetProperty("extend", out var extend))
        {
            ScriptServer(Wrap(extend), acquire: false);
        }
    }

    /// <summary>
    /// Re-parses a nested script as the shape
    /// <see cref="ScriptServer(JsonElement, bool)"/> reads: it takes the whole
    /// operation and looks for a server property on it.
    /// </summary>
    private static JsonElement Wrap(JsonElement script) =>
        JsonDocument.Parse($"{{\"server\":{script.GetRawText()}}}").RootElement;

    private void AssertCheckOutcome(JsonElement op, CheckResult result, bool fellBack)
    {
        if (Expect(op) is not { } expect)
        {
            return;
        }
        if (expect.TryGetProperty("allowed", out var allowed))
        {
            Assert.That(result.Allowed, Is.EqualTo(allowed.GetBoolean()));
        }
        if (expect.TryGetProperty("reason", out var reason))
        {
            Assert.That(result.Reason, Is.EqualTo(reason.GetString()));
        }
        if (expect.TryGetProperty("err", out var err))
        {
            Assert.That(result.Error, Is.EqualTo(err.GetString()));
        }
        if (expect.TryGetProperty("has_reservation", out var hasReservation))
        {
            Assert.That(result.Reservation != null, Is.EqualTo(hasReservation.GetBoolean()));
        }
        if (expect.TryGetProperty("fallback_called", out var fallbackCalled))
        {
            Assert.That(fellBack, Is.EqualTo(fallbackCalled.GetBoolean()));
        }
        if (!expect.TryGetProperty("reservation", out var want))
        {
            return;
        }

        Assert.That(result.Reservation, Is.Not.Null);
        var record = result.Reservation!;
        if (want.TryGetProperty("lease_id", out var leaseId))
        {
            Assert.That(record.LeaseId, Is.EqualTo(leaseId.GetString()));
        }
        if (want.TryGetProperty("credit_type_id", out var creditTypeId))
        {
            Assert.That(record.CreditTypeId, Is.EqualTo(creditTypeId.GetString()));
        }
        if (want.TryGetProperty("event_subtype", out var eventSubtype))
        {
            Assert.That(record.EventSubtype, Is.EqualTo(eventSubtype.GetString()));
        }
        if (want.TryGetProperty("quantity_reserved", out var quantityReserved))
        {
            Assert.That(
                record.QuantityReserved,
                Is.EqualTo(quantityReserved.GetDouble()).Within(Tolerance)
            );
        }
        if (want.TryGetProperty("credits_reserved", out var creditsReserved))
        {
            Assert.That(
                record.CreditsReserved,
                Is.EqualTo(creditsReserved.GetDouble()).Within(Tolerance)
            );
        }
        if (want.TryGetProperty("consumption_rate", out var consumptionRate))
        {
            Assert.That(
                record.ConsumptionRate,
                Is.EqualTo(consumptionRate.GetDouble()).Within(Tolerance)
            );
        }
    }

    private void AssertEngineCalls(JsonElement op, List<EngineCall> calls, JsonElement companySpec)
    {
        if (Expect(op) is not { } expect || !expect.TryGetProperty("engine_calls", out var want))
        {
            return;
        }

        var expected = want.EnumerateArray().ToArray();
        Assert.That(calls.Count, Is.EqualTo(expected.Length), "engine call count");

        var creditId = CreditIdFromVector(op, companySpec);
        for (var i = 0; i < expected.Length; i++)
        {
            var call = calls[i];
            if (expected[i].TryGetProperty("credit_balance", out var balance))
            {
                Assert.That(
                    creditId,
                    Is.Not.Empty,
                    "engine_calls needs a credit id to assert a balance against"
                );
                call.Balances.TryGetValue(creditId, out var actual);
                Assert.That(actual, Is.EqualTo(ExpectedBalance(balance)).Within(Tolerance));
            }
            if (expected[i].TryGetProperty("credit_cost", out var cost))
            {
                Assert.That(call.Preflight, Is.Not.Null);
                Assert.That(call.Preflight!.CreditCost, Is.Not.Null);
                call.Preflight.CreditCost!.TryGetValue(creditId, out var actualCost);
                Assert.That(actualCost, Is.EqualTo(cost.GetDouble()).Within(Tolerance));
            }
            if (expected[i].TryGetProperty("event_usage", out var eventUsage))
            {
                Assert.That(call.Preflight?.EventUsage, Is.Not.Null);
                Assert.That(
                    call.Preflight!.EventUsage!.EventSubtype,
                    Is.EqualTo(eventUsage.GetProperty("event_subtype").GetString())
                );
                Assert.That(
                    call.Preflight.EventUsage.Quantity,
                    Is.EqualTo(eventUsage.GetProperty("quantity").GetInt64())
                );
            }
            if (expected[i].TryGetProperty("usage", out var usage))
            {
                Assert.That(call.Preflight?.Usage, Is.EqualTo(usage.GetInt64()));
            }
        }
    }

    /// <summary>
    /// A balance expectation, which the vectors write as a number or as the name
    /// of the fail-open substitution.
    /// </summary>
    private static double ExpectedBalance(JsonElement raw) =>
        raw.ValueKind == JsonValueKind.String ? LeaseCheck.FailOpenBalance : raw.GetDouble();

    /// <summary>
    /// Names the credit a vector's balance and cost expectations are about: the
    /// one the scripted entitlement meters, or the company's only balance when no
    /// entitlement names one.
    /// </summary>
    private static string CreditIdFromVector(JsonElement op, JsonElement companySpec)
    {
        if (op.TryGetProperty("engine", out var engine))
        {
            foreach (var result in engine.EnumerateArray())
            {
                if (
                    result.TryGetProperty("entitlement", out var entitlement)
                    && StrOpt(entitlement, "credit_id") is { } creditId
                )
                {
                    return creditId;
                }
            }
        }
        foreach (var balance in CreditBalances(companySpec))
        {
            return balance.Key;
        }
        return string.Empty;
    }

    private static Dictionary<string, double> CreditBalances(JsonElement companySpec)
    {
        var balances = new Dictionary<string, double>();
        if (
            companySpec.ValueKind != JsonValueKind.Object
            || !companySpec.TryGetProperty("credit_balances", out var raw)
        )
        {
            return balances;
        }
        foreach (var entry in raw.EnumerateObject())
        {
            balances[entry.Name] = entry.Value.GetDouble();
        }
        return balances;
    }

    private static Queue<JsonElement> EngineResults(JsonElement op)
    {
        var results = new Queue<JsonElement>();
        if (!op.TryGetProperty("engine", out var engine))
        {
            return results;
        }
        foreach (var result in engine.EnumerateArray())
        {
            results.Enqueue(result);
        }
        return results;
    }

    /// <summary>
    /// One evaluation the flow asked for, kept so a vector can assert on what
    /// the flow put in front of the engine.
    /// </summary>
    private sealed record EngineCall(
        Dictionary<string, double> Balances,
        PreflightRequestBody? Preflight
    );

    /// <summary>
    /// Serves one flag and one company from cache and answers each evaluation
    /// with the next scripted result, in call order.
    /// </summary>
    private sealed class ScriptedCheckDataStream : ICheckDataStream
    {
        private readonly string _flagKey;
        private readonly RulesengineFlag _flag;
        private readonly RulesengineCompany _company;
        private readonly Queue<JsonElement> _results;

        public ScriptedCheckDataStream(
            string flagKey,
            string companyId,
            Dictionary<string, double> balances,
            Queue<JsonElement> results
        )
        {
            _flagKey = flagKey;
            _results = results;
            _flag = new RulesengineFlag
            {
                Id = "flag_1",
                Key = flagKey,
                AccountId = "acct_1",
                EnvironmentId = "env_1",
                DefaultValue = false,
            };
            _company = new RulesengineCompany
            {
                Id = companyId,
                AccountId = "acct_1",
                EnvironmentId = "env_1",
                CreditBalances = balances,
            };
        }

        public List<EngineCall> Calls { get; } = new();

        public Task<RulesengineFlag?> GetFlagAsync(string flagKey) =>
            Task.FromResult<RulesengineFlag?>(_flag);

        public Task<RulesengineCompany?> GetCompanyAsync(Dictionary<string, string> keys) =>
            Task.FromResult<RulesengineCompany?>(_company);

        public Task<RulesengineUser?> GetUserAsync(Dictionary<string, string> keys) =>
            Task.FromResult<RulesengineUser?>(null);

        public Task<CheckFlagResult> EvaluateAsync(
            RulesengineFlag flag,
            RulesengineCompany company,
            RulesengineUser? user,
            PreflightRequestBody? preflight
        )
        {
            Calls.Add(
                new EngineCall(new Dictionary<string, double>(company.CreditBalances), preflight)
            );
            if (_results.Count == 0)
            {
                throw new InvalidOperationException(
                    $"unscripted engine call in a check op for flag {_flagKey}"
                );
            }

            var scripted = _results.Dequeue();
            return Task.FromResult(
                new CheckFlagResult
                {
                    Value = scripted.GetProperty("value").GetBoolean(),
                    Reason = StrOpt(scripted, "reason") ?? string.Empty,
                    FlagKey = _flagKey,
                    FlagId = "flag_1",
                    Entitlement = EntitlementFromSpec(scripted, _flagKey),
                }
            );
        }

        private static RulesengineFeatureEntitlement? EntitlementFromSpec(
            JsonElement scripted,
            string flagKey
        )
        {
            if (!scripted.TryGetProperty("entitlement", out var spec))
            {
                return null;
            }
            return new RulesengineFeatureEntitlement
            {
                FeatureId = StrOpt(spec, "feature_id") ?? "feat_1",
                FeatureKey = StrOpt(spec, "feature_key") ?? flagKey,
                ValueType = new RulesengineEntitlementValueType(Str(spec, "value_type")),
                CreditId = StrOpt(spec, "credit_id"),
                ConsumptionRate = NumOpt(spec, "consumption_rate"),
                EventSubtype = StrOpt(spec, "event_subtype"),
            };
        }
    }
}
