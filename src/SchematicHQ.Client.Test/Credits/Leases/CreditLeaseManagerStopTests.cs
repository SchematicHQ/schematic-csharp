using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using SchematicHQ.Client.Leases;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// What a Stop does to an acquire. A drain only waits on flights it can see, so
/// a Stop that lands between the manager's stopped check and the registration
/// would let a lease arrive behind a client that has already shut down.
/// </summary>
[TestFixture]
public class CreditLeaseManagerStopTests
{
    [Test]
    public async Task A_Stopped_Manager_Does_Not_Reach_The_Wire()
    {
        var leases = new InMemoryLeaseStore();
        var wire = new ScriptedLeaseWireClient { NextAcquire = Grant() };
        var manager = Manager(wire, leases);

        manager.Stop();
        var lease = await manager.AcquireIfNeededAsync("co_1", "ct_1");

        Assert.That(lease, Is.Null);
        Assert.That(wire.AcquireCount, Is.EqualTo(0));
    }

    [Test]
    public async Task A_Lease_That_Lands_After_A_Stop_Is_Handed_Straight_Back()
    {
        var leases = new InMemoryLeaseStore();
        var wire = new ScriptedLeaseWireClient { NextAcquire = Grant() };
        var manager = Manager(wire, leases);
        // Stop lands while the wire call is out, which is the window the
        // stopped check before the registration cannot cover.
        wire.InstallDuringWire = () =>
        {
            manager.Stop();
            return Task.CompletedTask;
        };

        var lease = await manager.AcquireIfNeededAsync("co_1", "ct_1");

        Assert.That(lease, Is.Null);
        // No check will ever spend these credits, and the shutdown path
        // releases nothing against a shared backend, so they go back here.
        Assert.That(wire.ReleasedLeaseIds, Is.EqualTo(new[] { "lse_1" }));
        Assert.That(await leases.GetAsync("co_1", "ct_1"), Is.Null);
    }

    private static CreditLeaseManager Manager(ScriptedLeaseWireClient wire, ILeaseStore leases) =>
        new(wire, leases, new CreditLeaseConfig { DefaultLeaseSize = 1000 }, NullLogger.Instance);

    private static LeaseGrant Grant() =>
        new()
        {
            LeaseId = "lse_1",
            CompanyId = "co_1",
            CreditTypeId = "ct_1",
            GrantedAmount = 1000,
            ExpiresAt = DateTime.UtcNow.AddMinutes(5),
        };
}
