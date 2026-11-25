using System.Net;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using SchematicHQ.Client.Cache;
using SchematicHQ.Client.Datastream;
using SchematicHQ.Client.Leases;
using SchematicHQ.Client.RulesEngine;
using SchematicHQ.Client.Test.Credits.Leases;

namespace SchematicHQ.Client.Test.Datastream
{
    [TestFixture]
    public class RulesEngineUnavailableTests
    {
        private const string FlagKey = "engine-flag";
        private static readonly Dictionary<string, string> CompanyKeys = new() { { "id", "company-123" } };

        private Schematic? _schematic;

        [TearDown]
        public async Task TearDown()
        {
            if (_schematic != null)
            {
                await _schematic.Shutdown();
                _schematic = null;
            }
        }

        [Test]
        public async Task EngineAvailable_EvaluatesLocally()
        {
            var api = new FakeApiHandler();
            _schematic = await CreateSchematic(api, engineAvailable: true);

            var value = await _schematic.CheckFlag(FlagKey, company: CompanyKeys);

            Assert.That(value, Is.False);
            Assert.That(api.Paths, Is.Empty);
        }

        [Test]
        public async Task EngineUnavailable_SingleCheck_UsesApi()
        {
            var api = new FakeApiHandler();
            _schematic = await CreateSchematic(api, engineAvailable: false);

            var value = await _schematic.CheckFlag(FlagKey, company: CompanyKeys);

            Assert.That(value, Is.True);
            Assert.That(api.Paths, Is.EqualTo(new[] { $"/flags/{FlagKey}/check" }));
        }

        [Test]
        public async Task EngineUnavailable_CheckWithEntitlement_UsesApi()
        {
            var api = new FakeApiHandler();
            _schematic = await CreateSchematic(api, engineAvailable: false);

            var resp = await _schematic.CheckFlagWithEntitlement(FlagKey, company: CompanyKeys);

            Assert.That(resp.Value, Is.True);
            Assert.That(resp.Reason, Is.EqualTo("api"));
            Assert.That(api.Paths, Is.EqualTo(new[] { $"/flags/{FlagKey}/check" }));
        }

        [Test]
        public async Task EngineUnavailable_BulkCheck_UsesApi()
        {
            var api = new FakeApiHandler();
            _schematic = await CreateSchematic(api, engineAvailable: false);

            var results = await _schematic.CheckFlags(company: CompanyKeys, keys: new[] { FlagKey });

            Assert.That(results, Has.Count.EqualTo(1));
            Assert.That(results[0].Value, Is.True);
            Assert.That(results[0].Reason, Is.EqualTo("api"));
            Assert.That(api.Paths, Is.EqualTo(new[] { "/flags/check" }));
        }

        [Test]
        public async Task EngineUnavailable_CheckWithoutUsage_UsesApi()
        {
            var api = new FakeApiHandler();
            _schematic = await CreateSchematic(api, engineAvailable: false);

            var result = await _schematic.Check(FlagKey, CompanyKeys);

            Assert.That(result.Value, Is.True);
            Assert.That(api.Paths, Is.EqualTo(new[] { $"/flags/{FlagKey}/check" }));
        }

        [Test]
        public async Task EngineUnavailable_AutoLeaseMode_UsesServerReservation()
        {
            var api = new FakeApiHandler();
            _schematic = await CreateSchematic(
                api, engineAvailable: false, creditLeases: new CreditLeaseConfig { Mode = CreditLeaseMode.Auto });

            var result = await _schematic.Check(
                FlagKey, CompanyKeys, options: new CheckOptions { Usage = 5, EventSubtype = "tokens" });

            Assert.That(result.Allowed, Is.True);
            Assert.That(api.Paths, Is.EqualTo(new[] { $"/flags/{FlagKey}/check-and-reserve" }));
        }

        [Test]
        public async Task EngineUnavailable_ClientLeaseMode_UsesPlainApiCheck()
        {
            var api = new FakeApiHandler();
            _schematic = await CreateSchematic(
                api, engineAvailable: false, creditLeases: new CreditLeaseConfig { Mode = CreditLeaseMode.Client });

            var result = await _schematic.Check(
                FlagKey, CompanyKeys, options: new CheckOptions { Usage = 5, EventSubtype = "tokens" });

            Assert.That(result.Allowed, Is.True);
            Assert.That(result.Reservation, Is.Null);
            Assert.That(api.Paths, Is.EqualTo(new[] { $"/flags/{FlagKey}/check" }));
        }

