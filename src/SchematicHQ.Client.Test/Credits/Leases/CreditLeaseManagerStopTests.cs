using System.Diagnostics;
using Microsoft.Extensions.Logging;
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
    public async Task A_Lease_That_Lands_After_A_Stop_Is_Left_Where_It_Is()
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

        // The drain waits on this flight, and the shutdown release handles a
        // per-process store. Releasing here instead would refund a lease that
        // sibling pods are already reserving against on a shared backend.
        Assert.That(lease, Is.Not.Null);
        Assert.That(wire.ReleasedLeaseIds, Is.Empty);
        Assert.That((await leases.GetAsync("co_1", "ct_1"))!.LeaseId, Is.EqualTo("lse_1"));
    }

    [Test]
    public async Task A_Release_That_Never_Lands_Does_Not_Hold_The_Close_Open()
    {
        var leases = new InMemoryLeaseStore();
        await leases.ReplaceAsync(Grant());
        var wire = new HangingReleaseWire();
        using var logs = new RecordingLoggerFactory();
        var manager = new CreditLeaseManager(
            wire,
            leases,
            new CreditLeaseConfig { DefaultLeaseSize = 1000 },
            logs.CreateLogger("test")
        );

        var clock = Stopwatch.StartNew();
        await manager.ReleaseAllLocalLeasesAsync(TimeSpan.FromMilliseconds(50));
        clock.Stop();

        Assert.That(clock.Elapsed, Is.LessThan(TimeSpan.FromSeconds(2)));
        Assert.That(logs.Logged(LogLevel.Warning, "releasing credit leases on close"), Is.True);
        // Abandoned, not cancelled: the lease is still the server's to expire.
        wire.Finish();
    }

    /// <summary>
    /// A server whose release call never answers, which is what the shutdown
    /// budget exists for.
    /// </summary>
    private sealed class HangingReleaseWire : ILeaseWireClient
    {
        private readonly TaskCompletionSource<bool> _hang = new();

        public void Finish() => _hang.TrySetResult(true);

        public Task<LeaseGrant> AcquireAsync(
            string companyId,
            string creditTypeId,
            double requestedAmount,
            DateTime expiresAt,
            RequestOptions? options = null
        ) => throw new InvalidOperationException("no acquire was expected");

        public Task<LeaseGrant> ExtendAsync(
            string leaseId,
            double additionalAmount,
            DateTime expiresAt,
            RequestOptions? options = null
        ) => throw new InvalidOperationException("no extend was expected");

        public Task ReleaseAsync(string leaseId, RequestOptions? options = null) => _hang.Task;
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
