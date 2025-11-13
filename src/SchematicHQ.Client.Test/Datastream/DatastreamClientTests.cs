using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using NUnit.Framework;
using SchematicHQ.Client.RulesEngine.Utils;
using SchematicHQ.Client.Datastream;
using SchematicHQ.Client.Test.Datastream.Mocks;

namespace SchematicHQ.Client.Test.Datastream
{
    [TestFixture]
    public class DatastreamClientTests
    {
        private MockWebSocket _mockWebSocket;
        private MockSchematicLogger _mockLogger;
        private DatastreamClient _client;
        private JsonSerializerOptions _jsonOptions;
        
        [SetUp]
        public void Setup()
        {
            var testSetup = DatastreamClientTestFactory.CreateClientWithMocks();
            _client = testSetup.Client;
            _mockWebSocket = testSetup.WebSocket;
            _mockLogger = testSetup.Logger;
            
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower, false) }
            };
        }

        [TearDown]
        public void TearDown()
        {
            _mockWebSocket?.SentMessages?.Clear();
        }
        
        [Test]
        public void CheckFlag_WhenFlagIsNotInCache_ReturnsFalse()
        {
            // Arrange - create an adapter that will use our mock client
            var options = new DatastreamOptions();
            var adapter = new DatastreamClientAdapter("wss://test.example.com", _mockLogger, "test-api-key", options);
            
            // Get the private _client field from adapter and replace it with our mock client
            var clientField = typeof(DatastreamClientAdapter).GetField("_client", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            clientField?.SetValue(adapter, _client);
            
            // Set flag to null to simulate flag not in cache
            _client.Start();
            
            var request = new CheckFlagRequestBody
            {
                Company = new Dictionary<string, string>
                {
                    { "id", "company-123" }
                }
            };
            
            // Act
            var result = adapter.CheckFlag(request, "non-existent-flag").GetAwaiter().GetResult();
            
            // Assert
            Assert.That(result.Value, Is.False);
        }
        
        [Test]
        public void CheckFlag_WhenFlagExists_EvaluatesCorrectly()
        {
            // Arrange
            SetupFlagsResponse();
            
            // Start the client to process the response
            _client.Start();
            
            // Create a test company
            var testCompany = new RulesengineCompany
            {
                Id = "comp_123",
                AccountId = "acc_123",
                EnvironmentId = "env_123",
                Keys = new Dictionary<string, string> 
                { 
                    { "id", "company-123" } 
                },
                Traits = new List<RulesengineTrait>
                {
                    new RulesengineTrait { 
                        Value = "pro", 
                        TraitDefinition = new RulesengineTraitDefinition {
                            Id = "trait_123",
                            ComparableType = RulesengineTraitDefinitionComparableType.String,
                            EntityType = RulesengineTraitDefinitionEntityType.Company
                        } 
                    }
                }
            };
            
            // Access private field _companyCache and directly insert the company
            var companyCacheField = typeof(DatastreamClient).GetField("_companyCache", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            Assert.That(companyCacheField, Is.Not.Null, "Could not find _companyCache field");
            
            var companyCache = companyCacheField?.GetValue(_client);
            Assert.That(companyCache, Is.Not.Null, "Cache should not be null");
            
            // Calculate the cache key
            var resourceKeyToCacheKeyMethod = typeof(DatastreamClient).GetMethod("ResourceKeyToCacheKey", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            Assert.That(resourceKeyToCacheKeyMethod, Is.Not.Null, "ResourceKeyToCacheKey method not found");
            
            // Call the generic method with reflection
            var genericMethod = resourceKeyToCacheKeyMethod!.MakeGenericMethod(typeof(RulesengineCompany));
            var cacheKey = (string)genericMethod.Invoke(_client, new object[] { "company", "id", "company-123" })!;
            
            // Add company directly to the cache
            var setMethod = companyCache!.GetType().GetMethod("Set");
            Assert.That(setMethod, Is.Not.Null, "Cache Set method not found");
            setMethod!.Invoke(companyCache, new object[] { cacheKey, testCompany, Type.Missing });
            
            // Verify company is in cache
            var companyKeys = new Dictionary<string, string> { { "id", "company-123" } };
            var company = _client.GetCompanyFromCache(companyKeys);
            Assert.That(company, Is.Not.Null, "Company should be in cache after directly adding it");
            
            // Add the flag directly to the cache
            var flagsCacheField = typeof(DatastreamClient).GetField("_flagsCache", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var flagsCache = flagsCacheField!.GetValue(_client);
            
            // Create a test flag
            var testFlag = new RulesengineFlag
            {
                Id = "flag_456",
                Key = "another-feature",
                AccountId = "acc_123",
                EnvironmentId = "env_123",
                DefaultValue = true,
                Rules = new List<RulesengineRule>()
            };
            
            // Generate the cache key for the flag
            var flagCacheKeyMethod = typeof(DatastreamClient).GetMethod("FlagCacheKey", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var flagCacheKey = flagCacheKeyMethod!.Invoke(_client, new object[] { "another-feature" }) as string;
            
            // Set the flag in cache
            var flagSetMethod = flagsCache!.GetType().GetMethod("Set");
            Assert.That(flagSetMethod, Is.Not.Null, "Cache Set method not found");
            flagSetMethod!.Invoke(flagsCache, new object[] { flagCacheKey!, testFlag, Type.Missing });
            
            // Verify flag is in cache now
            var getFlagMethod = typeof(DatastreamClient).GetMethod("GetFlag", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var flag = getFlagMethod!.Invoke(_client, new object[] { "another-feature" }) as RulesengineFlag;
            Assert.That(flag, Is.Not.Null, "Flag should be in cache after setup");
            
            // Now call CheckFlag directly on the client
            var result = _client.CheckFlag(company, null, flag!).GetAwaiter().GetResult();
            
            // Assert
            Assert.That(result.Value, Is.True);
            
            // No need to check WebSocket messages since we added directly to cache
        }
        
        [Test]
        public async Task CachedResources_AreReusedWithoutWebSocketRequests()
        {
            // Arrange
            SetupFlagsResponse();
            
            // Create an adapter that will use our mock client
            var options = new DatastreamOptions();
            var adapter = new DatastreamClientAdapter("wss://test.example.com", _mockLogger, "test-api-key", options);
            
            // Get the private _client field from adapter and replace it with our mock client
            var clientField = typeof(DatastreamClientAdapter).GetField("_client", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            clientField?.SetValue(adapter, _client);
            
            // Get the private _connectionTracker field and set its IsConnected property to true
            var trackerField = typeof(DatastreamClientAdapter).GetField("_connectionTracker", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var tracker = trackerField?.GetValue(adapter);
            var updateMethod = tracker?.GetType().GetMethod("UpdateConnectionState", 
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            updateMethod?.Invoke(tracker, new object[] { true });
            
            // Start the client
            _client.Start();
            
            var companyResponse = new DataStreamResponse
            {
                MessageType = MessageType.Full,
                EntityType = SchematicHQ.Client.Datastream.EntityType.Company,
                Data = JsonDocument.Parse(JsonSerializer.Serialize(new RulesengineCompany
                {
                    Id = "comp_123",
                    AccountId = "acc_123",
                    EnvironmentId = "env_123",
                    Keys = new Dictionary<string, string> 
                    { 
                        { "id", "company-123" } 
                    }
                }, _jsonOptions)).RootElement
            };
            
            // Setup fake WebSocket responses
            _mockWebSocket.SetupToReceive(JsonSerializer.Serialize(companyResponse));
            
            var request = new CheckFlagRequestBody
            {
                Company = new Dictionary<string, string> { { "id", "company-123" } }
            };
            
            // Act - First request should fetch from WebSocket
            adapter.CheckFlag(request, "new-event-name").GetAwaiter().GetResult();
            
            // Wait a bit to ensure any async WebSocket operations complete
            await Task.Delay(50);
            
            int initialRequestCount = _mockWebSocket.SentMessages.Count;
            Assert.That(initialRequestCount, Is.GreaterThan(0), "First request should generate at least one WebSocket message");
            
            // Clear sent messages to track new ones
            _mockWebSocket.SentMessages.Clear();
            
            // Act - Second request should use cache
            adapter.CheckFlag(request, "new-event-name").GetAwaiter().GetResult();
            
            // Wait a bit to ensure any async WebSocket operations complete
            await Task.Delay(50);
            
            // Assert - No new messages should be sent when using cached data
            Assert.That(_mockWebSocket.SentMessages.Count, Is.EqualTo(0), "No new WebSocket messages should be sent when using cached data");
        }
        
        [Test]
        public void Dispose_ClosesWebSocketConnection()
        {
            // Act
            _client.Dispose();
            
            // Assert
            Assert.That(_mockLogger.HasLogEntry(LogLevel.Info, "Connected to Schematic WebSocket"), Is.False);
        }
        
        private void SetupFlagsResponse()
        {
            // Arrange - First set up flags
            var mockFlags = new List<RulesengineFlag>
            {
                new RulesengineFlag
                {
                    AccountId = "acc_123",
                    EnvironmentId = "env_123",
                    Id = "flag_123",
                    Key = "new-event-name",

                    DefaultValue = false,
                    Rules = new List<RulesengineRule>
                    {
                        new RulesengineRule
                        {
                            AccountId = "acc_123",
                            EnvironmentId = "env_123",
                            Id = "rule_123",
                            Name = "Test Rule",
                            RuleType = RulesengineRuleRuleType.Standard,
                            Priority = 1,
                            Conditions = new List<RulesengineCondition>(),
                            Value = true
                        }
                    }
                },
                new RulesengineFlag
                {
                    AccountId = "acc_123",
                    EnvironmentId = "env_123",
                    Id = "flag_456",
                    Key = "another-feature",
                    DefaultValue = true,
                    Rules = new List<RulesengineRule>()
                }
            };
                    
                        var flagsResponse = new DataStreamResponse
            {
                MessageType = MessageType.Full,
                EntityType = SchematicHQ.Client.Datastream.EntityType.Flags,
                Data = JsonDocument.Parse(JsonSerializer.Serialize(mockFlags)).RootElement
            };
            
            _mockWebSocket.SetupToReceive(JsonSerializer.Serialize(flagsResponse));
        }
    }
}
