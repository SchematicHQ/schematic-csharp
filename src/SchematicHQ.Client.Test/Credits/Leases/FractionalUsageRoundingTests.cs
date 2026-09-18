using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using SchematicHQ.Client.Leases;
using SchematicHQ.Client.RulesEngine;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// A fractional usage is held and debited exactly. Only the two integer fields
/// round up: the preflight the engine gates on, and the quantity the settling
/// event bills.
/// </summary>
[TestFixture]
public class FractionalUsageRoundingTests
{
    private const string CreditId = "ct_1";
    private const string FlagKey = "inference";

    [Test]
    public async Task Half_A_Unit_At_A_Rate_Of_Ten_Holds_Five_Credits()
    {
        var leases = new InMemoryLeaseStore();
        using var reservations = new InMemoryReservationStore(leases);
        var wire = new ScriptedLeaseWireClient
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
        var datastream = new StubCheckDataStream(FlagKey, Company())
            .ProbesCredit(CreditId, 10, "inference_tokens")
            .Answers(true, "ok");

        var result = await LeaseCheck.CheckWithLeaseAsync(
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
            new CheckOptions { Usage = 0.5, EventSubtype = "inference_tokens" },
            () => throw new InvalidOperationException("the fallback should not be reached")
        );

        Assert.That(result.Allowed, Is.True);
        Assert.That(result.Reservation!.QuantityReserved, Is.EqualTo(0.5));
        Assert.That(result.Reservation.CreditsReserved, Is.EqualTo(5));

        var lease = await leases.GetAsync("co_1", CreditId);
        Assert.That(lease!.LocalRemainingCredits, Is.EqualTo(995));

        // The engine's gate rounds up, so a check never passes on less usage
        // than the caller will record.
        Assert.That(LeasePreflight.PreflightQuantity(0.5), Is.EqualTo(1));
    }

    [Test]
    public async Task Settling_Half_A_Unit_At_A_Rate_Of_Ten_Debits_Five_And_Bills_One()
    {
        var leases = new InMemoryLeaseStore();
        await leases.ReplaceAsync(
            new LeaseGrant
            {
                LeaseId = "lse_1",
                CompanyId = "co_1",
                CreditTypeId = CreditId,
                GrantedAmount = 1000,
                ExpiresAt = DateTime.UtcNow.AddMinutes(5),
            }
        );
        using var reservations = new InMemoryReservationStore(leases);
        await leases.TryReserveAsync("co_1", CreditId, 10);
        await reservations.AddAsync(
            new ReservationRecord
            {
                Id = "res_1",
                LeaseId = "lse_1",
                CompanyId = "co_1",
                CreditTypeId = CreditId,
                EventSubtype = "inference_tokens",
                QuantityReserved = 1,
                CreditsReserved = 10,
                // The hold covers a whole unit; this settle spends half of it.
                ConsumptionRate = 10,
                ExpiresAt = DateTime.UtcNow.AddMinutes(1),
            }
        );

        var outcome = await ReservationTrack.ConsumeAndBuildEventAsync(
            reservations,
            await reservations.GetAsync("res_1") ?? throw new InvalidOperationException(),
            0.5
        );

        // The lease is debited the exact credits the usage cost, and the
        // unspent half of the hold goes back. The event's quantity is an
        // integer, so it still bills a whole unit.
        Assert.That(outcome.Track.Quantity, Is.EqualTo(1));
        Assert.That(outcome.SettledLocally, Is.True);
        var lease = await leases.GetAsync("co_1", CreditId);
        Assert.That(lease!.LocalRemainingCredits, Is.EqualTo(995));
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
