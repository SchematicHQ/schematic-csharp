using NUnit.Framework;
using SchematicHQ.Client.Leases;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// How a reservation lands in Redis, and how the sweep loop starts and stops.
/// </summary>
[TestFixture]
public class ReservationStoreWriteTests
{
    [Test]
    public async Task The_Hash_And_Its_Expiry_Go_Out_As_One_Write()
    {
        var recording = new RecordingLeaseRedis(new FakeLeaseRedis());
        var leases = new RedisLeaseStore(recording);
        using var reservations = new RedisReservationStore(recording, leases);

        await reservations.AddAsync(Record());

        // Two writes would leave a window where the hash exists with no TTL and
        // no index pointing at it, so nothing reaps it and nothing finds it.
        Assert.That(recording.HashSetWithExpiryCalls, Is.EqualTo(1));
    }

    [Test]
    public void A_Failed_Combined_Write_Leaves_No_Reservation_Behind()
    {
        var redis = new FakeLeaseRedis { FailNextHashSetWithExpiry = true };
        var leases = new RedisLeaseStore(redis);
        using var reservations = new RedisReservationStore(redis, leases);

        Assert.ThrowsAsync<InvalidOperationException>(() => reservations.AddAsync(Record()));

        Assert.That(reservations.GetAsync("res_1").Result, Is.Null);
        Assert.That(reservations.CountAsync().Result, Is.EqualTo(0));
    }

    [Test]
    public void Starting_And_Stopping_The_Sweep_At_Once_Never_Throws()
    {
        // StartSweep takes the token inside the lock Stop writes under. Reading
        // it afterwards races a Stop that has already disposed the source.
        for (var i = 0; i < 200; i++)
        {
            var leases = new InMemoryLeaseStore();
            var store = new InMemoryReservationStore(leases);
            Assert.DoesNotThrow(
                () =>
                    Task.WaitAll(
                        Task.Run(() => store.StartSweep()),
                        Task.Run(() => store.Stop())
                    )
            );
            store.Dispose();
        }
    }

    private static ReservationRecord Record() =>
        new()
        {
            Id = "res_1",
            LeaseId = "lse_1",
            CompanyId = "co_1",
            CreditTypeId = "ct_1",
            EventSubtype = "inference_tokens",
            QuantityReserved = 1,
            CreditsReserved = 10,
            ConsumptionRate = 10,
            ExpiresAt = DateTime.UtcNow.AddMinutes(1),
        };

    /// <summary>
    /// Counts which write the store reaches for, so a split back into a
    /// separate expire cannot creep back in.
    /// </summary>
    private sealed class RecordingLeaseRedis : ILeaseRedis
    {
        private readonly ILeaseRedis _inner;

        public RecordingLeaseRedis(ILeaseRedis inner)
        {
            _inner = inner;
        }

        public int HashSetWithExpiryCalls { get; private set; }

        public Task HashSetWithExpiryAsync(
            string key,
            IReadOnlyList<KeyValuePair<string, string>> entries,
            long unixTimeMilliseconds
        )
        {
            HashSetWithExpiryCalls++;
            return _inner.HashSetWithExpiryAsync(key, entries, unixTimeMilliseconds);
        }

        public Task<IReadOnlyDictionary<string, string>> HashGetAllAsync(string key) =>
            _inner.HashGetAllAsync(key);

        public Task<string?> HashGetAsync(string key, string field) =>
            _inner.HashGetAsync(key, field);

        public Task HashSetAsync(string key, IReadOnlyList<KeyValuePair<string, string>> entries) =>
            _inner.HashSetAsync(key, entries);

        public Task HashDeleteAsync(string key, string field) => _inner.HashDeleteAsync(key, field);

        public Task KeyDeleteAsync(string key) => _inner.KeyDeleteAsync(key);

        public Task SortedSetAddAsync(string key, string member, double score) =>
            _inner.SortedSetAddAsync(key, member, score);

        public Task SortedSetRemoveAsync(string key, string member) =>
            _inner.SortedSetRemoveAsync(key, member);

        public Task<IReadOnlyList<string>> SortedSetRangeByScoreAsync(
            string key,
            double min,
            double max,
            long skip,
            long take
        ) => _inner.SortedSetRangeByScoreAsync(key, min, max, skip, take);

        public Task<long> SortedSetLengthAsync(string key) => _inner.SortedSetLengthAsync(key);

        public Task<object?> EvalAsync(string script, string[] keys, string[] args) =>
            _inner.EvalAsync(script, keys, args);
    }
}
