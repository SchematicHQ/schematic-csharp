using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Testing;
using NUnit.Framework;
using SchematicHQ.Client.Cache;
using SchematicHQ.Client.Datastream;

namespace SchematicHQ.Client.Test.Datastream
{
    /// <summary>
    /// Replicator mode defaults its health URL to http://localhost:8090/ready when none is
    /// configured, matching the Go, Python, Java and Ruby SDKs. An explicit URL always wins.
    /// </summary>
    [TestFixture]
    public class ReplicatorHealthUrlDefaultTests
    {
        private const string ExpectedDefault = "http://localhost:8090/ready";
        private const string CustomUrl = "http://replicator.internal:9000/ready";

        [Test]
        public void DefaultReplicatorHealthUrl_MatchesOtherSdks()
        {
            Assert.That(ClientOptions.DefaultReplicatorHealthUrl, Is.EqualTo(ExpectedDefault));
        }

        // Options resolution

        [Test]
        public void Resolve_PropertyReplicatorModeWithoutUrl_UsesDefault()
        {
            var options = new ClientOptions { ReplicatorMode = true };

            Assert.That(options.ResolveReplicatorHealthUrl(), Is.EqualTo(ExpectedDefault));
        }

        [TestCase("")]
        [TestCase("   ")]
        public void Resolve_PropertyReplicatorModeWithBlankUrl_UsesDefault(string blank)
        {
            var options = new ClientOptions { ReplicatorMode = true, ReplicatorHealthUrl = blank };

            Assert.That(options.ResolveReplicatorHealthUrl(), Is.EqualTo(ExpectedDefault));
        }

        [Test]
        public void Resolve_PropertyReplicatorModeWithExplicitUrl_UsesExplicitUrl()
        {
            var options = new ClientOptions { ReplicatorMode = true, ReplicatorHealthUrl = CustomUrl };

            Assert.That(options.ResolveReplicatorHealthUrl(), Is.EqualTo(CustomUrl));
        }

        [Test]
        public void Resolve_NotReplicatorMode_ReturnsNull()
        {
            Assert.That(new ClientOptions().ResolveReplicatorHealthUrl(), Is.Null);
            Assert.That(
                new ClientOptions { ReplicatorHealthUrl = CustomUrl }.ResolveReplicatorHealthUrl(),
                Is.Null);
        }

        // WithReplicatorMode helper

        [Test]
        public void WithReplicatorMode_NoArguments_UsesDefault()
        {
            var options = new ClientOptions().WithReplicatorMode();

            Assert.That(options.ReplicatorMode, Is.True);
            Assert.That(options.UseDatastream, Is.False);
            Assert.That(options.ReplicatorHealthUrl, Is.EqualTo(ExpectedDefault));
            Assert.That(options.ResolveReplicatorHealthUrl(), Is.EqualTo(ExpectedDefault));
        }

        [Test]
        public void WithReplicatorMode_ExplicitUrl_UsesExplicitUrl()
        {
            var options = new ClientOptions().WithReplicatorMode(CustomUrl);

            Assert.That(options.ReplicatorMode, Is.True);
            Assert.That(options.ReplicatorHealthUrl, Is.EqualTo(CustomUrl));
            Assert.That(options.ResolveReplicatorHealthUrl(), Is.EqualTo(CustomUrl));
        }

        [TestCase("")]
        [TestCase("   ")]
        public void WithReplicatorMode_ExplicitBlankUrl_Throws(string blank)
        {
            Assert.Throws<ArgumentException>(() => new ClientOptions().WithReplicatorMode(blank));
        }

        // DatastreamClientAdapter

        [Test]
        public void Adapter_ReplicatorModeWithoutUrl_CreatesHealthServiceWithDefault()
        {
            var adapter = CreateAdapter(replicatorMode: true, healthUrl: null);
            try
            {
                Assert.That(adapter.HasReplicatorHealthService, Is.True);
                Assert.That(adapter.ReplicatorHealthUrl, Is.EqualTo(ExpectedDefault));
            }
            finally
            {
                adapter.Close();
            }
        }

        [Test]
        public void Adapter_ReplicatorModeWithExplicitUrl_UsesExplicitUrl()
        {
            var adapter = CreateAdapter(replicatorMode: true, healthUrl: CustomUrl);
            try
            {
                Assert.That(adapter.HasReplicatorHealthService, Is.True);
                Assert.That(adapter.ReplicatorHealthUrl, Is.EqualTo(CustomUrl));
            }
            finally
            {
                adapter.Close();
            }
        }

        [Test]
        public void Adapter_NotReplicatorMode_CreatesNoHealthService()
        {
            var adapter = CreateAdapter(replicatorMode: false, healthUrl: CustomUrl);
            try
            {
                Assert.That(adapter.HasReplicatorHealthService, Is.False);
                Assert.That(adapter.ReplicatorHealthUrl, Is.Null);
                Assert.That(adapter.IsReplicatorReady(), Is.False);
            }
            finally
            {
                adapter.Close();
            }
        }

        // End to end through the Schematic constructor

        [Test]
        public async Task Schematic_ReplicatorModePropertyWithoutUrl_UsesDefault()
        {
            var options = new ClientOptions { ReplicatorMode = true };
            options.WithRedisCache(UnreachableRedis());

            await AssertSchematicHealthUrl(options, ExpectedDefault);
        }

        [Test]
        public async Task Schematic_WithReplicatorModeNoArguments_UsesDefault()
        {
            var options = new ClientOptions()
                .WithRedisCache(UnreachableRedis())
                .WithReplicatorMode();

            await AssertSchematicHealthUrl(options, ExpectedDefault);
        }

        [Test]
        public async Task Schematic_ReplicatorModeWithExplicitUrl_UsesExplicitUrl()
        {
            var options = new ClientOptions()
                .WithRedisCache(UnreachableRedis())
                .WithReplicatorMode(CustomUrl);

            await AssertSchematicHealthUrl(options, CustomUrl);
        }

        private static async Task AssertSchematicHealthUrl(ClientOptions options, string expected)
        {
            options.LoggerFactory = Microsoft.Extensions.Logging.Abstractions.NullLoggerFactory.Instance;
            var schematic = new Schematic("test-api-key", options);
            try
            {
                Assert.That(schematic.IsReplicatorMode(), Is.True);
                Assert.That(schematic.DatastreamClient, Is.Not.Null);
                Assert.That(schematic.DatastreamClient!.HasReplicatorHealthService, Is.True);
                Assert.That(schematic.DatastreamClient.ReplicatorHealthUrl, Is.EqualTo(expected));
            }
            finally
            {
                await schematic.Shutdown();
            }
        }

        // Replicator mode requires a Redis cache config. abortConnect=false lets the
        // multiplexer come up without a server, so no Redis is needed for these tests.
        private static RedisCacheConfig UnreachableRedis() => new RedisCacheConfig
        {
            Configuration = "127.0.0.1:1,abortConnect=false,connectTimeout=100",
        };

        private static DatastreamClientAdapter CreateAdapter(bool replicatorMode, string? healthUrl)
        {
            return new DatastreamClientAdapter(
                "wss://test.example.com",
                new FakeLogger(),
                "test-api-key",
                new LocalCache(),
                new DatastreamOptions { CacheTTL = TimeSpan.FromMinutes(10) },
                replicatorMode,
                healthUrl);
        }
    }
}
