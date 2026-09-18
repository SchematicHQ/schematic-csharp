using NUnit.Framework;
using SchematicHQ.Client.Datastream;
using SchematicHQ.Client.Leases;
using StackExchange.Redis;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// Who closes the lease Redis connection. The SDK closes one it opened itself;
/// a connection the caller manages, including one their multiplexer factory
/// handed back, outlives the client and stays theirs.
/// </summary>
[TestFixture]
public class LeaseRedisOwnershipTests
{
    [Test]
    public void A_Connection_The_Sdk_Opened_Is_Owned_And_Closed_On_Dispose()
    {
        var backend = StackExchangeLeaseRedis.FromConfig(DeadEndpointConfig());

        Assert.That(backend.OwnsConnection, Is.True);
        var multiplexer = backend.OwnedConnection!;

        backend.Dispose();

        // A live connection answers a command or fails to reach the server; a
        // closed one refuses to try.
        Assert.Throws<ObjectDisposedException>(() => multiplexer.GetDatabase().Ping());
    }

    [Test]
    public void A_Connection_From_The_Callers_Factory_Is_Not_Owned_And_Survives_Dispose()
    {
        using var shared = ConnectionMultiplexer.Connect(DeadEndpointOptions());
        var config = DeadEndpointConfig();
        config.ConnectionMultiplexerFactory = () => shared;

        var backend = StackExchangeLeaseRedis.FromConfig(config);
        Assert.That(backend.OwnsConnection, Is.False);

        backend.Dispose();

        // The factory's multiplexer is shared with whatever else the caller
        // points at it, so the lease backend must leave it open. Nothing
        // answers on the endpoint, so the command still fails, but as an
        // unreachable server rather than a closed connection.
        var error = Assert.Catch(() => shared.GetDatabase().Ping());
        Assert.That(error, Is.Not.InstanceOf<ObjectDisposedException>());
    }

    [Test]
    public async Task A_Backend_The_Caller_Supplied_Is_Not_Disposed_On_Shutdown()
    {
        var supplied = new DisposableLeaseRedis();
        var schematic = new Schematic(
            "sch_test",
            new ClientOptions
            {
                EventBuffer = new RecordingEventBuffer(),
                CreditLeases = new CreditLeaseConfig
                {
                    Mode = CreditLeaseMode.Client,
                    RedisClient = supplied,
                },
            }
        );

        await schematic.Shutdown();

        Assert.That(supplied.Disposed, Is.False);
    }

    private static RedisCacheConfig DeadEndpointConfig() =>
        new() { ConfigurationOptions = DeadEndpointOptions() };

    /// <summary>
    /// An endpoint nothing answers on, with the abort disabled so Connect hands
    /// back a multiplexer instead of throwing. These tests are about who closes
    /// it, so it never has to reach a server.
    /// </summary>
    private static ConfigurationOptions DeadEndpointOptions()
    {
        var options = new ConfigurationOptions
        {
            AbortOnConnectFail = false,
            ConnectTimeout = 50,
            ConnectRetry = 0,
        };
        options.EndPoints.Add("127.0.0.1", 1);
        return options;
    }

    private sealed class DisposableLeaseRedis : ILeaseRedis, IDisposable
    {
        private readonly FakeLeaseRedis _inner = new(() => DateTime.UtcNow);

        public bool Disposed { get; private set; }

        public void Dispose() => Disposed = true;

        public Task<IReadOnlyDictionary<string, string>> HashGetAllAsync(string key) =>
            _inner.HashGetAllAsync(key);

        public Task<string?> HashGetAsync(string key, string field) =>
            _inner.HashGetAsync(key, field);

        public Task HashSetAsync(string key, IReadOnlyList<KeyValuePair<string, string>> entries) =>
            _inner.HashSetAsync(key, entries);

        public Task HashSetWithExpiryAsync(
            string key,
            IReadOnlyList<KeyValuePair<string, string>> entries,
            long unixTimeMilliseconds
        ) => _inner.HashSetWithExpiryAsync(key, entries, unixTimeMilliseconds);

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
