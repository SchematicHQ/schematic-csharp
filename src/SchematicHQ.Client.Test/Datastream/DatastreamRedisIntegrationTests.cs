using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Datastream;
using SchematicHQ.Client.Test.Datastream.Mocks;

// Type alias to avoid conflict with Schematic namespace
using SchematicClient = SchematicHQ.Client.Schematic;

namespace SchematicHQ.Tests.Datastream
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
            var mockLogger = new MockSchematicLogger();
            var clientOptions = new ClientOptions
            {
                Logger = mockLogger,
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
            SchematicClient? schematic = null;
            try
            {
                Assert.DoesNotThrow(() =>
                {
                    schematic = new SchematicClient("test_api_key", clientOptions);
                });
                Assert.That(schematic, Is.Not.Null);
            }
            finally
            {
                schematic?.Shutdown().Wait(TimeSpan.FromSeconds(5));
            }
        }

        [Test]
        public void DatastreamClient_CanLoadRulesEngineTypes()
        {
            // This test verifies the fix for the TypeLoadException:
            // "Could not load type 'RulesEngine.Models.Company' from assembly 'RulesEngine'"

            // Arrange
            var mockLogger = new MockSchematicLogger();
            var clientOptions = new ClientOptions
            {
                Logger = mockLogger,
                UseDatastream = true,
                DatastreamOptions = new DatastreamOptions()
                    .WithRedisCache(new RedisCacheConfig
                    {
                        Endpoints = new List<string> { "localhost:6379" },
                        Password = "testpass",
                        AbortOnConnectFail = false,
                        ConnectRetry = 0, // Don't retry for tests
                        ConnectTimeout = 1000, // Short timeout for tests
                        KeyPrefix = "schematic:test:",
                        CacheTTL = TimeSpan.FromMinutes(5)
                    })
            };

            // Act & Assert - The key test is that we can instantiate Schematic with Datastream enabled
            // which internally creates a DatastreamClient that depends on RulesEngine types
            Exception? caughtException = null;
            SchematicClient? schematic = null;
            try
            {
                schematic = new SchematicClient("test_api_key", clientOptions);
                // The DatastreamClient constructor is where the TypeLoadException would occur
                // if the RulesEngine assembly wasn't properly referenced
                Assert.That(schematic, Is.Not.Null);
            }
            catch (Exception ex)
            {
                caughtException = ex;
            }
            finally
            {
                // Clean up if Schematic was created
                if (schematic != null)
                {
                    try
                    {
                        schematic.Shutdown().Wait(TimeSpan.FromSeconds(5));
                    }
                    catch
                    {
                        // Ignore cleanup errors
                    }
                }
            }

            // Assert - We should not get a TypeLoadException
            if (caughtException != null)
            {
                // If we get an exception, it should be about Redis connection, not type loading
                Assert.That(caughtException, Is.Not.TypeOf<TypeLoadException>(),
                    $"Got TypeLoadException: {caughtException.Message}");

                // We expect either InvalidOperationException (Redis connection failed)
                // or similar, but NOT TypeLoadException
                Assert.That(caughtException.GetType().Name,
                    Does.Not.Contain("TypeLoadException"),
                    "Should not have any TypeLoadException in the exception chain");
            }
        }

        [Test]
        public void RulesEngineAssembly_CanBeLoaded()
        {
            // Direct test to verify RulesEngine assembly and types are accessible

            // Act & Assert - Try to load the RulesEngine.Models.Company type directly
            Type? companyType = null;
            Exception? loadException = null;

            try
            {
                // This is the exact type that was failing to load in the customer's issue
                companyType = Type.GetType("Schematic.RulesEngine.Models.Company, SchematicHQ.Client");

                // If the type is null, try to load from any loaded assembly
                if (companyType == null)
                {
                    var clientAssembly = AppDomain.CurrentDomain.GetAssemblies()
                        .FirstOrDefault(a => a.GetName().Name == "SchematicHQ.Client");

                    if (clientAssembly != null)
                    {
                        companyType = clientAssembly.GetType("Schematic.RulesEngine.Models.Company");
                    }
                }
            }
            catch (Exception ex)
            {
                loadException = ex;
            }

            // Assert
            Assert.That(loadException, Is.Null,
                $"Should not throw exception when loading RulesEngine types: {loadException?.Message}");
            Assert.That(companyType, Is.Not.Null,
                "Should be able to load RulesEngine.Models.Company type");

            // Verify we can also load other RulesEngine types that DatastreamClient might use
            var userType = Type.GetType("Schematic.RulesEngine.Models.User, SchematicHQ.Client") ??
                          AppDomain.CurrentDomain.GetAssemblies()
                              .FirstOrDefault(a => a.GetName().Name == "SchematicHQ.Client")
                              ?.GetType("Schematic.RulesEngine.Models.User");

            Assert.That(userType, Is.Not.Null,
                "Should be able to load Schematic.RulesEngine.Models.User type");
        }

    }
}
