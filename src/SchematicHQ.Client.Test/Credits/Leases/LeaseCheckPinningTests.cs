using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using SchematicHQ.Client.Leases;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// Which lease a hold is pinned to. The debit is not keyed by lease, so the
/// slot's lease can be replaced between a caller's acquire and its reserve, and
/// only the store can say which lease was actually charged.
/// </summary>
[TestFixture]
public class LeaseCheckPinningTests
{
    private const string CreditId = "ct_1";
    private const string FlagKey = "inference";

    [Test]
    public async Task The_Hold_Names_The_Charged_Lease_Even_When_The_Store_Names_None()
    {
        // A store that debits without naming a lease. Falling back to the
        // acquired id here would send the settle refund, the sweep refund and
        // the track event's lease id to a lease that never held the credits, so
        // the empty id has to survive: it matches no lease, which drops the
        // refunds rather than crediting the wrong lease.
        var leases = new AnonymousReserveLeaseStore(new InMemoryLeaseStore());
        using var reservations = new InMemoryReservationStore(leases);
        var wire = new ScriptedLeaseWireClient
        {
            NextAcquire = new LeaseGrant
            {
                LeaseId = "lse_acquired",
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
            new CheckOptions { Usage = 10, EventSubtype = "inference_tokens" },
            () => throw new InvalidOperationException("the fallback should not be reached")
        );

        Assert.That(result.Allowed, Is.True);
        Assert.That(result.Reservation, Is.Not.Null);
        Assert.That(result.Reservation!.LeaseId, Is.Empty);
    }

    private static RulesengineCompany Company() =>
        new()
        {
            Id = "co_1",
            AccountId = "acct_1",
            EnvironmentId = "env_1",
            CreditBalances = new Dictionary<string, double> { [CreditId] = 5000 },
        };

    /// <summary>
    /// Debits through the real store but reports no lease id, the way a backend
    /// whose row has lost the field would.
    /// </summary>
    private sealed class AnonymousReserveLeaseStore : ILeaseStore
    {
        private readonly ILeaseStore _inner;

        public AnonymousReserveLeaseStore(ILeaseStore inner)
        {
            _inner = inner;
        }

        public Task<LeaseState?> GetAsync(string companyId, string creditTypeId) =>
            _inner.GetAsync(companyId, creditTypeId);

        public Task<bool> ReplaceAsync(LeaseGrant grant) => _inner.ReplaceAsync(grant);

        public async Task<ReserveResult?> TryReserveAsync(
            string companyId,
            string creditTypeId,
            double credits
        )
        {
            var result = await _inner.TryReserveAsync(companyId, creditTypeId, credits);
            return result == null ? null : new ReserveResult(result.Value.Balance, string.Empty);
        }

        public Task RefundAsync(
            string companyId,
            string creditTypeId,
            double credits,
            string? pinLeaseId = null
        ) => _inner.RefundAsync(companyId, creditTypeId, credits, pinLeaseId);

        public Task ExtendAsync(
            string companyId,
            string creditTypeId,
            double grantedTotal,
            DateTime? newExpiresAt = null,
            string? pinLeaseId = null
        ) => _inner.ExtendAsync(companyId, creditTypeId, grantedTotal, newExpiresAt, pinLeaseId);

        public Task DropAsync(string companyId, string creditTypeId) =>
            _inner.DropAsync(companyId, creditTypeId);
    }
}
