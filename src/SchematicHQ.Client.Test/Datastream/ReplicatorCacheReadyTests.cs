using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging.Testing;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using SchematicHQ.Client.Cache;
using SchematicHQ.Client.Datastream;
using StackExchange.Redis;

namespace SchematicHQ.Client.Test.Datastream
{
    /// <summary>
    /// In replicator mode the SDK serves flag checks from the replicator cache only once the
    /// replicator reports the cache ready. Before that, single and bulk flag checks skip the
    /// cache and use the API.
    /// </summary>
    [TestFixture]
    public class ReplicatorCacheReadyTests
    {
        private const string FlagKey = "replicator-flag";
        private static readonly Dictionary<string, string> CompanyKeys = new() { { "id", "company-123" } };

        // ---- not ready ----

        [Test]
        public async Task NotReady_SingleAndBulk_SkipCacheAndUseApi()
        {
            var api = new FakeApiHandler(flagValue: true);
            using var harness = CreateHarness(api, ready: false);
            // The cache would answer false, so a true value can only come from the API.
            await harness.CacheFlag(defaultValue: false);
            await harness.CacheCompany();
            harness.Cache.ResetCounts();

            var single = await harness.Schematic.CheckFlag(FlagKey, company: CompanyKeys);
            var withEntitlement = await harness.Schematic.CheckFlagWithEntitlement(FlagKey, company: CompanyKeys);
            var bulk = await harness.Schematic.CheckFlags(company: CompanyKeys, keys: new[] { FlagKey });

            Assert.That(harness.Schematic.IsCacheReady(), Is.False);
            Assert.That(single, Is.True);
            Assert.That(withEntitlement.Value, Is.True);
            Assert.That(bulk, Has.Count.EqualTo(1));
            Assert.That(bulk[0].Flag, Is.EqualTo(FlagKey));
            Assert.That(bulk[0].Value, Is.True);
            Assert.That(api.SingleRequests, Is.GreaterThan(0), "Single flag checks should use the API");
            Assert.That(api.BulkRequests, Is.EqualTo(1), "Bulk flag checks should use the API");
            Assert.That(harness.Cache.GetCount, Is.EqualTo(0), "The replicator cache must not be read before it is ready");
        }

        [Test]
        public async Task NotReady_ApiFailing_SingleAndBulk_ReturnFlagDefaults()
        {
            var api = new FakeApiHandler(flagValue: false, status: HttpStatusCode.InternalServerError);
            using var harness = CreateHarness(api, ready: false, flagDefaults: new Dictionary<string, bool> { { FlagKey, true } });
            await harness.CacheFlag(defaultValue: false);

            var single = await harness.Schematic.CheckFlag(FlagKey, company: CompanyKeys);
            var bulk = await harness.Schematic.CheckFlags(company: CompanyKeys, keys: new[] { FlagKey });

            Assert.That(single, Is.True);
            Assert.That(bulk, Has.Count.EqualTo(1));
            Assert.That(bulk[0].Value, Is.True);
            Assert.That(api.SingleRequests + api.BulkRequests, Is.GreaterThan(0));
        }

        [Test]
        public void Adapter_NotReady_ThrowsWithoutReadingCache()
        {
            using var harness = CreateHarness(new FakeApiHandler(flagValue: true), ready: false);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await harness.Adapter.CheckFlag(new CheckFlagRequestBody { Company = CompanyKeys }, FlagKey));
            Assert.That(harness.Cache.GetCount, Is.EqualTo(0));
        }

        // ---- ready ----

        [Test]
        public async Task Ready_SingleAndBulk_EvaluateFromCacheWithoutApi_AndAgree()
        {
            var api = new FakeApiHandler(flagValue: false);
            using var harness = CreateHarness(api, ready: true);
            // The API would answer false, so a true value can only come from the cache.
            await harness.CacheFlag(defaultValue: true);
            await harness.CacheCompany();

            var single = await harness.Schematic.CheckFlagWithEntitlement(FlagKey, company: CompanyKeys);
            var bulk = await harness.Schematic.CheckFlags(company: CompanyKeys, keys: new[] { FlagKey });

            Assert.That(harness.Schematic.IsCacheReady(), Is.True);
            Assert.That(single.Value, Is.True);
            Assert.That(single.FlagId, Is.EqualTo("flag_123"));
            Assert.That(single.CompanyId, Is.EqualTo("comp_123"));
            Assert.That(bulk, Has.Count.EqualTo(1));
            Assert.That(bulk[0].Value, Is.EqualTo(single.Value));
            Assert.That(bulk[0].FlagId, Is.EqualTo(single.FlagId));
            Assert.That(bulk[0].CompanyId, Is.EqualTo(single.CompanyId));
            Assert.That(bulk[0].Reason, Is.EqualTo(single.Reason));
            Assert.That(api.SingleRequests + api.BulkRequests, Is.EqualTo(0), "Flag checks should not call the API");
        }

