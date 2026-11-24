using System.Net;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Logging.Testing;
using Moq;
using NUnit.Framework;
using SchematicHQ.Client.Cache;
using SchematicHQ.Client.Datastream;
using StackExchange.Redis;

namespace SchematicHQ.Client.Test
{
    [TestFixture]
    public class IsReadyTests
    {
        private const string WhoAmIBody =
            """{"data":{"account_id":"acc_1","account_name":"Test","actor_type":"api_key","onboarding_complete":true,"environments":[]},"params":{}}""";

        private Schematic? _schematic;

        [TearDown]
        public async Task TearDown()
        {
            if (_schematic != null) await _schematic.Shutdown();
            _schematic = null;
        }

        [Test]
        public async Task Offline_ReturnsTrueWithoutRequest()
        {
            var api = new RecordingHandler(_ => Respond(HttpStatusCode.InternalServerError));
            _schematic = new Schematic(
                "test-api-key",
                new ClientOptions { Offline = true, EventBuffer = new NoopEventBuffer() }.WithHttpClient(new HttpClient(api)));

            Assert.That(await _schematic.IsReadyAsync(), Is.True);
            Assert.That(api.Requests, Is.Empty);
        }

        [Test]
        public async Task DatastreamUsable_ReturnsTrueWithoutRequest()
        {
            var api = new RecordingHandler(_ => Respond(HttpStatusCode.InternalServerError));
            _schematic = CreateReplicatorSchematic(api, replicatorReady: true);

            Assert.That(await _schematic.IsReadyAsync(), Is.True);
            Assert.That(api.Requests, Is.Empty);
        }

        [Test]
        public async Task DatastreamNotUsable_CallsWhoAmI()
        {
            var api = new RecordingHandler(_ => Respond(HttpStatusCode.OK, WhoAmIBody));
            _schematic = CreateReplicatorSchematic(api, replicatorReady: false);

            Assert.That(await _schematic.IsReadyAsync(), Is.True);
            Assert.That(api.Requests, Is.EqualTo(new[] { "/whoami" }));
        }

        [Test]
        public async Task WebsocketConnected_ReturnsTrueWithoutRequest()
        {
            var api = new RecordingHandler(_ => Respond(HttpStatusCode.InternalServerError));
            _schematic = CreateWebsocketSchematic(api, connected: true);

            Assert.That(await _schematic.IsReadyAsync(), Is.True);
            Assert.That(api.Requests, Is.Empty);
        }

        [Test]
        public async Task WebsocketNotConnected_CallsWhoAmI()
        {
            var api = new RecordingHandler(_ => Respond(HttpStatusCode.OK, WhoAmIBody));
            _schematic = CreateWebsocketSchematic(api, connected: false);

            Assert.That(await _schematic.IsReadyAsync(), Is.True);
            Assert.That(api.Requests, Is.EqualTo(new[] { "/whoami" }));
        }

        [Test]
        public async Task ApiMode_WhoAmISucceeds_ReturnsTrue()
        {
            var api = new RecordingHandler(_ => Respond(HttpStatusCode.OK, WhoAmIBody));
            _schematic = CreateApiSchematic(api);

            Assert.That(await _schematic.IsReadyAsync(), Is.True);
            Assert.That(api.Requests, Is.EqualTo(new[] { "/whoami" }));
        }

        [Test]
        public async Task ApiMode_WhoAmIUnauthorized_ReturnsFalse()
        {
            var api = new RecordingHandler(_ => Respond(HttpStatusCode.Unauthorized, """{"error":"invalid api key"}"""));
            _schematic = CreateApiSchematic(api);

            Assert.That(await _schematic.IsReadyAsync(), Is.False);
        }

        [Test]
        public async Task ApiMode_ServerError_ReturnsFalseWithoutRetry()
        {
            var api = new RecordingHandler(_ => Respond(HttpStatusCode.InternalServerError));
            _schematic = CreateApiSchematic(api);

            Assert.That(await _schematic.IsReadyAsync(), Is.False);
            Assert.That(api.Requests, Is.EqualTo(new[] { "/whoami" }));
        }

        [Test]
        public async Task ApiMode_IgnoresClientTimeout()
        {
            var api = new RecordingHandler(async token =>
            {
                await Task.Delay(TimeSpan.FromMilliseconds(300), token);
                return Respond(HttpStatusCode.OK, WhoAmIBody);
            });
            _schematic = CreateApiSchematic(api, timeout: TimeSpan.FromMilliseconds(50));

            Assert.That(await _schematic.IsReadyAsync(), Is.True);
        }

        [Test]
        public async Task ApiMode_NetworkError_ReturnsFalse()
        {
            var api = new RecordingHandler(_ => Task.FromException<HttpResponseMessage>(new HttpRequestException("Connection refused")));
            _schematic = CreateApiSchematic(api);

            Assert.That(await _schematic.IsReadyAsync(), Is.False);
        }

