using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using SchematicHQ.Client.Leases;
using SchematicHQ.Client.RulesEngine;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// The credit gate against the real WASM engine.
///
/// <para>The conformance vectors script the engine, so the contract that
/// matters most (resolving the matched credit entitlement from the probe,
/// substituting the lease balance into the company's credit balances, and
/// letting the engine's credit_cost gate decide) is never exercised against the
/// real thing there. These tests load the bundled WASM and drive a
/// credit-balance flag end to end, so a drift in the snake_case option envelope
/// fails here rather than mis-gating in production.</para>
/// </summary>
[TestFixture]
public class WasmCreditGateTests
{
    private const string CreditId = "credit-1";
    private const string EventSubtype = "inference_tokens";
    private const string FlagKey = "infer";

    private WasmRulesEngine _engine = null!;

    [OneTimeSetUp]
    public void SetUpEngine()
    {
        _engine = new WasmRulesEngine(NullLogger.Instance);
        _engine.Initialize();
    }

    [OneTimeTearDown]
    public void TearDownEngine() => _engine.Dispose();

    [Test]
    public async Task Substitutes_The_Lease_Balance_And_Lets_A_Within_Balance_Usage_Through()
    {
        var harness = new Harness(_engine, CreditFlag(), Company(100), grantedAmount: 10_000);

        var result = await harness.CheckAsync(usage: 50);

        Assert.That(result.Allowed, Is.True);
        Assert.That(result.Reservation, Is.Not.Null);
        Assert.That(result.Reservation!.CreditTypeId, Is.EqualTo(CreditId));
        Assert.That(result.Reservation.CreditsReserved, Is.EqualTo(50));
        var lease = await harness.Leases.GetAsync("co", CreditId);
        Assert.That(lease!.LocalRemainingCredits, Is.EqualTo(9_950));
        Assert.That(await harness.Reservations.CountAsync(), Is.EqualTo(1));
    }

    [Test]
    public async Task A_Fractional_Usage_Holds_The_Exact_Cost_And_Bills_A_Whole_Unit()
    {
        var harness = new Harness(_engine, CreditFlag(), Company(100), grantedAmount: 10_000);

        var result = await harness.CheckAsync(usage: 2.5);

        // The hold is sized from the unrounded usage times the rate, so no
        // credit is over-held and the lease's arithmetic stays exact.
        Assert.That(result.Reservation!.QuantityReserved, Is.EqualTo(2.5));
        Assert.That(result.Reservation.CreditsReserved, Is.EqualTo(2.5));
        var lease = await harness.Leases.GetAsync("co", CreditId);
        Assert.That(lease!.LocalRemainingCredits, Is.EqualTo(9_997.5));

        // A track event's quantity is an integer, so a partial unit bills as a
        // whole one rather than as none. The preflight rounds the same way, so
        // the gate never passes on less usage than the caller will record.
        Assert.That(ReservationTrack.SettleQuantity(2.5), Is.EqualTo(3));
        Assert.That(LeasePreflight.PreflightQuantity(2.5), Is.EqualTo(3));
    }

    [Test]
    public async Task Denies_And_Refunds_When_The_Rule_Fails_For_A_Non_Credit_Reason()
    {
        // The balance is plentiful, but the company-membership condition
        // excludes this company, so the engine denies the rule. The hold taken
        // before the evaluation must go back to the lease.
        var flag = CreditFlag(CompanyCondition("some-other-company"));
        var harness = new Harness(_engine, flag, Company(10_000), grantedAmount: 10_000);

        var result = await harness.CheckAsync(usage: 50);

        Assert.That(result.Allowed, Is.False);
        Assert.That(result.Reservation, Is.Null);
        var lease = await harness.Leases.GetAsync("co", CreditId);
        Assert.That(lease!.LocalRemainingCredits, Is.EqualTo(10_000));
        Assert.That(await harness.Reservations.CountAsync(), Is.EqualTo(0));
    }

