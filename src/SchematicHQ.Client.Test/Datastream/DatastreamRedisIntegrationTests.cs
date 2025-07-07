using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Datastream;
using SchematicHQ.Client.Test.Datastream.Mocks;

namespace SchematicHQ.Client.Test.Datastream
{
    [TestFixture]
    public class DatastreamRedisIntegrationTests
    {
        [Test]
        public void DatastreamOptions_WithRedisConfiguration_CreatesCorrectly()
        {
            // Arrange
            var options = new DatastreamOptions
            {
                CacheProviderType = DatastreamCacheProviderType.Redis,
                RedisConfig = new RedisCacheConfig
                {
                    Endpoints = new List<string> 
                    { 
                        "localhost:6379",
                        "localhost:6380"
                    },
                    Password = "testpass",
                    Ssl = true,
                    KeyPrefix = "schematic:test:",
                    Database = 1,
                    CacheTTL = TimeSpan.FromMinutes(10)
                }
            };
            
            // Act & Assert
            Assert.That(options.CacheProviderType, Is.EqualTo(DatastreamCacheProviderType.Redis));
            Assert.That(options.RedisConfig, Is.Not.Null);
            Assert.That(options.RedisConfig.Endpoints.Count, Is.EqualTo(2));
            Assert.That(options.RedisConfig.Password, Is.EqualTo("testpass"));
            Assert.That(options.RedisConfig.Ssl, Is.True);
            Assert.That(options.RedisConfig.KeyPrefix, Is.EqualTo("schematic:test:"));
            Assert.That(options.RedisConfig.Database, Is.EqualTo(1));
        }
        
        [Test]
        public void Schematic_WithDatastreamRedisOptions_InitializesWithoutError()
        {
            // Arrange
            var clientOptions = new ClientOptions
            {
                DatastreamOptions = new DatastreamOptions()
                    .WithRedisCache(new RedisCacheConfig
                    {
                        Endpoints = new List<string> { "localhost:6379" },
                        Password = "testpass",
                        AbortOnConnectFail = false, // prevents hanging on missing Redis
                        KeyPrefix = "schematic:test:",
                        CacheTTL = TimeSpan.FromMinutes(5)
                    })
            };
            
            // Act & Assert - Should not throw TypeLoadException
            Assert.DoesNotThrow(() =>
            {
                var schematic = new Schematic("test_api_key", clientOptions);
            });
        }
        
    }
}