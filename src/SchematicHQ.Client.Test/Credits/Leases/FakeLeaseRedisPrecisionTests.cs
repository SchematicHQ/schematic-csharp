using NUnit.Framework;
using SchematicHQ.Client.Leases;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// The fake stands in for the Lua the real stores run, and Redis runs Lua 5.1,
/// whose tostring writes %.14g. A fake that kept full double precision would
/// refuse reserves real Redis allows, so the vectors would pin behaviour no
/// deployment has.
/// </summary>
[TestFixture]
public class FakeLeaseRedisPrecisionTests
{
    [Test]
    public async Task A_Balance_That_Only_Binary_Arithmetic_Would_Undershoot_Still_Reserves()
    {
        var redis = new FakeLeaseRedis();
        var store = new RedisLeaseStore(redis);
        await store.ReplaceAsync(
            new LeaseGrant
            {
                LeaseId = "lse_1",
                CompanyId = "co_1",
                CreditTypeId = "ct_1",
                GrantedAmount = 0.3,
                ExpiresAt = DateTime.UtcNow.AddMinutes(5),
            }
        );

        var first = await store.TryReserveAsync("co_1", "ct_1", 0.1);
        Assert.That(first, Is.Not.Null);

        // 0.3 - 0.1 is 0.19999999999999998 in binary, so a store that kept
        // every digit would refuse this. Redis rounds the balance to 14
        // significant digits on the way back out and allows it.
        var second = await store.TryReserveAsync("co_1", "ct_1", 0.2);
        Assert.That(second, Is.Not.Null, "real Redis allows this reserve");
        Assert.That(second!.Value.Balance, Is.EqualTo(0).Within(1e-12));
    }
}