        [Test]
        public async Task EngineUnavailable_WarnsOnce()
        {
            var api = new FakeApiHandler();
            var logs = new RecordingLoggerFactory();
            _schematic = await CreateSchematic(api, engineAvailable: false, loggerFactory: logs);

            await _schematic.CheckFlag(FlagKey, company: CompanyKeys);
            await _schematic.CheckFlagWithEntitlement(FlagKey, company: CompanyKeys);
            await _schematic.CheckFlags(company: CompanyKeys, keys: new[] { FlagKey });

            var warnings = logs.Lines
                .Where(l => l.Level == LogLevel.Warning && l.Message.Contains("rules engine unavailable"))
                .ToList();
            Assert.That(warnings, Has.Count.EqualTo(1));
        }

        [Test]
        public async Task EngineUnavailable_AdapterThrowsRatherThanReturningDefault()
        {
            _schematic = await CreateSchematic(new FakeApiHandler(), engineAvailable: false);

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await _schematic.DatastreamClient!.CheckFlag(new CheckFlagRequestBody { Company = CompanyKeys }, FlagKey));
        }

        private static async Task<Schematic> CreateSchematic(
            FakeApiHandler api,
            bool engineAvailable,
            CreditLeaseConfig? creditLeases = null,
            ILoggerFactory? loggerFactory = null)
        {
            var options = new ClientOptions
            {
                BaseUrl = "http://127.0.0.1:1",
                UseDatastream = true,
                CacheProvider = new LocalCache(),
                EventBuffer = new RecordingEventBuffer(),
                MaxRetries = 0,
                CreditLeases = creditLeases,
            };
            if (loggerFactory != null)
            {
                options.LoggerFactory = loggerFactory;
            }
            var schematic = new Schematic("test-api-key", options.WithHttpClient(new HttpClient(api)));

            var inner = (DatastreamClient)typeof(DatastreamClientAdapter)
                .GetField("_client", BindingFlags.NonPublic | BindingFlags.Instance)!
                .GetValue(schematic.DatastreamClient)!;
            await SeedCache(inner);

            if (!engineAvailable)
            {
                var engineField = typeof(DatastreamClient)
                    .GetField("_rulesEngine", BindingFlags.NonPublic | BindingFlags.Instance)!;
                ((WasmRulesEngine?)engineField.GetValue(inner))?.Dispose();
                engineField.SetValue(inner, null);
            }

            return schematic;
        }

        private static async Task SeedCache(DatastreamClient inner)
        {
            var flagsCache = (ICacheProvider)typeof(DatastreamClient)
                .GetField("_flagsCache", BindingFlags.NonPublic | BindingFlags.Instance)!
                .GetValue(inner)!;
            var flagCacheKey = (string)typeof(DatastreamClient)
                .GetMethod("FlagCacheKey", BindingFlags.NonPublic | BindingFlags.Instance)!
                .Invoke(inner, new object[] { FlagKey })!;
            await flagsCache.Set(flagCacheKey, new RulesengineFlag
            {
                Id = "flag_123",
                Key = FlagKey,
                AccountId = "acc_123",
                EnvironmentId = "env_123",
                DefaultValue = false,
                Rules = new List<RulesengineRule>()
            });

            var company = new RulesengineCompany
            {
                Id = "comp_123",
                AccountId = "acc_123",
                EnvironmentId = "env_123",
                Keys = new Dictionary<string, string>(CompanyKeys)
            };
            await (Task)typeof(DatastreamClient)
                .GetMethod("CacheCompanyForKeys", BindingFlags.NonPublic | BindingFlags.Instance)!
                .Invoke(inner, new object[] { company })!;
        }

        private sealed class FakeApiHandler : HttpMessageHandler
        {
            private readonly List<string> _paths = new();

            public IReadOnlyList<string> Paths
            {
                get { lock (_paths) return _paths.ToList(); }
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var path = request.RequestUri!.AbsolutePath;
                lock (_paths) _paths.Add(path);

                string body;
                if (path == "/flags/check")
                {
                    body = $$$"""{"data":{"flags":[{"flag":"{{{FlagKey}}}","reason":"api","value":true}]},"params":{}}""";
                }
                else if (path == $"/flags/{FlagKey}/check" || path == $"/flags/{FlagKey}/check-and-reserve")
                {
                    body = $$$"""{"data":{"flag":"{{{FlagKey}}}","reason":"api","value":true},"params":{}}""";
                }
                else
                {
                    return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
                }

                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(body, Encoding.UTF8, "application/json")
                });
            }
        }
    }
}