        [Test]
        public async Task ApiMode_Timeout_ReturnsFalse()
        {
            var api = new RecordingHandler(async token =>
            {
                await Task.Delay(TimeSpan.FromSeconds(10), token);
                return Respond(HttpStatusCode.OK, WhoAmIBody);
            });
            _schematic = CreateApiSchematic(api, timeout: TimeSpan.FromMinutes(1));

            var started = DateTime.UtcNow;
            Assert.That(await _schematic.IsReadyAsync(), Is.False);
            Assert.That(DateTime.UtcNow - started, Is.LessThan(TimeSpan.FromSeconds(8)));
        }

        [Test]
        public async Task ApiMode_CallerCancels_ReturnsFalse()
        {
            var api = new RecordingHandler(async token =>
            {
                await Task.Delay(TimeSpan.FromSeconds(10), token);
                return Respond(HttpStatusCode.OK, WhoAmIBody);
            });
            _schematic = CreateApiSchematic(api);

            using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(100));
            Assert.That(await _schematic.IsReadyAsync(cts.Token), Is.False);
        }

        private static Schematic CreateApiSchematic(RecordingHandler api, TimeSpan? timeout = null)
        {
            var options = new ClientOptions
            {
                EventBuffer = new NoopEventBuffer(),
                Timeout = timeout ?? TimeSpan.FromSeconds(30),
            };
            return new Schematic("test-api-key", options.WithHttpClient(new HttpClient(api)));
        }

        private static Schematic CreateReplicatorSchematic(RecordingHandler api, bool replicatorReady)
        {
            var redis = new Mock<IConnectionMultiplexer>();
            redis.Setup(r => r.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(new Mock<IDatabase>().Object);

            var options = new ClientOptions
            {
                ReplicatorMode = true,
                ReplicatorHealthUrl = "http://127.0.0.1:1/ready",
                CacheProvider = new RedisCache(new RedisCacheConfig { ConnectionMultiplexerFactory = () => redis.Object }),
                EventBuffer = new NoopEventBuffer(),
            };
            var schematic = new Schematic("test-api-key", options.WithHttpClient(new HttpClient(api)));

            var adapter = new DatastreamClientAdapter(
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

        private static Schematic CreateWebsocketSchematic(RecordingHandler api, bool connected)
        {
            var options = new ClientOptions
            {
                UseDatastream = true,
                BaseUrl = "http://127.0.0.1:1",
                EventBuffer = new NoopEventBuffer(),
            };
            var schematic = new Schematic("test-api-key", options.WithHttpClient(new HttpClient(api)));

            var adapter = new DatastreamClientAdapter(
                "wss://test.example.com",
                new FakeLogger(),
                "test-api-key",
                new LocalCache(),
                new DatastreamOptions());
            var tracker = typeof(DatastreamClientAdapter)
                .GetField("_connectionTracker", BindingFlags.NonPublic | BindingFlags.Instance)!
                .GetValue(adapter)!;
            tracker.GetType().GetMethod("UpdateConnectionState")!.Invoke(tracker, new object[] { connected });

            ReplaceAdapter(schematic, adapter);
            return schematic;
        }

        private static void ReplaceAdapter(Schematic schematic, DatastreamClientAdapter adapter)
        {
            var field = typeof(Schematic).GetField("_datastreamClient", BindingFlags.NonPublic | BindingFlags.Instance)!;
            ((DatastreamClientAdapter)field.GetValue(schematic)!).Close();
            field.SetValue(schematic, adapter);
        }

        private static HttpResponseMessage Respond(HttpStatusCode status, string body = "{}") =>
            new HttpResponseMessage(status) { Content = new StringContent(body, Encoding.UTF8, "application/json") };

        private sealed class RecordingHandler : HttpMessageHandler
        {
            private readonly Func<CancellationToken, Task<HttpResponseMessage>> _respond;
            private readonly List<string> _requests = new();

            public RecordingHandler(Func<CancellationToken, HttpResponseMessage> respond)
                : this(token => Task.FromResult(respond(token))) { }

            public RecordingHandler(Func<CancellationToken, Task<HttpResponseMessage>> respond) => _respond = respond;

            public IReadOnlyList<string> Requests
            {
                get { lock (_requests) return _requests.ToList(); }
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                lock (_requests) _requests.Add(request.RequestUri!.AbsolutePath);
                return _respond(cancellationToken);
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

        private sealed class NoopEventBuffer : IEventBuffer<CreateEventRequestBody>
        {
            public void Push(CreateEventRequestBody item) { }
            public void Start() { }
            public Task Stop() => Task.CompletedTask;
            public Task Flush() => Task.CompletedTask;
            public int GetEventCount() => 0;
        }
    }
}
