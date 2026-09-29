using System.Net;
using System.Reflection;
using Microsoft.Extensions.Logging.Testing;
using Moq;
using NUnit.Framework;
using SchematicHQ.Client.Cache;
using SchematicHQ.Client.Datastream;
using StackExchange.Redis;

namespace SchematicHQ.Client.Test.Datastream
{
    /// <summary>
    /// In replicator mode a replicator that reports ready: false (for example because
    /// Schematic is unreachable) still serves its last known state from Redis. Flag checks
    /// should evaluate from that cache rather than falling back to the API.
    /// </summary>
    [TestFixture]
    public class ReplicatorNotReadyCheckFlagTests
    {
        private const string FlagKey = "replicator-flag";
        private static readonly Dictionary<string, string> CompanyKeys = new() { { "id", "company-123" } };

        private FakeLogger _logger = null!;
        private FakeReplicatorHealthService _health = null!;
        private DatastreamClientAdapter _adapter = null!;

        [SetUp]
        public void Setup()
        {
            _logger = new FakeLogger();
            _health = new FakeReplicatorHealthService { IsHealthy = false };
            _adapter = CreateReplicatorAdapter(_logger, _health);
        }

        [TearDown]
        public void TearDown()
        {
            _adapter.Close();
            _health.Dispose();
        }

        [Test]
        public async Task CheckFlag_NotReady_FlagAndCompanyCached_EvaluatesFromCache()
        {
            await CacheFlag(_adapter, defaultValue: true);
            await CacheCompany(_adapter);

            var result = await _adapter.CheckFlag(new CheckFlagRequestBody { Company = CompanyKeys }, FlagKey);

            Assert.That(result.Value, Is.True);
            Assert.That(result.FlagId, Is.EqualTo("flag_123"));
            Assert.That(result.CompanyId, Is.EqualTo("comp_123"));
        }

        [Test]
        public async Task CheckFlag_NotReady_CompanyNotCached_EvaluatesFromCacheInsteadOfThrowing()
        {
            // Only the flag is cached. Before this change a not-ready replicator threw here,
            // which sent the caller to the API even though the flag could be evaluated.
            await CacheFlag(_adapter, defaultValue: true);

            var result = await _adapter.CheckFlag(new CheckFlagRequestBody { Company = CompanyKeys }, FlagKey);

            Assert.That(result.Value, Is.True);
            Assert.That(result.FlagId, Is.EqualTo("flag_123"));
            Assert.That(result.CompanyId, Is.Null);
        }

        [Test]
        public async Task CheckFlag_NotReadyAndReady_CompanyNotCached_BehaveTheSame()
        {
            await CacheFlag(_adapter, defaultValue: true);
            var request = new CheckFlagRequestBody { Company = CompanyKeys };

            var notReady = await _adapter.CheckFlag(request, FlagKey);
            _health.IsHealthy = true;
            var ready = await _adapter.CheckFlag(request, FlagKey);

            Assert.That(notReady.Value, Is.EqualTo(ready.Value));
            Assert.That(notReady.FlagId, Is.EqualTo(ready.FlagId));
            Assert.That(notReady.Reason, Is.EqualTo(ready.Reason));
        }

        [Test]
        public void CheckFlag_NotReady_FlagNotCached_ThrowsForApiFallback()
        {
            // Nothing to evaluate locally, so the caller should still fall back to the API.
            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await _adapter.CheckFlag(new CheckFlagRequestBody { Company = CompanyKeys }, FlagKey));
        }

        [Test]
        public async Task Schematic_CheckFlagAndCheckFlags_NotReady_EvaluateFromCacheWithoutCallingApi()
        {
            var handler = new RecordingHttpHandler();
            var schematic = CreateReplicatorSchematic(handler, out var adapter, out _);
            try
            {
                await CacheFlag(adapter, defaultValue: true);

                // SDK-level default is false, so a true value can only come from the cached flag.
                var single = await schematic.CheckFlag(FlagKey, company: CompanyKeys);
                var bulk = await schematic.CheckFlags(company: CompanyKeys, keys: new[] { FlagKey });

                Assert.That(schematic.IsReplicatorHealthy(), Is.False);
                Assert.That(single, Is.True);
                Assert.That(bulk, Has.Count.EqualTo(1));
                Assert.That(bulk[0].Flag, Is.EqualTo(FlagKey));
                Assert.That(bulk[0].Value, Is.True);
                Assert.That(bulk[0].FlagId, Is.EqualTo("flag_123"));
                Assert.That(handler.RequestCount, Is.EqualTo(0), "Flag checks should not fall back to the API");
            }
            finally
            {
                await schematic.Shutdown();
            }
        }

        // ---- helpers ----

