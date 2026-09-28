using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using SchematicHQ.Client.Leases;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// How acquires share the manager. Its lock guards the flight maps and nothing
/// else, so a store or wire client that blocks stalls only its own slot; and a
/// caller joining someone else's acquire waits no longer than its own deadline.
/// </summary>
[TestFixture]
public class CreditLeaseManagerAcquireTests
{
    private const string CreditType = "ct_1";

    [Test]
    public async Task A_Blocking_Acquire_Does_Not_Stall_Another_Slot()
    {
        var wire = new BlockingAcquireWire("co_blocked");
        var manager = new CreditLeaseManager(
            wire,
            new InMemoryLeaseStore(),
            new CreditLeaseConfig { DefaultLeaseSize = 1_000 },
            NullLogger.Instance
        );

        var blocked = Task.Run(() => manager.AcquireIfNeededAsync("co_blocked", CreditType));
        Assert.That(wire.Blocking.Wait(TimeSpan.FromSeconds(5)), Is.True);

        try
        {
            var other = Task.Run(() => manager.AcquireIfNeededAsync("co_free", CreditType));
            var winner = await Task.WhenAny(other, Task.Delay(TimeSpan.FromSeconds(5)));
            Assert.That(winner, Is.SameAs(other));
            Assert.That((await other)!.CompanyId, Is.EqualTo("co_free"));
        }
        finally
        {
            wire.Unblock.Set();
        }
        Assert.That(await blocked, Is.Not.Null);
    }

    [Test]
    public async Task A_Joiner_Waits_No_Longer_Than_Its_Deadline()
    {
        var wire = new BlockingAcquireWire("co_blocked");
        var manager = new CreditLeaseManager(
            wire,
            new InMemoryLeaseStore(),
            new CreditLeaseConfig { DefaultLeaseSize = 1_000 },
            NullLogger.Instance
        );

        var flight = Task.Run(() => manager.AcquireIfNeededAsync("co_blocked", CreditType));
        Assert.That(wire.Blocking.Wait(TimeSpan.FromSeconds(5)), Is.True);

        try
        {
            var joiner = manager.AcquireIfNeededAsync(
                "co_blocked",
                CreditType,
                joinDeadline: DateTime.UtcNow.AddMilliseconds(50)
            );
            var winner = await Task.WhenAny(joiner, Task.Delay(TimeSpan.FromSeconds(5)));
            Assert.That(winner, Is.SameAs(joiner));
            Assert.That(await joiner, Is.Null);
        }
        finally
        {
            wire.Unblock.Set();
        }
        // The flight lands for the caller that started it.
        Assert.That(await flight, Is.Not.Null);
    }

    /// <summary>
    /// Grants every acquire, except that one company's call blocks its thread,
    /// the way a synchronous client over a stalled socket would.
    /// </summary>
    private sealed class BlockingAcquireWire : ILeaseWireClient
    {
        private readonly string _blockedCompany;

        public BlockingAcquireWire(string blockedCompany)
        {
            _blockedCompany = blockedCompany;
        }

        public ManualResetEventSlim Blocking { get; } = new();

        public ManualResetEventSlim Unblock { get; } = new();

        public Task<LeaseGrant> AcquireAsync(
            string companyId,
            string creditTypeId,
            double requestedAmount,
            DateTime expiresAt,
            RequestOptions? options = null
        )
        {
            if (companyId == _blockedCompany)
            {
                Blocking.Set();
                Unblock.Wait();
            }
            return Task.FromResult(
                new LeaseGrant
                {
                    LeaseId = "lse_" + companyId,
                    CompanyId = companyId,
                    CreditTypeId = creditTypeId,
                    GrantedAmount = requestedAmount,
                    ExpiresAt = expiresAt,
                }
            );
        }

        public Task<LeaseGrant> ExtendAsync(
            string leaseId,
            double additionalAmount,
            DateTime expiresAt,
            RequestOptions? options = null
        ) => throw new InvalidOperationException("no extend was expected");

        public Task ReleaseAsync(string leaseId, RequestOptions? options = null) =>
            Task.CompletedTask;
    }
}
