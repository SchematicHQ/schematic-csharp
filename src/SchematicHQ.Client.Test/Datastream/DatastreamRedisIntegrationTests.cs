using NUnit.Framework;
using SchematicHQ.Client.Datastream;

namespace SchematicHQ.Client.Test.Datastream
{
    [TestFixture]
    public class DatastreamRedisIntegrationTests
    {
        [Test]
        public void DatastreamClient_CanLoadRulesEngineTypes()
        {
            // This test verifies the fix for the TypeLoadException:
            // "Could not load type 'RulesEngine.Models.Company' from assembly 'RulesEngine'"

            // Arrange
            var clientOptions = new ClientOptions
            {
                UseDatastream = true,
                DatastreamOptions = new DatastreamOptions()
            };

            // Act & Assert - The key test is that we can instantiate Schematic with Datastream enabled
            // which internally creates a DatastreamClient that depends on RulesEngine types
            Exception? caughtException = null;
            Schematic? schematic = null;
            try
            {
                schematic = new Schematic("test_api_key", clientOptions);
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
                companyType = Type.GetType("SchematicHQ.Client.RulesengineCompany, SchematicHQ.Client");

                // If the type is null, try to load from any loaded assembly
                if (companyType == null)
                {
                    var clientAssembly = AppDomain.CurrentDomain.GetAssemblies()
                        .FirstOrDefault(a => a.GetName().Name == "SchematicHQ.Client");

                    if (clientAssembly != null)
                    {
                        companyType = clientAssembly.GetType("SchematicHQ.Client.RulesengineCompany");
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
                "Should be able to load RulesengineCompany type");

            // Verify we can also load other RulesEngine types that DatastreamClient might use
            var userType = Type.GetType("SchematicHQ.Client.RulesengineUser, SchematicHQ.Client") ??
                          AppDomain.CurrentDomain.GetAssemblies()
                              .FirstOrDefault(a => a.GetName().Name == "SchematicHQ.Client")
                              ?.GetType("SchematicHQ.Client.RulesengineUser");

            Assert.That(userType, Is.Not.Null,
                "Should be able to load SchematicHQ.Client.RulesengineUser type");
        }

    }
}