    [Test]
    public async Task Skips_The_Lease_When_The_Matched_Entitlement_Is_An_Override()
    {
        // A company override grants the feature outright, so the company's
        // effective entitlement is no longer credit-metered. The probe surfaces
        // the boolean entitlement, so the flow never acquires or reserves: no
        // reserve-then-cancel, and no credits billed for usage the override
        // grants for free.
        var flag = CreditFlag();
        var rules = flag.Rules.ToList();
        rules.Insert(
            0,
            new RulesengineRule
            {
                Id = "rule-override",
                AccountId = "acct",
                EnvironmentId = "env",
                Name = "Override",
                RuleType = RulesengineRuleType.CompanyOverride,
                Priority = 1,
                Value = true,
                Conditions = new List<RulesengineCondition> { CompanyCondition("co") },
                ConditionGroups = new List<RulesengineConditionGroup>(),
            }
        );
        flag.Rules = rules;

        var company = Company(100);
        company.Entitlements = new List<RulesengineFeatureEntitlement>
        {
            new()
            {
                FeatureId = "feat-infer",
                FeatureKey = FlagKey,
                ValueType = RulesengineEntitlementValueType.Boolean,
            },
        };

        var harness = new Harness(_engine, flag, company, grantedAmount: 10_000);
        var result = await harness.CheckAsync(usage: 50);

        Assert.That(harness.FellBack, Is.True);
        Assert.That(result.Allowed, Is.True);
        Assert.That(result.Reservation, Is.Null);
        Assert.That(await harness.Leases.GetAsync("co", CreditId), Is.Null);
        Assert.That(await harness.Reservations.CountAsync(), Is.EqualTo(0));
    }

    [Test]
    public void Serializes_The_Preflight_To_The_Envelope_The_Engine_Gates_On()
    {
        // A direct engine check: the SDK's preflight reaches the WASM as
        // event_usage and gates exactly at the balance boundary. This is the
        // contract the lease flow leans on.
        var flag = CreditFlag();
        var company = Company(100);

        var under = _engine.CheckFlag(company, null, flag, Preflight(50));
        var over = _engine.CheckFlag(company, null, flag, Preflight(150));

        Assert.That(under.Value, Is.True);
        Assert.That(over.Value, Is.False);
    }

    private static PreflightRequestBody Preflight(long quantity) =>
        new()
        {
            EventUsage = new PreflightEventUsageRequestBody
            {
                EventSubtype = EventSubtype,
                Quantity = quantity,
            },
        };

    /// <summary>
    /// A consumption rate of 1 keeps credits equal to quantity, so the
    /// arithmetic under test stays readable.
    /// </summary>
    private static RulesengineCondition CreditCondition() =>
        new()
        {
            Id = "cond-credit",
            AccountId = "acct",
            EnvironmentId = "env",
            ConditionType = RulesengineConditionType.Credit,
            Operator = ComparableOperator.Lt,
            ResourceIds = new List<string>(),
            TraitValue = string.Empty,
            MetricValue = 0,
            CreditId = CreditId,
            ConsumptionRate = 1,
            EventSubtype = EventSubtype,
        };

    /// <summary>
    /// A membership condition a test can flip to make the engine deny the rule
    /// for reasons unrelated to the credit balance.
    /// </summary>
    private static RulesengineCondition CompanyCondition(string resourceId) =>
        new()
        {
            Id = "cond-company",
            AccountId = "acct",
            EnvironmentId = "env",
            ConditionType = RulesengineConditionType.Company,
            Operator = ComparableOperator.Eq,
            ResourceIds = new List<string> { resourceId },
            TraitValue = string.Empty,
            MetricValue = 0,
        };

