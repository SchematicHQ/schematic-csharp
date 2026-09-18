using SchematicHQ.Client.Leases;
using SchematicHQ.Client.RulesEngine;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// Serves one flag and one company from cache and answers each evaluation with
/// the next scripted result, in call order. The conformance runner has its own
/// vector-driven version; this one is for the tests that build a case in code.
/// </summary>
public sealed class StubCheckDataStream : ICheckDataStream
{
    private readonly Queue<CheckFlagResult> _results = new();

    public StubCheckDataStream(string flagKey, RulesengineCompany company)
    {
        FlagKey = flagKey;
        Company = company;
        Flag = new RulesengineFlag
        {
            Id = "flag_1",
            Key = flagKey,
            AccountId = "acct_1",
            EnvironmentId = "env_1",
            DefaultValue = false,
        };
    }

    public string FlagKey { get; }

    public RulesengineFlag Flag { get; }

    public RulesengineCompany Company { get; }

    public List<PreflightRequestBody?> Preflights { get; } = new();

    /// <summary>
    /// Queues the entitlement probe's answer: a credit entitlement naming the
    /// credit, its rate and the metered event.
    /// </summary>
    public StubCheckDataStream ProbesCredit(
        string creditId,
        double consumptionRate,
        string eventSubtype
    )
    {
        _results.Enqueue(
            new CheckFlagResult
            {
                Value = true,
                Reason = "probe",
                FlagKey = FlagKey,
                FlagId = "flag_1",
                Entitlement = new RulesengineFeatureEntitlement
                {
                    FeatureId = "feat_1",
                    FeatureKey = FlagKey,
                    ValueType = RulesengineEntitlementValueType.Credit,
                    CreditId = creditId,
                    ConsumptionRate = consumptionRate,
                    EventSubtype = eventSubtype,
                },
            }
        );
        return this;
    }

    /// <summary>
    /// Sets the flag's default, which is what the datastream substitutes when
    /// the engine cannot answer.
    /// </summary>
    public StubCheckDataStream WithFlagDefault(bool value)
    {
        Flag.DefaultValue = value;
        return this;
    }

    /// <summary>
    /// Queues the answer the datastream gives when the engine faults. It does
    /// not throw and it does not return null: it hands back the flag's default
    /// under an engine reason, which is what a credit gate has to recognise as
    /// a failure rather than a verdict.
    /// </summary>
    public StubCheckDataStream FailsInTheEngine(string reason, Exception? error = null)
    {
        _results.Enqueue(
            new CheckFlagResult
            {
                Value = Flag.DefaultValue,
                Reason = reason,
                FlagKey = FlagKey,
                FlagId = "flag_1",
                Error = error,
            }
        );
        return this;
    }

    public StubCheckDataStream Answers(bool value, string reason)
    {
        _results.Enqueue(
            new CheckFlagResult
            {
                Value = value,
                Reason = reason,
                FlagKey = FlagKey,
                FlagId = "flag_1",
            }
        );
        return this;
    }

    public Task<RulesengineFlag?> GetFlagAsync(string flagKey) =>
        Task.FromResult<RulesengineFlag?>(Flag);

    public Task<RulesengineCompany?> GetCompanyAsync(Dictionary<string, string> keys) =>
        Task.FromResult<RulesengineCompany?>(Company);

    public Task<RulesengineUser?> GetUserAsync(Dictionary<string, string> keys) =>
        Task.FromResult<RulesengineUser?>(null);

    public Task<CheckFlagResult> EvaluateAsync(
        RulesengineFlag flag,
        RulesengineCompany company,
        RulesengineUser? user,
        PreflightRequestBody? preflight
    )
    {
        Preflights.Add(preflight);
        if (_results.Count == 0)
        {
            throw new InvalidOperationException("unscripted engine call");
        }
        return Task.FromResult(_results.Dequeue());
    }
}
