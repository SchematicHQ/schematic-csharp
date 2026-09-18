using NUnit.Framework;
using SchematicHQ.Client.Leases;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// The two reservation backends have to answer alike, so a deployment can move
/// from per-process stores to Redis without a behaviour change.
/// </summary>
[TestFixture]
public class ReservationStoreParityTests
{
    [Test]
    public async Task Neither_Backend_Carries_The_Mode_Through_A_Round_Trip()
    {
        var now = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        LeaseClock clock = () => now;

        var inMemoryLeases = new InMemoryLeaseStore(clock);
        var inMemory = new InMemoryReservationStore(inMemoryLeases, clock: clock);

        var redisClient = new FakeLeaseRedis(clock);
        var redisLeases = new RedisLeaseStore(redisClient, clock: clock);
        var redis = new RedisReservationStore(redisClient, redisLeases, clock: clock);

        foreach (var store in new IReservationStore[] { inMemory, redis })
        {
            await store.AddAsync(Record(now));

            var read = await store.GetAsync("res_1");

            Assert.That(read, Is.Not.Null);
            // Redis has no field for it and a server-mode hold never enters a
            // store at all, so the mode is a caller-side annotation on the
            // handle rather than something a store keeps.
            Assert.That(read!.Mode, Is.EqualTo(CreditLeaseMode.Client));
            Assert.That(read.EventSubtype, Is.EqualTo("inference_tokens"));
            Assert.That(read.CreditsReserved, Is.EqualTo(100));

            store.Stop();
        }
    }

    private static ReservationRecord Record(DateTime now) =>
        new()
        {
            Id = "res_1",
            LeaseId = "lse_1",
            Mode = CreditLeaseMode.Server,
            CompanyId = "co_1",
            CreditTypeId = "ct_1",
            EventSubtype = "inference_tokens",
            QuantityReserved = 10,
            CreditsReserved = 100,
            ConsumptionRate = 10,
            ExpiresAt = now.AddMinutes(1),
        };
}