        private static DatastreamClientAdapter CreateReplicatorAdapter(FakeLogger logger, IReplicatorHealthService health)
        {
            // No health URL, so the adapter does not start a real health poller; the fake is injected instead.
            var adapter = new DatastreamClientAdapter(
                "wss://test.example.com",
                logger,
                "test-api-key",
                new LocalCache(),
                new DatastreamOptions(),
                replicatorMode: true);
            typeof(DatastreamClientAdapter)
                .GetField("_replicatorHealthService", BindingFlags.NonPublic | BindingFlags.Instance)!
                .SetValue(adapter, health);
            return adapter;
        }

        /// <summary>
        /// Builds a replicator-mode Schematic client whose datastream adapter reads a local
        /// cache and reports the replicator as not ready.
        /// </summary>
        private static Schematic CreateReplicatorSchematic(
            RecordingHttpHandler handler,
            out DatastreamClientAdapter adapter,
            out FakeReplicatorHealthService health)
        {
            // Replicator mode requires a Redis cache; a mocked multiplexer satisfies that without a server.
            var redis = new Mock<IConnectionMultiplexer>();
            redis.Setup(r => r.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(new Mock<IDatabase>().Object);

            var options = new ClientOptions
            {
                ReplicatorMode = true,
                ReplicatorHealthUrl = "http://127.0.0.1:1/ready",
                CacheProvider = new RedisCache(new RedisCacheConfig { ConnectionMultiplexerFactory = () => redis.Object }),
                EventBuffer = new NoopEventBuffer(),
            };
            var schematic = new Schematic("test-api-key", options.WithHttpClient(new HttpClient(handler)));

            var field = typeof(Schematic).GetField("_datastreamClient", BindingFlags.NonPublic | BindingFlags.Instance)!;
            ((DatastreamClientAdapter)field.GetValue(schematic)!).Close();

            health = new FakeReplicatorHealthService { IsHealthy = false };
            adapter = CreateReplicatorAdapter(new FakeLogger(), health);
            field.SetValue(schematic, adapter);
            return schematic;
        }

        private static DatastreamClient InnerClient(DatastreamClientAdapter adapter) =>
            (DatastreamClient)typeof(DatastreamClientAdapter)
                .GetField("_client", BindingFlags.NonPublic | BindingFlags.Instance)!
                .GetValue(adapter)!;

        private static async Task CacheFlag(DatastreamClientAdapter adapter, bool defaultValue)
        {
            var client = InnerClient(adapter);
            var flagsCache = (ICacheProvider)typeof(DatastreamClient)
                .GetField("_flagsCache", BindingFlags.NonPublic | BindingFlags.Instance)!
                .GetValue(client)!;
            var cacheKey = (string)typeof(DatastreamClient)
                .GetMethod("FlagCacheKey", BindingFlags.NonPublic | BindingFlags.Instance)!
                .Invoke(client, new object[] { FlagKey })!;

            await flagsCache.Set(cacheKey, new RulesengineFlag
            {
                Id = "flag_123",
                Key = FlagKey,
                AccountId = "acc_123",
                EnvironmentId = "env_123",
                DefaultValue = defaultValue,
                Rules = new List<RulesengineRule>()
            });
        }

        private static async Task CacheCompany(DatastreamClientAdapter adapter)
        {
            var client = InnerClient(adapter);
            var company = new RulesengineCompany
            {
                Id = "comp_123",
                AccountId = "acc_123",
                EnvironmentId = "env_123",
                Keys = new Dictionary<string, string>(CompanyKeys)
            };
            var task = (Task)typeof(DatastreamClient)
                .GetMethod("CacheCompanyForKeys", BindingFlags.NonPublic | BindingFlags.Instance)!
                .Invoke(client, new object[] { company })!;
            await task;
        }

        private sealed class FakeReplicatorHealthService : IReplicatorHealthService
        {
            public bool IsHealthy { get; set; }
            public string? CacheVersion { get; set; } = "test-cache-version";
            public Task<string?> GetCacheVersionAsync() => Task.FromResult(CacheVersion);
            public event Action<string?, string?>? CacheVersionChanged { add { } remove { } }
            public void Start() { }
            public void Stop() { }
            public void Dispose() { }
        }

        private sealed class NoopEventBuffer : IEventBuffer<CreateEventRequestBody>
        {
            public void Push(CreateEventRequestBody item) { }
            public void Start() { }
            public Task Stop() => Task.CompletedTask;
            public Task Flush() => Task.CompletedTask;
            public int GetEventCount() => 0;
        }

        private sealed class RecordingHttpHandler : HttpMessageHandler
        {
            private int _requestCount;
            public int RequestCount => _requestCount;

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                Interlocked.Increment(ref _requestCount);
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.ServiceUnavailable));
            }
        }
    }
}
