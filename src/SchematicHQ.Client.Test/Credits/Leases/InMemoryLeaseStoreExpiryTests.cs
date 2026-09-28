using NUnit.Framework;
using SchematicHQ.Client.Leases;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// DateTime comparisons ignore Kind, so the per-process store holds every
/// expiry as UTC. Otherwise a local or unspecified expiry would compare against
/// UtcNow shifted by the host's offset.
/// </summary>
[TestFixture]
public class InMemoryLeaseStoreExpiryTests
{
    private static readonly DateTime Expiry = new(2030, 1, 1, 12, 0, 0, DateTimeKind.Utc);

    [Test]
    public async Task A_Local_Expiry_Is_Held_As_The_Same_Utc_Instant()
    {
        var store = new InMemoryLeaseStore(() => Expiry.AddMinutes(-1));
        await store.ReplaceAsync(Grant(Expiry.ToLocalTime()));

        var lease = await store.GetAsync("co_1", "ct_1");

        Assert.That(lease!.ExpiresAt, Is.EqualTo(Expiry));
        Assert.That(lease.ExpiresAt.Kind, Is.EqualTo(DateTimeKind.Utc));
    }

    [Test]
    public async Task An_Unspecified_Expiry_Is_Read_As_Utc()
    {
        var store = new InMemoryLeaseStore(() => Expiry.AddMinutes(-1));
        await store.ReplaceAsync(Grant(DateTime.SpecifyKind(Expiry, DateTimeKind.Unspecified)));

        var lease = await store.GetAsync("co_1", "ct_1");

        Assert.That(lease!.ExpiresAt, Is.EqualTo(Expiry));
        Assert.That(lease.ExpiresAt.Kind, Is.EqualTo(DateTimeKind.Utc));
    }

    [Test]
    public async Task An_Extend_With_A_Local_Expiry_Moves_It_By_The_Utc_Instant()
    {
        var store = new InMemoryLeaseStore(() => Expiry.AddMinutes(-1));
        await store.ReplaceAsync(Grant(Expiry));

        await store.ExtendAsync("co_1", "ct_1", 1000, Expiry.AddMinutes(5).ToLocalTime());

        var lease = await store.GetAsync("co_1", "ct_1");
        Assert.That(lease!.ExpiresAt, Is.EqualTo(Expiry.AddMinutes(5)));
        Assert.That(lease.ExpiresAt.Kind, Is.EqualTo(DateTimeKind.Utc));
    }

    private static LeaseGrant Grant(DateTime expiresAt) =>
        new()
        {
            LeaseId = "lse_1",
            CompanyId = "co_1",
            CreditTypeId = "ct_1",
            GrantedAmount = 1000,
            ExpiresAt = expiresAt,
        };
}