        [Test]
        public async Task Ready_CompanyNotCached_SingleAndBulk_EvaluateWithAvailableData()
        {
            var api = new FakeApiHandler(flagValue: false);
            using var harness = CreateHarness(api, ready: true);
            await harness.CacheFlag(defaultValue: true);

            var single = await harness.Schematic.CheckFlagWithEntitlement(FlagKey, company: CompanyKeys);
            var bulk = await harness.Schematic.CheckFlags(company: CompanyKeys, keys: new[] { FlagKey });

            Assert.That(single.Value, Is.True);
            Assert.That(single.CompanyId, Is.Null);
            Assert.That(bulk[0].Value, Is.EqualTo(single.Value));
            Assert.That(bulk[0].Reason, Is.EqualTo(single.Reason));
            Assert.That(api.SingleRequests + api.BulkRequests, Is.EqualTo(0));
        }

        [Test]
        public async Task Ready_FlagNotCached_SingleAndBulk_FallBackToApi()
        {
            var api = new FakeApiHandler(flagValue: true);
            using var harness = CreateHarness(api, ready: true);

            var single = await harness.Schematic.CheckFlag(FlagKey, company: CompanyKeys);
            var bulk = await harness.Schematic.CheckFlags(company: CompanyKeys, keys: new[] { FlagKey });

            Assert.That(single, Is.True);
            Assert.That(bulk[0].Value, Is.True);
            Assert.That(api.SingleRequests, Is.GreaterThan(0));
            Assert.That(api.BulkRequests, Is.EqualTo(1));
        }

        [Test]
        public async Task ReadinessChange_SwitchesBetweenApiAndCache()
        {
            var api = new FakeApiHandler(flagValue: false);
            using var harness = CreateHarness(api, ready: false);
            await harness.CacheFlag(defaultValue: true);

            var beforeReady = await harness.Schematic.CheckFlags(keys: new[] { FlagKey });
            harness.Health.IsHealthy = true;
            var afterReady = await harness.Schematic.CheckFlags(keys: new[] { FlagKey });

            Assert.That(beforeReady[0].Value, Is.False, "Not ready: value comes from the API");
            Assert.That(afterReady[0].Value, Is.True, "Ready: value comes from the cache");
            Assert.That(api.BulkRequests, Is.EqualTo(1));
        }

        [Test]
        public void IsCacheReady_ReflectsReplicatorReadiness()
        {
            using var harness = CreateHarness(new FakeApiHandler(flagValue: true), ready: false);

            Assert.That(harness.Adapter.IsCacheReady(), Is.False);
            Assert.That(harness.Schematic.IsCacheReady(), Is.False);

            harness.Health.IsHealthy = true;

            Assert.That(harness.Adapter.IsCacheReady(), Is.True);
            Assert.That(harness.Schematic.IsCacheReady(), Is.True);
            Assert.That(harness.Schematic.IsReplicatorHealthy(), Is.True);
        }

        [Test]
        public void IsCacheReady_OutsideReplicatorMode_IsTrue()
        {
            var adapter = new DatastreamClientAdapter(
                "wss://test.example.com", new FakeLogger(), "test-api-key", new LocalCache(), new DatastreamOptions());
            try
            {
                Assert.That(adapter.IsCacheReady(), Is.True);
                Assert.That(adapter.IsReplicatorReady(), Is.False);
            }
            finally
            {
                adapter.Close();
            }
        }

        // ---- health poll ----

        [Test]
        public async Task HealthCheck_503NotReady_SetsNotReadyAndRecordsCacheVersion()
        {
            var responses = new Queue<Func<HttpResponseMessage>>();
            responses.Enqueue(() => Json(HttpStatusCode.OK, """{"ready": true, "cache_version": "v1"}"""));
            responses.Enqueue(() => Json(HttpStatusCode.ServiceUnavailable, """{"ready": false, "cache_version": "vX"}"""));
            using var service = CreateHealthService(responses);

            await CheckHealth(service);
            Assert.That(service.IsHealthy, Is.True);
            Assert.That(service.CacheVersion, Is.EqualTo("v1"));

            await CheckHealth(service);
            Assert.That(service.IsHealthy, Is.False);
            Assert.That(service.CacheVersion, Is.EqualTo("vX"));
        }

        [Test]
        public async Task HealthCheck_ResponseWithoutCacheVersion_KeepsPreviousVersion()
        {
            var responses = new Queue<Func<HttpResponseMessage>>();
            responses.Enqueue(() => Json(HttpStatusCode.OK, """{"ready": true, "cache_version": "v1"}"""));
            responses.Enqueue(() => Json(HttpStatusCode.ServiceUnavailable, """{"ready": false}"""));
            using var service = CreateHealthService(responses);

            await CheckHealth(service);
            await CheckHealth(service);

            Assert.That(service.IsHealthy, Is.False);
            Assert.That(service.CacheVersion, Is.EqualTo("v1"));
        }

