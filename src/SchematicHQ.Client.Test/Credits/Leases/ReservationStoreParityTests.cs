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

    [Test]
    public async Task Neither_Backend_Refunds_A_Hold_That_Cannot_Name_Its_Lease()
    {
        // The two lease stores read an empty pin differently: the Lua takes it
        // as no pin at all and would credit whichever lease holds the slot,
        // while the per-process store takes it as a pin nothing matches. Left
        // to them, one backend would refund and the other would not. Both
        // reservation stores decide it instead, and both decline.
        var now = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        LeaseClock clock = () => now;

        var inMemoryLeases = new InMemoryLeaseStore(clock);
        var inMemory = new InMemoryReservationStore(inMemoryLeases, clock: clock);

        var redisClient = new FakeLeaseRedis(clock);
        var redisLeases = new RedisLeaseStore(redisClient, clock: clock);
        var redis = new RedisReservationStore(redisClient, redisLeases, clock: clock);

        foreach (var pair in new (IReservationStore Store, ILeaseStore Leases)[]
        {
            (inMemory, inMemoryLeases),
            (redis, redisLeases),
        })
        {
            await pair.Leases.ReplaceAsync(
                new LeaseGrant
                {
                    LeaseId = "lse_successor",
                    CompanyId = "co_1",
                    CreditTypeId = "ct_1",
                    GrantedAmount = 1000,
                    ExpiresAt = now.AddMinutes(5),
                }
            );
            await pair.Leases.TryReserveAsync("co_1", "ct_1", 100);

            var orphan = Record(now);
            orphan.LeaseId = string.Empty;
            await pair.Store.AddAsync(orphan);
            await pair.Store.ConsumeAsync("res_1", 0);

            // 900, not 1000: the successor's balance is untouched.
            var lease = await pair.Leases.GetAsync("co_1", "ct_1");
            Assert.That(lease!.LocalRemainingCredits, Is.EqualTo(900));

            pair.Store.Stop();
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
