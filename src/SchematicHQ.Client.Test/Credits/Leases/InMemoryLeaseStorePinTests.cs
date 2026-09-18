using NUnit.Framework;
using SchematicHQ.Client.Leases;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// How the per-process store reads a lease pin. A hold carved out of lease A
/// must never inflate a successor B: A's unspent remainder went back to the
/// company balance server-side when A expired, so crediting B would
/// double-count.
/// </summary>
[TestFixture]
public class InMemoryLeaseStorePinTests
{
    private InMemoryLeaseStore _leases = null!;

    [SetUp]
    public async Task SetUp()
    {
        _leases = new InMemoryLeaseStore();
        await _leases.ReplaceAsync(
            new LeaseGrant
            {
                LeaseId = "lse_1",
                CompanyId = "co_1",
                CreditTypeId = "ct_1",
                GrantedAmount = 1000,
                ExpiresAt = DateTime.UtcNow.AddMinutes(5),
            }
        );
        await _leases.TryReserveAsync("co_1", "ct_1", 100);
    }

    [Test]
    public async Task No_Pin_Refunds_Whichever_Lease_Holds_The_Slot()
    {
        await _leases.RefundAsync("co_1", "ct_1", 100);

        Assert.That((await _leases.GetAsync("co_1", "ct_1"))!.LocalRemainingCredits, Is.EqualTo(1000));
    }

    [Test]
    public async Task A_Matching_Pin_Refunds()
    {
        await _leases.RefundAsync("co_1", "ct_1", 100, "lse_1");

        Assert.That((await _leases.GetAsync("co_1", "ct_1"))!.LocalRemainingCredits, Is.EqualTo(1000));
    }

    [Test]
    public async Task A_Pin_Naming_Another_Lease_Drops_The_Refund()
    {
        await _leases.RefundAsync("co_1", "ct_1", 100, "lse_2");

        Assert.That((await _leases.GetAsync("co_1", "ct_1"))!.LocalRemainingCredits, Is.EqualTo(900));
    }

    [Test]
    public async Task An_Empty_Pin_Is_A_Pin_And_Matches_Nothing()
    {
        // An empty id is not the absence of a pin: a caller that could not name
        // the charged lease must drop the refund rather than credit whichever
        // lease happens to hold the slot.
        await _leases.RefundAsync("co_1", "ct_1", 100, string.Empty);

        Assert.That((await _leases.GetAsync("co_1", "ct_1"))!.LocalRemainingCredits, Is.EqualTo(900));
    }

    [Test]
    public async Task An_Empty_Pin_Drops_An_Extend_Too()
    {
        await _leases.ExtendAsync("co_1", "ct_1", 2000, pinLeaseId: string.Empty);

        var lease = (await _leases.GetAsync("co_1", "ct_1"))!;
        Assert.That(lease.GrantedAmount, Is.EqualTo(1000));
        Assert.That(lease.LocalRemainingCredits, Is.EqualTo(900));
    }

    [Test]
    public async Task No_Pin_Applies_An_Extend()
    {
        await _leases.ExtendAsync("co_1", "ct_1", 2000);

        var lease = (await _leases.GetAsync("co_1", "ct_1"))!;
        Assert.That(lease.GrantedAmount, Is.EqualTo(2000));
        Assert.That(lease.LocalRemainingCredits, Is.EqualTo(1900));
    }
}