        [Test]
        public async Task HealthCheck_Unreachable_SetsNotReadyAndKeepsCacheVersion()
        {
            var responses = new Queue<Func<HttpResponseMessage>>();
            responses.Enqueue(() => Json(HttpStatusCode.OK, """{"ready": true, "cache_version": "v1"}"""));
            responses.Enqueue(() => throw new HttpRequestException("Connection refused"));
            using var service = CreateHealthService(responses);

            await CheckHealth(service);
            Assert.That(service.IsHealthy, Is.True);

            await CheckHealth(service);
            Assert.That(service.IsHealthy, Is.False);
            Assert.That(service.CacheVersion, Is.EqualTo("v1"));
        }

        [Test]
        public async Task HealthCheck_Timeout_SetsNotReadyAndKeepsCacheVersion()
        {
            var responses = new Queue<Func<HttpResponseMessage>>();
            responses.Enqueue(() => Json(HttpStatusCode.OK, """{"ready": true, "cache_version": "v1"}"""));
            // HttpClient reports a request timeout as a TaskCanceledException
            responses.Enqueue(() => throw new TaskCanceledException("The request timed out"));
            using var service = CreateHealthService(responses);

            await CheckHealth(service);
            await CheckHealth(service);

            Assert.That(service.IsHealthy, Is.False);
            Assert.That(service.CacheVersion, Is.EqualTo("v1"));
        }

        [Test]
        public async Task HealthCheck_UnparseableBody_SetsNotReadyAndKeepsCacheVersion()
        {
            var responses = new Queue<Func<HttpResponseMessage>>();
            responses.Enqueue(() => Json(HttpStatusCode.OK, """{"ready": true, "cache_version": "v1"}"""));
            responses.Enqueue(() => Json(HttpStatusCode.OK, "not json"));
            using var service = CreateHealthService(responses);

            await CheckHealth(service);
            await CheckHealth(service);

            Assert.That(service.IsHealthy, Is.False);
            Assert.That(service.CacheVersion, Is.EqualTo("v1"));
        }

        // ---- helpers ----

        private static HttpResponseMessage Json(HttpStatusCode status, string body) =>
            new HttpResponseMessage(status) { Content = new StringContent(body, Encoding.UTF8, "application/json") };

        private static ReplicatorHealthService CreateHealthService(Queue<Func<HttpResponseMessage>> responses)
        {
            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns(() => Task.FromResult(responses.Dequeue()()));
            return new ReplicatorHealthService(new HttpClient(handler.Object), "http://test/ready", new FakeLogger());
        }

        /// <summary>Runs one health poll synchronously, as the background loop would.</summary>
        private static Task CheckHealth(ReplicatorHealthService service) =>
            (Task)typeof(ReplicatorHealthService)
                .GetMethod("PerformHealthCheck", BindingFlags.NonPublic | BindingFlags.Instance)!
                .Invoke(service, null)!;

