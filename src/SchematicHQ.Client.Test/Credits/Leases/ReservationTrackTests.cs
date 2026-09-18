using NUnit.Framework;
using SchematicHQ.Client.Leases;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// The event a settle bills, which is built from the caller-held record alone
/// so the usage is reported even once the hold has been swept.
/// </summary>
[TestFixture]
public class ReservationTrackTests
{
    [Test]
    public void Client_Mode_Routes_Through_The_Lease_Sub_Ledger()
    {
        var track = ReservationTrack.BuildTrackEvent(Record(CreditLeaseMode.Client), 4);

        Assert.That(track.Event, Is.EqualTo("inference_tokens"));
        Assert.That(track.LeaseId, Is.EqualTo("lse_1"));
        // The server prefers a lease id when both are set, so a client-mode
        // event must never carry a reservation id too.
        Assert.That(track.ReservationId, Is.Null);
        Assert.That(track.Company, Is.EqualTo(new Dictionary<string, string> { ["id"] = "co_1" }));
    }

    [Test]
    public void Server_Mode_Settles_By_Reservation_Id_And_Never_Sends_A_Lease_Id()
    {
        var track = ReservationTrack.BuildTrackEvent(Record(CreditLeaseMode.Server), 4);

        Assert.That(track.ReservationId, Is.EqualTo("res_1"));
        Assert.That(track.LeaseId, Is.Null);
    }

    [Test]
    [TestCase(0.0, 0L)]
    [TestCase(0.1, 1L)]
    [TestCase(4.0, 4L)]
    [TestCase(4.2, 5L)]
    public void A_Fractional_Usage_Settles_As_A_Whole_Unit(double actual, long expected)
    {
        // A hold can be sized from a fraction, but the event's quantity is an
        // integer, so a partial unit bills as one rather than as none.
        Assert.That(ReservationTrack.SettleQuantity(actual), Is.EqualTo(expected));
    }

    [Test]
    public async Task Consume_Refunds_The_Unspent_Slice_And_Reports_A_Local_Settle()
    {
        var leases = new InMemoryLeaseStore();
        await leases.ReplaceAsync(
            new LeaseGrant
            {
                LeaseId = "lse_1",
                CompanyId = "co_1",
                CreditTypeId = "ct_1",
                GrantedAmount = 1000,
                ExpiresAt = DateTime.UtcNow.AddMinutes(5),
            }
        );
        await leases.TryReserveAsync("co_1", "ct_1", 100);

        using var reservations = new InMemoryReservationStore(leases);
        var record = Record(CreditLeaseMode.Client);
        await reservations.AddAsync(record);

        var outcome = await ReservationTrack.ConsumeAndBuildEventAsync(reservations, record, 4);

        Assert.That(outcome.SettledLocally, Is.True);
        Assert.That(outcome.Track.Quantity, Is.EqualTo(4));
        var lease = await leases.GetAsync("co_1", "ct_1");
        Assert.That(lease!.LocalRemainingCredits, Is.EqualTo(960));
    }

    [Test]
    public async Task A_Settle_After_The_Sweep_Is_A_Recovery_Emit()
    {
        var leases = new InMemoryLeaseStore();
        using var reservations = new InMemoryReservationStore(leases);
        var record = Record(CreditLeaseMode.Client);

        var outcome = await ReservationTrack.ConsumeAndBuildEventAsync(reservations, record, 4);

        // Nothing was claimed, so the lease was not touched here, but the usage
        // still has to reach the server.
        Assert.That(outcome.SettledLocally, Is.False);
        Assert.That(outcome.Track.Quantity, Is.EqualTo(4));
        Assert.That(outcome.Track.LeaseId, Is.EqualTo("lse_1"));
    }

    private static ReservationRecord Record(CreditLeaseMode mode) =>
        new()
        {
            Id = "res_1",
            LeaseId = mode == CreditLeaseMode.Server ? "res_1" : "lse_1",
            Mode = mode,
            CompanyId = "co_1",
            CreditTypeId = "ct_1",
            EventSubtype = "inference_tokens",
            QuantityReserved = 10,
            CreditsReserved = 100,
            ConsumptionRate = 10,
            ExpiresAt = DateTime.UtcNow.AddMinutes(1),
            Company = new Dictionary<string, string> { ["id"] = "co_1" },
        };
}
