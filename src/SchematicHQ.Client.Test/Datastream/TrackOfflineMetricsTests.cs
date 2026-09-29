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
    /// Track optimistically bumps the cached company's metrics. That bump should happen
    /// whether or not the replicator reports ready (or the websocket is connected), since
    /// flag checks keep evaluating from the cache in both cases.
    /// </summary>
    [TestFixture]
    public class TrackOfflineMetricsTests
    {
        private const string EventName = "api-call";
        private static readonly Dictionary<string, string> CompanyKeys = new() { { "id", "company-123" } };

        // Before this change the bump never ran in replicator mode, ready or not, because
        // the connected flag it checked is only maintained for websocket mode.
        [TestCase(false)]
        [TestCase(true)]
        public async Task Track_ReplicatorMode_UpdatesCachedCompanyMetric(bool replicatorReady)
        {
            var schematic = CreateReplicatorSchematic(out var adapter, replicatorReady);
            try
            {
                await CacheCompany(adapter, metricValue: 10);
                Assert.That(schematic.IsReplicatorHealthy(), Is.EqualTo(replicatorReady));

                schematic.Track(EventName, company: CompanyKeys, quantity: 3);
                schematic.Track(EventName, company: CompanyKeys, quantity: 2);

                Assert.That(await CachedMetricValue(adapter), Is.EqualTo(15));
            }
            finally
            {
                await schematic.Shutdown();
            }
        }

        [Test]
        public async Task Track_ReplicatorNotReady_CompanyNotCached_WritesNothing()
        {
            var schematic = CreateReplicatorSchematic(out var adapter);
            try
            {
                schematic.Track(EventName, company: CompanyKeys, quantity: 3);

                Assert.That(await InnerClient(adapter).GetCompanyFromCache(CompanyKeys), Is.Null);
            }
            finally
            {
                await schematic.Shutdown();
            }
        }

        [Test]
        public async Task Track_WebsocketDisconnected_UpdatesCachedCompanyMetric()
        {
            var schematic = CreateWebsocketSchematic(out var adapter);
            try
            {
                await CacheCompany(adapter, metricValue: 10);
                Assert.That(await adapter.IsConnectedAsync(), Is.False);

                schematic.Track(EventName, company: CompanyKeys, quantity: 4);

                Assert.That(await CachedMetricValue(adapter), Is.EqualTo(14));
            }
            finally
            {
                await schematic.Shutdown();
            }
        }

        // ---- helpers ----

        /// <summary>
        /// Builds a replicator-mode client whose datastream adapter reads a local cache and
        /// reports the given replicator readiness.
        /// </summary>
        private static Schematic CreateReplicatorSchematic(out DatastreamClientAdapter adapter, bool replicatorReady = false)
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
            var schematic = new Schematic("test-api-key", options.WithHttpClient(new HttpClient(new FailingHttpHandler())));

            adapter = new DatastreamClientAdapter(
                "wss://test.example.com",
                new FakeLogger(),
                "test-api-key",
                new LocalCache(),
                new DatastreamOptions(),
                replicatorMode: true);
            typeof(DatastreamClientAdapter)
                .GetField("_replicatorHealthService", BindingFlags.NonPublic | BindingFlags.Instance)!
                .SetValue(adapter, new FakeReplicatorHealthService { IsHealthy = replicatorReady });

            ReplaceAdapter(schematic, adapter);
            return schematic;
        }

        /// <summary>
        /// Builds a websocket-mode client whose datastream adapter reads a local cache and
        /// has never connected.
        /// </summary>
        private static Schematic CreateWebsocketSchematic(out DatastreamClientAdapter adapter)
        {
            var options = new ClientOptions
            {
                UseDatastream = true,
                BaseUrl = "http://127.0.0.1:1",
                EventBuffer = new NoopEventBuffer(),
            };
            var schematic = new Schematic("test-api-key", options.WithHttpClient(new HttpClient(new FailingHttpHandler())));

            adapter = new DatastreamClientAdapter(
                "wss://test.example.com",
                new FakeLogger(),
                "test-api-key",
                new LocalCache(),
                new DatastreamOptions());

            ReplaceAdapter(schematic, adapter);
            typeof(Schematic)
                .GetField("_datastreamConnected", BindingFlags.NonPublic | BindingFlags.Instance)!
                .SetValue(schematic, false);
            return schematic;
        }

        private static void ReplaceAdapter(Schematic schematic, DatastreamClientAdapter adapter)
        {
            var field = typeof(Schematic).GetField("_datastreamClient", BindingFlags.NonPublic | BindingFlags.Instance)!;
            ((DatastreamClientAdapter)field.GetValue(schematic)!).Close();
            field.SetValue(schematic, adapter);
        }

        private static DatastreamClient InnerClient(DatastreamClientAdapter adapter) =>
            (DatastreamClient)typeof(DatastreamClientAdapter)
                .GetField("_client", BindingFlags.NonPublic | BindingFlags.Instance)!
                .GetValue(adapter)!;

        private static async Task CacheCompany(DatastreamClientAdapter adapter, long metricValue)
        {
            var company = new RulesengineCompany
            {
                Id = "comp_123",
                AccountId = "acc_123",
                EnvironmentId = "env_123",
                Keys = new Dictionary<string, string>(CompanyKeys),
                Metrics = new List<RulesengineCompanyMetric>
                {
                    new RulesengineCompanyMetric
                    {
                        AccountId = "acc_123",
                        EnvironmentId = "env_123",
                        CompanyId = "comp_123",
                        EventSubtype = EventName,
                        Period = RulesengineMetricPeriod.AllTime,
                        MonthReset = RulesengineMetricPeriodMonthReset.FirstOfMonth,
                        Value = metricValue,
                        CreatedAt = DateTime.UtcNow
                    }
                }
            };
            var task = (Task)typeof(DatastreamClient)
                .GetMethod("CacheCompanyForKeys", BindingFlags.NonPublic | BindingFlags.Instance)!
                .Invoke(InnerClient(adapter), new object[] { company })!;
            await task;
        }

        private static async Task<long> CachedMetricValue(DatastreamClientAdapter adapter)
        {
            var company = await InnerClient(adapter).GetCompanyFromCache(CompanyKeys);
            Assert.That(company, Is.Not.Null);
            return company!.Metrics.Single(m => m.EventSubtype == EventName).Value;
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

        private sealed class FailingHttpHandler : HttpMessageHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) =>
                Task.FromResult(new HttpResponseMessage(HttpStatusCode.ServiceUnavailable));
        }
    }
}
