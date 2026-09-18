using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using SchematicHQ.Client.Leases;
using SchematicHQ.Client.RulesEngine;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// What a credit gate does when the rules engine faults. The datastream
/// swallows the fault and answers with the flag's default value, which is the
/// right answer for a plain check and no answer at all for a gated one: a
/// default of true would allow without the credits behind it, and a default of
/// false would deny a caller who asked to fail open. The gate has to read
/// those answers as failures and resolve by mode.
/// </summary>
[TestFixture]
public class LeaseCheckEngineFailureTests
{
    private const string CreditId = "ct_1";
    private const string FlagKey = "inference";
    private const string EngineError = "RULES_ENGINE_ERROR";
    private const string EngineUnavailable = "RULES_ENGINE_UNAVAILABLE";

    [Test]
    public async Task Fail_Closed_Denies_And_Refunds_Even_When_The_Flag_Defaults_To_True()
    {
        var leases = new InMemoryLeaseStore();
        using var reservations = new InMemoryReservationStore(leases);
        var datastream = new StubCheckDataStream(FlagKey, Company())
            .WithFlagDefault(true)
            .ProbesCredit(CreditId, 10, "inference_tokens")
            .FailsInTheEngine(EngineError, new InvalidOperationException("wasm trap"));

        var result = await CheckAsync(leases, reservations, datastream, OnAcquireFailure.FailClosed);

        Assert.That(result.Allowed, Is.False);
        Assert.That(result.Reservation, Is.Null);

        // The hold was taken before the evaluation, so it has to go back.
        var lease = await leases.GetAsync("co_1", CreditId);
        Assert.That(lease!.LocalRemainingCredits, Is.EqualTo(1000));
        Assert.That(await reservations.CountAsync(), Is.EqualTo(0));
    }

    [Test]
    public async Task Fail_Open_Allows_Even_When_The_Flag_Defaults_To_False()
    {
        var leases = new InMemoryLeaseStore();
        using var reservations = new InMemoryReservationStore(leases);
        var datastream = new StubCheckDataStream(FlagKey, Company())
            .WithFlagDefault(false)
            .ProbesCredit(CreditId, 10, "inference_tokens")
            .FailsInTheEngine(EngineUnavailable);

        var result = await CheckAsync(leases, reservations, datastream, OnAcquireFailure.FailOpen);

        // No evaluation ran, so there is no verdict to defer to and fail-open
        // means allow, not "return whatever the flag defaults to".
        Assert.That(result.Allowed, Is.True);
        Assert.That(result.Reservation, Is.Null);
        var lease = await leases.GetAsync("co_1", CreditId);
        Assert.That(lease!.LocalRemainingCredits, Is.EqualTo(1000));
    }

    [Test]
    public async Task A_Fail_Open_Rerun_That_Faults_Allows_Rather_Than_Returning_The_Default()
    {
        var leases = new InMemoryLeaseStore();
        using var reservations = new InMemoryReservationStore(leases);
        var datastream = new StubCheckDataStream(FlagKey, Company())
            .WithFlagDefault(false)
            .ProbesCredit(CreditId, 10, "inference_tokens")
            .FailsInTheEngine(EngineError, new InvalidOperationException("wasm trap"));
        // The lease never arrives, so the check drops to the fail-open re-run,
        // and that re-run is the call that faults.
        var wire = new ScriptedLeaseWireClient { NextAcquireError = "acquire failed" };

        var result = await CheckAsync(
            leases,
            reservations,
            datastream,
            OnAcquireFailure.FailOpen,
            wire
        );

        Assert.That(result.Allowed, Is.True);
        Assert.That(result.Reservation, Is.Null);
    }

    [Test]
    public async Task A_Company_With_No_Credit_Balances_Is_Evaluated_Rather_Than_Throwing()
    {
        var leases = new InMemoryLeaseStore();
        using var reservations = new InMemoryReservationStore(leases);
        // The payload omits credit_balances rather than sending an empty map,
        // and the gate has to substitute into it anyway.
        var company = Company();
        company.CreditBalances = null;
        var datastream = new StubCheckDataStream(FlagKey, company)
            .ProbesCredit(CreditId, 10, "inference_tokens")
            .Answers(true, "ok");

        var result = await CheckAsync(leases, reservations, datastream, OnAcquireFailure.FailClosed);

        Assert.That(result.Allowed, Is.True);
        Assert.That(result.Reservation, Is.Not.Null);
    }

    [Test]
    public async Task Anything_Thrown_While_Gating_Refunds_The_Hold()
    {
        var leases = new InMemoryLeaseStore();
        using var reservations = new InMemoryReservationStore(leases);
        var datastream = new ThrowingCheckDataStream(FlagKey, Company());

        var result = await CheckAsync(leases, reservations, datastream, OnAcquireFailure.FailClosed);

        Assert.That(result.Allowed, Is.False);
        // The hold is taken before the gate runs, so every way out of it has to
        // go back through the refund.
        var lease = await leases.GetAsync("co_1", CreditId);
        Assert.That(lease!.LocalRemainingCredits, Is.EqualTo(1000));
        Assert.That(await reservations.CountAsync(), Is.EqualTo(0));
    }

    /// <summary>
    /// Probes the entitlement, then throws on the gating evaluation.
    /// </summary>
    private sealed class ThrowingCheckDataStream : StubCheckDataStream
    {
        private int _calls;

        public ThrowingCheckDataStream(string flagKey, RulesengineCompany company)
            : base(flagKey, company)
        {
            ProbesCredit(CreditId, 10, "inference_tokens");
        }

        public override Task<CheckFlagResult> EvaluateAsync(
            RulesengineFlag flag,
            RulesengineCompany company,
            RulesengineUser? user,
            PreflightRequestBody? preflight
        )
        {
            if (_calls++ == 0)
            {
                return base.EvaluateAsync(flag, company, user, preflight);
            }
            throw new InvalidOperationException("the gate blew up");
        }
    }

    private static Task<CheckResult> CheckAsync(
        ILeaseStore leases,
        IReservationStore reservations,
        StubCheckDataStream datastream,
        OnAcquireFailure onAcquireFailure,
        ScriptedLeaseWireClient? wire = null
    )
    {
        wire ??= new ScriptedLeaseWireClient
        {
            NextAcquire = new LeaseGrant
            {
                LeaseId = "lse_1",
                CompanyId = "co_1",
                CreditTypeId = CreditId,
                GrantedAmount = 1000,
                ExpiresAt = DateTime.UtcNow.AddMinutes(5),
            },
        };

        return LeaseCheck.CheckWithLeaseAsync(
            new CheckDeps
            {
                LeaseStore = leases,
                Reservations = reservations,
                Manager = new CreditLeaseManager(
                    wire,
                    leases,
                    new CreditLeaseConfig { DefaultLeaseSize = 1000 },
                    NullLogger.Instance
                ),
                DataStream = datastream,
                Logger = NullLogger.Instance,
            },
            FlagKey,
            new Dictionary<string, string> { ["id"] = "co_1" },
            null,
            new CheckOptions
            {
                Usage = 10,
                EventSubtype = "inference_tokens",
                OnAcquireFailure = onAcquireFailure,
            },
            () => throw new InvalidOperationException("the fallback should not be reached")
        );
    }

    private static RulesengineCompany Company() =>
        new()
        {
            Id = "co_1",
            AccountId = "acct_1",
            EnvironmentId = "env_1",
            CreditBalances = new Dictionary<string, double> { [CreditId] = 5000 },
        };
}
