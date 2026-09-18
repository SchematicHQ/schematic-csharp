using NUnit.Framework;
using SchematicHQ.Client.Leases;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// A hold written by a sibling pod running another SDK has to settle here. The
/// other SDKs store the whole check request body as the evaluation context, so
/// the stored object can carry a preflight next to the company and user keys,
/// and those keys are what the settling track event is addressed to.
/// </summary>
[TestFixture]
public class ReservationEvalCtxTests
{
    // The layout a sibling pod writes to, which is fixed across SDKs so a mixed
    // fleet shares one Redis.
    private const string HashKey = "schematic:credit-reservation:res_1";

    [Test]
    public async Task A_Context_Carrying_A_Preflight_Still_Settles_With_The_Entity_Keys()
    {
        var redis = new FakeLeaseRedis();
        var leases = new RedisLeaseStore(redis);
        using var reservations = new RedisReservationStore(redis, leases);

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
        await reservations.AddAsync(Record());

        // What a sibling pod stores: the whole request body, not just the two
        // entity maps.
        await redis.HashSetAsync(
            HashKey,
            new[]
            {
                new KeyValuePair<string, string>(
                    "evalCtx",
                    """
                    {"company":{"id":"co_1"},"user":{"user_id":"u_1"},
                     "preflight":{"usage":10,"event_usage":{"event_subtype":"inference_tokens",
                     "quantity":10},"credit_cost":{"ct_1":100}}}
                    """
                ),
            }
        );

        var read = await reservations.GetAsync("res_1");

        Assert.That(read, Is.Not.Null);
        Assert.That(read!.Company, Is.EqualTo(new Dictionary<string, string> { ["id"] = "co_1" }));
        Assert.That(
            read.User,
            Is.EqualTo(new Dictionary<string, string> { ["user_id"] = "u_1" })
        );

        var outcome = await ReservationTrack.ConsumeAndBuildEventAsync(reservations, read, 10);

        Assert.That(outcome.SettledLocally, Is.True);
        // Without the keys the event would be billed to nobody.
        Assert.That(outcome.Track.Company, Is.EqualTo(read.Company));
        Assert.That(outcome.Track.User, Is.EqualTo(read.User));
    }

    [Test]
    public async Task A_Context_That_Is_Not_An_Object_Leaves_The_Keys_Unset()
    {
        var redis = new FakeLeaseRedis();
        var leases = new RedisLeaseStore(redis);
        using var reservations = new RedisReservationStore(redis, leases);

        await reservations.AddAsync(Record());
        await redis.HashSetAsync(
            HashKey,
            new[] { new KeyValuePair<string, string>("evalCtx", "\"not an object\"") }
        );

        var read = await reservations.GetAsync("res_1");

        // Still readable: a hold whose context makes no sense has to settle
        // rather than strand its credits until the TTL.
        Assert.That(read, Is.Not.Null);
        Assert.That(read!.Company, Is.Null);
        Assert.That(read.User, Is.Null);
    }

    private static ReservationRecord Record() =>
        new()
        {
            Id = "res_1",
            LeaseId = "lse_1",
            CompanyId = "co_1",
            CreditTypeId = "ct_1",
            EventSubtype = "inference_tokens",
            QuantityReserved = 10,
            CreditsReserved = 100,
            ConsumptionRate = 10,
            ExpiresAt = DateTime.UtcNow.AddMinutes(1),
        };
}
