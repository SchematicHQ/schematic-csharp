using System.Diagnostics;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using SchematicHQ.Client.Leases;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// How callers share one extend. Joining is what keeps a burst of checks to one
/// wire call, but a caller must not inherit an ask that is smaller than its own
/// shortfall, and must not sit on someone else's flight past its own timeout.
/// </summary>
[TestFixture]
public class CreditLeaseManagerExtendTests
{
    private const string Company = "co_1";
    private const string CreditType = "ct_1";

    [Test]
    public async Task A_Follow_Up_Does_Not_Inherit_Another_Callers_Smaller_Follow_Up()
    {
        // Two checks join one 10k refill, both needing more than it asked for.
        // Whichever resumes first tops up its own shortfall, and the other must
        // not take that result: it would go back to its retry against a lease
        // it already knows is short, with the credits sitting on the server.
        var wire = new GrantingExtendWire(10_000) { HoldFirstCall = true };
        var store = new InMemoryLeaseStore();
        var manager = Manager(wire, store);
        await Seed(store, 10_000);
        await store.TryReserveAsync(Company, CreditType, 10_000);

        var refill = manager.MaybeExtendInBackgroundAsync(Company, CreditType, 10_000);
        Assert.That(wire.ExtendAmounts, Is.EqualTo(new[] { 10_000d }));

        var cTask = manager.MaybeExtendInBackgroundAsync(Company, CreditType, 12_000);
        var aTask = manager.MaybeExtendInBackgroundAsync(Company, CreditType, 28_000);
        Assert.That(wire.ExtendAmounts, Has.Count.EqualTo(1));

        wire.ReleaseFirstCall();
        await refill;
        var c = await cTask;
        var a = await aTask;

        // Which of the two resumes first is the scheduler's to decide, so the
        // exact asks are not pinned. Either way the refill cannot be the last
        // word, and neither caller may come back with a lease that will not
        // cover what it asked for.
        Assert.That(wire.ExtendAmounts, Has.Count.GreaterThanOrEqualTo(2));
        Assert.That(wire.ExtendAmounts[0], Is.EqualTo(10_000));
        Assert.That(c!.LocalRemainingCredits, Is.GreaterThanOrEqualTo(12_000));
        Assert.That(a!.LocalRemainingCredits, Is.GreaterThanOrEqualTo(28_000));
    }

    [Test]
    public async Task A_Joiner_Waits_No_Longer_Than_Its_Own_Timeout()
    {
        // The flight runs on whatever timeout started it (a background refresh
        // uses the client default). A check with 50ms to spend must not sit
        // behind it: it gives up, takes its fail-open or fail-closed path, and
        // leaves the flight running for everyone else.
        var wire = new GrantingExtendWire(1_000) { HoldFirstCall = true };
        var store = new InMemoryLeaseStore();
        var manager = Manager(wire, store);
        await Seed(store, 1_000);
        // 200 left, below the water mark.
        await store.TryReserveAsync(Company, CreditType, 800);

        var flight = manager.MaybeExtendInBackgroundAsync(Company, CreditType);
        Assert.That(wire.ExtendAmounts, Has.Count.EqualTo(1));

        var clock = Stopwatch.StartNew();
        var impatient = await manager.MaybeExtendInBackgroundAsync(
            Company,
            CreditType,
            900,
            new RequestOptions { Timeout = TimeSpan.FromMilliseconds(50) }
        );
        clock.Stop();

        Assert.That(impatient, Is.Null);
        Assert.That(clock.Elapsed, Is.LessThan(TimeSpan.FromSeconds(2)));
        // No second wire call: the joiner abandoned its wait, it did not race
        // another extend onto the lease.
        Assert.That(wire.ExtendAmounts, Has.Count.EqualTo(1));

        // The flight still lands for the caller that started it.
        wire.ReleaseFirstCall();
        var first = await flight;
        Assert.That(first!.GrantedAmount, Is.EqualTo(2_000));
    }

    private static CreditLeaseManager Manager(ILeaseWireClient wire, ILeaseStore store) =>
        new(
            wire,
            store,
            new CreditLeaseConfig { DefaultLeaseSize = 1_000, LowWaterMark = 0.5 },
            NullLogger.Instance
        );

    private static Task<bool> Seed(InMemoryLeaseStore store, double granted) =>
        store.ReplaceAsync(
            new LeaseGrant
            {
                LeaseId = "lse_1",
                CompanyId = Company,
                CreditTypeId = CreditType,
                GrantedAmount = granted,
                ExpiresAt = DateTime.UtcNow.AddMinutes(5),
            }
        );

    /// <summary>
    /// A server that grants every extend in full, answering with the lease's new
    /// total. Its first call can be held open, which is how a test keeps one
    /// flight in the air while other callers arrive.
    /// </summary>
    private sealed class GrantingExtendWire : ILeaseWireClient
    {
        private readonly TaskCompletionSource<bool> _hold = new();
        private double _grantedTotal;

        public GrantingExtendWire(double grantedTotal)
        {
            _grantedTotal = grantedTotal;
        }

        public List<double> ExtendAmounts { get; } = new();

        public bool HoldFirstCall { get; set; }

        public void ReleaseFirstCall() => _hold.TrySetResult(true);

        public Task<LeaseGrant> AcquireAsync(
            string companyId,
            string creditTypeId,
            double requestedAmount,
            DateTime expiresAt,
            RequestOptions? options = null
        ) => throw new InvalidOperationException("no acquire was expected");

        public async Task<LeaseGrant> ExtendAsync(
            string leaseId,
            double additionalAmount,
            DateTime expiresAt,
            RequestOptions? options = null
        )
        {
            var first = ExtendAmounts.Count == 0;
            ExtendAmounts.Add(additionalAmount);
            if (first && HoldFirstCall)
            {
                await _hold.Task.ConfigureAwait(false);
            }
            _grantedTotal += additionalAmount;
            return new LeaseGrant
            {
                LeaseId = leaseId,
                CompanyId = Company,
                CreditTypeId = CreditType,
                GrantedAmount = _grantedTotal,
                ExpiresAt = expiresAt,
            };
        }

        public Task ReleaseAsync(string leaseId, RequestOptions? options = null) =>
            Task.CompletedTask;
    }
}
