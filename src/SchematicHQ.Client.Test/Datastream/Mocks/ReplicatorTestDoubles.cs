using System.Reflection;
using Microsoft.Extensions.Logging.Testing;
using Moq;
using SchematicHQ.Client.Cache;
using SchematicHQ.Client.Datastream;
using SchematicHQ.Client.Leases;
using StackExchange.Redis;

namespace SchematicHQ.Client.Test.Datastream.Mocks
{
    internal sealed class FakeReplicatorHealthService : IReplicatorHealthService
    {
        public bool IsHealthy { get; set; }
        public string? CacheVersion { get; set; } = "test-cache-version";
        public Task<string?> GetCacheVersionAsync() => Task.FromResult(CacheVersion);
        public event Action<string?, string?>? CacheVersionChanged { add { } remove { } }
        public void Start() { }
        public void Stop() { }
        public void Dispose() { }
    }

    internal sealed class NoopEventBuffer : IEventBuffer<CreateEventRequestBody>
    {
        public void Push(CreateEventRequestBody item) { }
        public void Start() { }
        public Task Stop() => Task.CompletedTask;
        public Task Flush() => Task.CompletedTask;
        public int GetEventCount() => 0;
    }

    internal static class ReplicatorTestHarness
    {
        public static Schematic CreateReplicatorSchematic(
            HttpMessageHandler api,
            ICacheProvider adapterCache,
            IReplicatorHealthService health,
            out DatastreamClientAdapter adapter,
            Dictionary<string, bool>? flagDefaults = null,
            CreditLeaseConfig? creditLeases = null)
        {
            var redis = new Mock<IConnectionMultiplexer>();
            redis.Setup(r => r.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(new Mock<IDatabase>().Object);

            var options = new ClientOptions
            {
                ReplicatorMode = true,
                ReplicatorHealthUrl = "http://127.0.0.1:1/ready",
                CacheProvider = new RedisCache(new RedisCacheConfig { ConnectionMultiplexerFactory = () => redis.Object }),
                EventBuffer = new NoopEventBuffer(),
                FlagDefaults = flagDefaults ?? new Dictionary<string, bool>(),
                CreditLeases = creditLeases,
            };
            var schematic = new Schematic("test-api-key", options.WithHttpClient(new HttpClient(api)));

            adapter = new DatastreamClientAdapter(
                "wss://test.example.com",
                new FakeLogger(),
                "test-api-key",
                adapterCache,
                new DatastreamOptions(),
                replicatorMode: true,
                replicatorHealthUrl: "http://127.0.0.1:1/ready");
            SetReplicatorHealthService(adapter, health);
            ReplaceAdapter(schematic, adapter);
            return schematic;
        }

        public static void SetReplicatorHealthService(DatastreamClientAdapter adapter, IReplicatorHealthService health)
        {
            var field = typeof(DatastreamClientAdapter)
                .GetField("_replicatorHealthService", BindingFlags.NonPublic | BindingFlags.Instance)!;
            (field.GetValue(adapter) as IDisposable)?.Dispose();
            field.SetValue(adapter, health);
        }

        public static void ReplaceAdapter(Schematic schematic, DatastreamClientAdapter? adapter)
        {
            var field = typeof(Schematic).GetField("_datastreamClient", BindingFlags.NonPublic | BindingFlags.Instance)!;
            (field.GetValue(schematic) as DatastreamClientAdapter)?.Close();
            field.SetValue(schematic, adapter);
        }
    }
}
