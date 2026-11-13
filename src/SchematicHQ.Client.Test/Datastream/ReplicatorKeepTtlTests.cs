using System.Collections;
using System.Text.Json;
using Microsoft.Extensions.Logging.Testing;
using Moq;
using NUnit.Framework;
using SchematicHQ.Client.Cache;
using SchematicHQ.Client.Datastream;
using SchematicHQ.Client.Test.Datastream.Mocks;
using StackExchange.Redis;

namespace SchematicHQ.Client.Test.Datastream
{
    /// <summary>
    /// The replicator writes company keys with its own TTL (none by default). The SDK's
    /// optimistic metric bump on track rewrites those keys, and in replicator mode must keep
    /// the replicator's TTL rather than apply the SDK's cache TTL.
    /// </summary>
    [TestFixture]
    public class ReplicatorKeepTtlTests
    {
        private const string EventName = "api-call";
        private const string CompanyId = "comp_123";
        private static readonly Dictionary<string, string> CompanyKeys = new() { { "id", "company-123" } };

        [Test]
        public async Task MetricBump_ReplicatorMode_RedisWritesKeepTtl()
        {
            var db = MockDatabaseHoldingCompany();
            var client = CreateClient(new RedisCache(RedisConfig(db)), replicatorMode: true);

            Assert.That(await client.UpdateCompanyMetricsAsync(TrackEvent()), Is.True);

            var sets = StringSets(db);
            Assert.That(sets.Select(s => s.Key), Is.EquivalentTo(new[]
            {
                $"schematic:company:{CacheVersion}:{CompanyId}",
                $"schematic:company:{CacheVersion}:id:company-123",
            }));
            foreach (var set in sets)
            {
                Assert.That(set.KeepTtl, Is.True, set.Key);
                Assert.That(set.Expiry, Is.Null, set.Key);
            }
        }

        [Test]
        public async Task MetricBump_NotReplicatorMode_RedisWritesApplySdkTtl()
        {
            var db = MockDatabaseHoldingCompany();
            var client = CreateClient(new RedisCache(RedisConfig(db)), replicatorMode: false);

            Assert.That(await client.UpdateCompanyMetricsAsync(TrackEvent()), Is.True);

            var sets = StringSets(db);
            Assert.That(sets, Has.Count.EqualTo(2));
            foreach (var set in sets)
            {
                Assert.That(set.KeepTtl, Is.False, set.Key);
                Assert.That(set.Expiry, Is.EqualTo(TimeSpan.FromHours(24)), set.Key);
            }
        }

        [Test]
        public async Task MetricBump_ReplicatorMode_LocalCacheKeepsExpiry()
        {
            var cache = new LocalCache();
            var client = CreateClient(cache, replicatorMode: true);
            var idKey = $"company:{CacheVersion}:{CompanyId}";
            var lookupKey = $"company:{CacheVersion}:id:company-123";

            // As the replicator would write them: no expiry
            await cache.Set(idKey, Company(10), LocalCache.UNLIMITED_TTL);
            await cache.Set(lookupKey, CompanyId, LocalCache.UNLIMITED_TTL);

            Assert.That(await client.UpdateCompanyMetricsAsync(TrackEvent()), Is.True);

            var stored = await cache.Get<RulesengineCompany>(idKey);
            Assert.That(stored!.Metrics.Single().Value, Is.EqualTo(13));
            Assert.That(LocalExpiration(cache, idKey), Is.EqualTo(DateTime.MaxValue));
            Assert.That(LocalExpiration(cache, lookupKey), Is.EqualTo(DateTime.MaxValue));
        }

        // ---- helpers ----

        private const string CacheVersion = "v-test";

        private static DatastreamClient CreateClient(ICacheProvider cache, bool replicatorMode) =>
            new DatastreamClient(
                "wss://test.example.com",
                new FakeLogger(),
                "test-api-key",
                _ => { },
                cache,
                cacheTtl: null,
                webSocket: new MockWebSocket(),
                cacheVersionProvider: () => CacheVersion,
                replicatorMode: replicatorMode);

        private static EventBodyTrack TrackEvent() => new()
        {
            Event = EventName,
            Company = new Dictionary<string, string>(CompanyKeys),
            Quantity = 3,
        };

        private static RulesengineCompany Company(long metricValue) => new()
        {
            Id = CompanyId,
            AccountId = "acc_123",
            EnvironmentId = "env_123",
            Keys = new Dictionary<string, string>(CompanyKeys),
            Metrics = new List<RulesengineCompanyMetric>
            {
                new()
                {
                    AccountId = "acc_123",
                    EnvironmentId = "env_123",
                    CompanyId = CompanyId,
                    EventSubtype = EventName,
                    Period = RulesengineMetricPeriod.AllTime,
                    MonthReset = RulesengineMetricPeriodMonthReset.FirstOfMonth,
                    Value = metricValue,
                    CreatedAt = DateTime.UtcNow,
                },
            },
        };

        private static Mock<IDatabase> MockDatabaseHoldingCompany()
        {
            var companyJson = JsonSerializer.Serialize(Company(10), SchematicCacheSerializerDefaults.Options);
            var idJson = JsonSerializer.Serialize(CompanyId, SchematicCacheSerializerDefaults.Options);

            var db = new Mock<IDatabase>();
            db.Setup(d => d.StringGetAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
                .ReturnsAsync((RedisKey key, CommandFlags _) => key.ToString() switch
                {
                    var k when k.EndsWith($":id:company-123") => (RedisValue)idJson,
                    var k when k.EndsWith($":{CompanyId}") => (RedisValue)companyJson,
                    _ => RedisValue.Null,
                });
            return db;
        }

        private static RedisCacheConfig RedisConfig(Mock<IDatabase> db)
        {
            var redis = new Mock<IConnectionMultiplexer>();
            redis.Setup(r => r.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(db.Object);
            return new RedisCacheConfig { ConnectionMultiplexerFactory = () => redis.Object };
        }

        private sealed record StringSet(string Key, TimeSpan? Expiry, bool KeepTtl);

        // Reads every StringSetAsync call by parameter name, whichever overload it bound to.
        private static List<StringSet> StringSets(Mock<IDatabase> db) =>
            db.Invocations
                .Where(i => i.Method.Name == nameof(IDatabase.StringSetAsync))
                .Select(i =>
                {
                    var ps = i.Method.GetParameters();
                    object? Arg(string name)
                    {
                        var idx = Array.FindIndex(ps, p => p.Name == name);
                        return idx < 0 ? null : i.Arguments[idx];
                    }
                    return new StringSet(
                        ((RedisKey)Arg("key")!).ToString(),
                        Arg("expiry") as TimeSpan?,
                        Arg("keepTtl") as bool? ?? false);
                })
                .ToList();

        private static DateTime LocalExpiration(LocalCache cache, string key)
        {
            var entries = (IDictionary)typeof(LocalCache)
                .GetField("_cache", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
                .GetValue(cache)!;
            var item = entries[key]!;
            return (DateTime)item.GetType().GetProperty("Expiration")!.GetValue(item)!;
        }
    }
}