        private static Harness CreateHarness(FakeApiHandler api, bool ready, Dictionary<string, bool>? flagDefaults = null)
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
                FlagDefaults = flagDefaults ?? new Dictionary<string, bool>(),
            };
            var schematic = new Schematic("test-api-key", options.WithHttpClient(new HttpClient(api)));

            // Swap in an adapter backed by a local cache and a controllable health service.
            var field = typeof(Schematic).GetField("_datastreamClient", BindingFlags.NonPublic | BindingFlags.Instance)!;
            ((DatastreamClientAdapter)field.GetValue(schematic)!).Close();

            var cache = new CountingCache(new LocalCache());
            var health = new FakeReplicatorHealthService { IsHealthy = ready };
            var adapter = new DatastreamClientAdapter(
                "wss://test.example.com", new FakeLogger(), "test-api-key", cache, new DatastreamOptions(), replicatorMode: true);
            typeof(DatastreamClientAdapter)
                .GetField("_replicatorHealthService", BindingFlags.NonPublic | BindingFlags.Instance)!
                .SetValue(adapter, health);
            field.SetValue(schematic, adapter);

            return new Harness(schematic, adapter, health, cache);
        }

        private sealed class Harness : IDisposable
        {
            public Harness(Schematic schematic, DatastreamClientAdapter adapter, FakeReplicatorHealthService health, CountingCache cache)
            {
                Schematic = schematic;
                Adapter = adapter;
                Health = health;
                Cache = cache;
            }

            public Schematic Schematic { get; }
            public DatastreamClientAdapter Adapter { get; }
            public FakeReplicatorHealthService Health { get; }
            public CountingCache Cache { get; }

            private DatastreamClient Inner =>
                (DatastreamClient)typeof(DatastreamClientAdapter)
                    .GetField("_client", BindingFlags.NonPublic | BindingFlags.Instance)!
                    .GetValue(Adapter)!;

            /// <summary>Writes the flag under the versioned key the replicator uses.</summary>
            public async Task CacheFlag(bool defaultValue)
            {
                var flagsCache = (ICacheProvider)typeof(DatastreamClient)
                    .GetField("_flagsCache", BindingFlags.NonPublic | BindingFlags.Instance)!
                    .GetValue(Inner)!;
                var cacheKey = (string)typeof(DatastreamClient)
                    .GetMethod("FlagCacheKey", BindingFlags.NonPublic | BindingFlags.Instance)!
                    .Invoke(Inner, new object[] { FlagKey })!;

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

            /// <summary>Writes the company under each of its versioned key lookups.</summary>
            public async Task CacheCompany()
            {
                var company = new RulesengineCompany
                {
                    Id = "comp_123",
                    AccountId = "acc_123",
                    EnvironmentId = "env_123",
                    Keys = new Dictionary<string, string>(CompanyKeys)
                };
                await (Task)typeof(DatastreamClient)
                    .GetMethod("CacheCompanyForKeys", BindingFlags.NonPublic | BindingFlags.Instance)!
                    .Invoke(Inner, new object[] { company })!;
            }

            public void Dispose()
            {
                Schematic.Shutdown().GetAwaiter().GetResult();
            }
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

        /// <summary>Counts reads so tests can assert the replicator cache was not consulted.</summary>
        private sealed class CountingCache : ICacheProvider
        {
            private readonly ICacheProvider _inner;
            private int _getCount;

            public CountingCache(ICacheProvider inner) => _inner = inner;

            public int GetCount => _getCount;
            public void ResetCounts() => Interlocked.Exchange(ref _getCount, 0);

            public ValueTask<T?> Get<T>(string key, CancellationToken token = default) where T : notnull
            {
                Interlocked.Increment(ref _getCount);
                return _inner.Get<T>(key, token);
            }

            public ValueTask Set<T>(string key, T val, TimeSpan? ttlOverride = null, CancellationToken token = default) where T : notnull =>
                _inner.Set(key, val, ttlOverride, token);

            public ValueTask<T> GetOrSet<T>(string key, Func<CancellationToken, Task<T>> factory, TimeSpan? ttlOverride = null, CancellationToken token = default) where T : notnull
            {
                Interlocked.Increment(ref _getCount);
                return _inner.GetOrSet(key, factory, ttlOverride, token);
            }

            public ValueTask<bool> Delete(string key, CancellationToken token = default) => _inner.Delete(key, token);

            public ValueTask DeleteMissing(IEnumerable<string> keys, string? scanPattern = null) => _inner.DeleteMissing(keys, scanPattern);
        }

        private sealed class NoopEventBuffer : IEventBuffer<CreateEventRequestBody>
        {
            public void Push(CreateEventRequestBody item) { }
            public void Start() { }
            public Task Stop() => Task.CompletedTask;
            public Task Flush() => Task.CompletedTask;
            public int GetEventCount() => 0;
        }

        /// <summary>Answers the single and bulk flag check endpoints and counts calls to each.</summary>
        private sealed class FakeApiHandler : HttpMessageHandler
        {
            private readonly bool _flagValue;
            private readonly HttpStatusCode _status;
            private int _singleRequests;
            private int _bulkRequests;

            public FakeApiHandler(bool flagValue, HttpStatusCode status = HttpStatusCode.OK)
            {
                _flagValue = flagValue;
                _status = status;
            }

            public int SingleRequests => _singleRequests;
            public int BulkRequests => _bulkRequests;

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var path = request.RequestUri!.AbsolutePath;
                object body;
                if (path.EndsWith("/flags/check"))
                {
                    Interlocked.Increment(ref _bulkRequests);
                    body = new CheckFlagsResponse
                    {
                        Data = new CheckFlagsResponseData
                        {
                            Flags = new List<CheckFlagResponseData>
                            {
                                new() { Flag = FlagKey, Value = _flagValue, Reason = "api" }
                            }
                        }
                    };
                }
                else if (path.EndsWith($"/flags/{FlagKey}/check"))
                {
                    Interlocked.Increment(ref _singleRequests);
                    body = new CheckFlagResponse
                    {
                        Data = new CheckFlagResponseData { Flag = FlagKey, Value = _flagValue, Reason = "api" }
                    };
                }
                else
                {
                    return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
                }

                if (_status != HttpStatusCode.OK)
                {
                    return Task.FromResult(new HttpResponseMessage(_status) { Content = new StringContent("{}") });
                }
                return Task.FromResult(Json(HttpStatusCode.OK, JsonSerializer.Serialize(body)));
            }
        }
    }
}
