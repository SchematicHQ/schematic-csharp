using System.Net;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Logging.Testing;
using NUnit.Framework;
using SchematicHQ.Client.Cache;
using SchematicHQ.Client.Datastream;
using SchematicHQ.Client.Test.Datastream.Mocks;

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
        public async Task ReplicatorReady_ReturnsTrueWithoutRequest()
        {
            var api = new RecordingHandler(_ => Respond(HttpStatusCode.InternalServerError));
            _schematic = CreateReplicatorSchematic(api, replicatorReady: true);

            Assert.That(await _schematic.IsReadyAsync(), Is.True);
            Assert.That(api.Requests, Is.Empty);
        }

        [Test]
        public async Task ReplicatorNotReady_CallsWhoAmI()
        {
            var api = new RecordingHandler(_ => Respond(HttpStatusCode.OK, WhoAmIBody));
            _schematic = CreateReplicatorSchematic(api, replicatorReady: false);

            Assert.That(await _schematic.IsReadyAsync(), Is.True);
            Assert.That(api.Requests, Is.EqualTo(new[] { "/whoami" }));
        }

        [Test]
        public async Task WebsocketSnapshotReceived_ReturnsTrueWithoutRequest()
        {
            var api = new RecordingHandler(_ => Respond(HttpStatusCode.InternalServerError));
            _schematic = CreateWebsocketSchematic(api, connected: true, snapshotReceived: true);

            Assert.That(await _schematic.IsReadyAsync(), Is.True);
            Assert.That(api.Requests, Is.Empty);
        }

        [Test]
        public async Task WebsocketConnectedWithoutSnapshot_CallsWhoAmI()
        {
            var api = new RecordingHandler(_ => Respond(HttpStatusCode.OK, WhoAmIBody));
            _schematic = CreateWebsocketSchematic(api, connected: true, snapshotReceived: false);

            Assert.That(await _schematic.IsReadyAsync(), Is.True);
            Assert.That(api.Requests, Is.EqualTo(new[] { "/whoami" }));
        }

        [Test]
        public async Task WebsocketNotConnected_CallsWhoAmI()
        {
            var api = new RecordingHandler(_ => Respond(HttpStatusCode.OK, WhoAmIBody));
            _schematic = CreateWebsocketSchematic(api, connected: false, snapshotReceived: false);

            Assert.That(await _schematic.IsReadyAsync(), Is.True);
            Assert.That(api.Requests, Is.EqualTo(new[] { "/whoami" }));
        }

        [Test]
        public async Task RulesEngineUnavailable_CallsWhoAmI()
        {
            var api = new RecordingHandler(_ => Respond(HttpStatusCode.OK, WhoAmIBody));
            _schematic = CreateWebsocketSchematic(api, connected: true, snapshotReceived: true);
            var inner = typeof(DatastreamClientAdapter)
                .GetField("_client", BindingFlags.NonPublic | BindingFlags.Instance)!
                .GetValue(_schematic.DatastreamClient)!;
            typeof(DatastreamClient)
                .GetField("_rulesEngine", BindingFlags.NonPublic | BindingFlags.Instance)!
                .SetValue(inner, null);

            Assert.That(await _schematic.IsReadyAsync(), Is.True);
            Assert.That(api.Requests, Is.EqualTo(new[] { "/whoami" }));
        }

        [Test]
        public async Task DatastreamFailedToStart_CallsWhoAmI()
        {
            var api = new RecordingHandler(_ => Respond(HttpStatusCode.OK, WhoAmIBody));
            _schematic = CreateWebsocketSchematic(api, connected: true, snapshotReceived: true);
            ReplicatorTestHarness.ReplaceAdapter(_schematic, null);

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
            _schematic = CreateApiSchematic(api, clientTimeout: TimeSpan.FromMilliseconds(50));

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
            _schematic = CreateApiSchematic(api, clientTimeout: TimeSpan.FromMinutes(1));
            _schematic._readinessTimeout = TimeSpan.FromMilliseconds(100);

            var started = DateTime.UtcNow;
            Assert.That(await _schematic.IsReadyAsync(), Is.False);
            Assert.That(DateTime.UtcNow - started, Is.LessThan(TimeSpan.FromSeconds(2)));
        }

        [Test]
        public async Task ApiMode_SlowBody_TimesOutWholeCall()
        {
            var api = new RecordingHandler(_ => Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(new StallingStream()),
            }));
            _schematic = CreateApiSchematic(api, clientTimeout: TimeSpan.FromMinutes(1));
            _schematic._readinessTimeout = TimeSpan.FromMilliseconds(100);

            var started = DateTime.UtcNow;
            Assert.That(await _schematic.IsReadyAsync(), Is.False);
            Assert.That(DateTime.UtcNow - started, Is.LessThan(TimeSpan.FromSeconds(2)));
        }

        [Test]
        public async Task ApiMode_CallerCancels_ReturnsFalseQuickly()
        {
            var api = new RecordingHandler(async token =>
            {
                await Task.Delay(TimeSpan.FromSeconds(10), token);
                return Respond(HttpStatusCode.OK, WhoAmIBody);
            });
            var logger = new FakeLogger();
            _schematic = CreateApiSchematic(api, logger: logger);

            using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(100));
            var started = DateTime.UtcNow;
            Assert.That(await _schematic.IsReadyAsync(cts.Token), Is.False);
            Assert.That(DateTime.UtcNow - started, Is.LessThan(TimeSpan.FromSeconds(2)));
            Assert.That(logger.Collector.GetSnapshot().Any(r => r.Level == Microsoft.Extensions.Logging.LogLevel.Warning), Is.False);
        }

        private static Schematic CreateApiSchematic(RecordingHandler api, TimeSpan? clientTimeout = null, FakeLogger? logger = null)
        {
            var options = new ClientOptions
            {
                EventBuffer = new NoopEventBuffer(),
                Timeout = clientTimeout ?? TimeSpan.FromSeconds(30),
                LoggerFactory = (logger ?? new FakeLogger()).ToLoggerFactory(),
            };
            return new Schematic("test-api-key", options.WithHttpClient(new HttpClient(api)));
        }

        private static Schematic CreateReplicatorSchematic(RecordingHandler api, bool replicatorReady) =>
            ReplicatorTestHarness.CreateReplicatorSchematic(
                api, new LocalCache(), new FakeReplicatorHealthService { IsHealthy = replicatorReady }, out _);

        private static Schematic CreateWebsocketSchematic(RecordingHandler api, bool connected, bool snapshotReceived)
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
            if (snapshotReceived)
            {
                var inner = typeof(DatastreamClientAdapter)
                    .GetField("_client", BindingFlags.NonPublic | BindingFlags.Instance)!
                    .GetValue(adapter)!;
                var snapshot = (TaskCompletionSource<bool>)typeof(DatastreamClient)
                    .GetField("_flagsSnapshot", BindingFlags.NonPublic | BindingFlags.Instance)!
                    .GetValue(inner)!;
                snapshot.TrySetResult(true);
            }

            ReplicatorTestHarness.ReplaceAdapter(schematic, adapter);
            return schematic;
        }

        private static HttpResponseMessage Respond(HttpStatusCode status, string body = "{}") =>
            new HttpResponseMessage(status) { Content = new StringContent(body, Encoding.UTF8, "application/json") };

        private sealed class StallingStream : Stream
        {
            public override bool CanRead => true;
            public override bool CanSeek => false;
            public override bool CanWrite => false;
            public override long Length => throw new NotSupportedException();
            public override long Position { get => 0; set => throw new NotSupportedException(); }
            public override void Flush() { }
            public override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException();
            public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
            public override void SetLength(long value) => throw new NotSupportedException();
            public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();

            public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
            {
                await Task.Delay(Timeout.Infinite, cancellationToken);
                return 0;
            }

            public override async ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
            {
                await Task.Delay(Timeout.Infinite, cancellationToken);
                return 0;
            }
        }

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
    }
}