    private static RulesengineFlag CreditFlag(params RulesengineCondition[] extra)
    {
        var conditions = new List<RulesengineCondition> { CreditCondition() };
        conditions.AddRange(extra);
        return new RulesengineFlag
        {
            Id = "flag-infer",
            AccountId = "acct",
            EnvironmentId = "env",
            Key = FlagKey,
            DefaultValue = false,
            Rules = new List<RulesengineRule>
            {
                new()
                {
                    Id = "rule-credit",
                    AccountId = "acct",
                    EnvironmentId = "env",
                    Name = "Credit",
                    RuleType = RulesengineRuleType.PlanEntitlement,
                    Priority = 100,
                    Value = true,
                    Conditions = conditions,
                    ConditionGroups = new List<RulesengineConditionGroup>(),
                },
            },
        };
    }

    /// <summary>
    /// The company carries its resolved credit entitlement for the feature, the
    /// shape the datastream cache holds after a plan assignment.
    /// Entitlement-first resolution reads the credit id, rate and subtype off it
    /// through the probe.
    /// </summary>
    private static RulesengineCompany Company(double creditBalance) =>
        new()
        {
            Id = "co",
            AccountId = "acct",
            EnvironmentId = "env",
            CreditBalances = new Dictionary<string, double> { [CreditId] = creditBalance },
            Entitlements = new List<RulesengineFeatureEntitlement>
            {
                new()
                {
                    FeatureId = "feat-infer",
                    FeatureKey = FlagKey,
                    ValueType = RulesengineEntitlementValueType.Credit,
                    CreditId = CreditId,
                    ConsumptionRate = 1,
                    EventSubtype = EventSubtype,
                    CreditTotal = creditBalance,
                    CreditUsed = 0,
                    CreditRemaining = creditBalance,
                },
            },
        };

    /// <summary>
    /// The stores, the manager and a datastream that serves one flag and one
    /// company from cache and hands evaluations to the real engine.
    /// </summary>
    private sealed class Harness
    {
        private readonly CheckDeps _deps;

        public Harness(
            WasmRulesEngine engine,
            RulesengineFlag flag,
            RulesengineCompany company,
            double grantedAmount
        )
        {
            Leases = new InMemoryLeaseStore();
            Reservations = new InMemoryReservationStore(Leases);
            var wire = new ScriptedLeaseWireClient
            {
                NextAcquire = new LeaseGrant
                {
                    LeaseId = "lse-1",
                    GrantedAmount = grantedAmount,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(1),
                },
            };
            _deps = new CheckDeps
            {
                LeaseStore = Leases,
                Reservations = Reservations,
                Manager = new CreditLeaseManager(
                    wire,
                    Leases,
                    new CreditLeaseConfig
                    {
                        DefaultLeaseSize = grantedAmount,
                        DefaultLeaseDuration = TimeSpan.FromMinutes(1),
                    },
                    NullLogger.Instance
                ),
                DataStream = new EngineDataStream(engine, flag, company),
                Logger = NullLogger.Instance,
            };
        }

        public InMemoryLeaseStore Leases { get; }

        public InMemoryReservationStore Reservations { get; }

        public bool FellBack { get; private set; }

        public Task<CheckResult> CheckAsync(double usage) =>
            LeaseCheck.CheckWithLeaseAsync(
                _deps,
                FlagKey,
                new Dictionary<string, string> { ["id"] = "co" },
                null,
                new CheckOptions { Usage = usage, EventSubtype = EventSubtype },
                () =>
                {
                    FellBack = true;
                    return Task.FromResult(
                        new CheckResult
                        {
                            Allowed = true,
                            Value = true,
                            Reason = "override",
                            FlagKey = FlagKey,
                        }
                    );
                }
            );
    }

    private sealed class EngineDataStream : ICheckDataStream
    {
        private readonly WasmRulesEngine _engine;
        private readonly RulesengineFlag _flag;
        private readonly RulesengineCompany _company;

        public EngineDataStream(
            WasmRulesEngine engine,
            RulesengineFlag flag,
            RulesengineCompany company
        )
        {
            _engine = engine;
            _flag = flag;
            _company = company;
        }

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
        ) => Task.FromResult(_engine.CheckFlag(company, user, flag, preflight));
    }
}
