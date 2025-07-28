using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using NUnit.Framework;
using SchematicHQ.Client.RulesEngine.Models;
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
        public async Task CheckFlagAsync_WhenFlagIsNotInCache_ReturnsFalse()
        {
            // Arrange
            var request = new CheckFlagRequestBody
            {
                Company = new Dictionary<string, string>
                {
                    { "id", "company-123" }
                }
            };
            
            // Act
            var result = await _client.CheckFlagAsync(request, "non-existent-flag");
            
            // Assert
            Assert.That(result.Value, Is.False);
        }
        
        [Test]
        public async Task CheckFlagAsync_WhenFlagExists_EvaluatesCorrectly()
        {
            // Arrange
            SetupFlagsResponse();

            // Start the client to process the response
            _client.Start();
            
            var companyResponse = new DataStreamResponse
            {
                MessageType = MessageType.Full,
                EntityType = SchematicHQ.Client.Datastream.EntityType.Company,
                Data = JsonDocument.Parse(JsonSerializer.Serialize(new Company
                {
                    Id = "comp_123",
                    AccountId = "acc_123",
                    EnvironmentId = "env_123",
                    Keys = new Dictionary<string, string> 
                    { 
                        { "id", "company-123" } 
                    },
                    Traits = new List<Trait>
                    {
                        new Trait { Value = "pro", TraitDefinition = new TraitDefinition { Id = "trait_123", ComparableType = ComparableType.String, EntityType = SchematicHQ.Client.RulesEngine.EntityType.Company } }
                    }
                }, _jsonOptions)).RootElement
            };
            
            // Setup fake WebSocket responses for flags and company
            _mockWebSocket.SetupToReceive(JsonSerializer.Serialize(companyResponse));
            
            var request = new CheckFlagRequestBody
            {
                Company = new Dictionary<string, string>
                {
                    { "id", "company-123" }
                }
            };
            
            // Act
            var result = await _client.CheckFlagAsync(request, "another-feature");
            
            // Assert
            Assert.That(result.Value, Is.True);
            Assert.That(_mockWebSocket.SentMessages.Count, Is.EqualTo(1));
            
            // Verify the WebSocket message sent was for the company lookup
            var sentMessage = Encoding.UTF8.GetString(_mockWebSocket.SentMessages[0]);
            var dataStreamRequest = JsonSerializer.Deserialize<DataStreamBaseRequest>(sentMessage);
            Assert.That(dataStreamRequest?.Data.EntityType, Is.EqualTo(SchematicHQ.Client.Datastream.EntityType.Company));
        }
        
        [Test]
        public async Task CachedResources_AreReusedWithoutWebSocketRequests()
        {
            // Arrange
            SetupFlagsResponse();
            
            var companyResponse = new DataStreamResponse
            {
                MessageType = MessageType.Full,
                EntityType = SchematicHQ.Client.Datastream.EntityType.Company,
                Data = JsonDocument.Parse(JsonSerializer.Serialize(new Company
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
            await _client.CheckFlagAsync(request, "new-event-name");
            int initialRequestCount = _mockWebSocket.SentMessages.Count;
            
            // Clear sent messages to track new ones
            _mockWebSocket.SentMessages.Clear();
            
            // Act - Second request should use cache
            await _client.CheckFlagAsync(request, "new-event-name");
            
            // Assert
            Assert.That(_mockWebSocket.SentMessages.Count, Is.EqualTo(initialRequestCount), "No new WebSocket messages should be sent when using cached data");
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
            var mockFlags = new List<Flag>
            {
                new Flag
                {
                    AccountId = "acc_123",
                    EnvironmentId = "env_123",
                    Id = "flag_123",
                    Key = "new-event-name",

                    DefaultValue = false,
                    Rules = new List<Rule>
                    {
                        new Rule
                        {
                            AccountId = "acc_123",
                            EnvironmentId = "env_123",
                            Id = "rule_123",
                            Name = "Test Rule",
                            Conditions = new List<Condition>(),
                            Value = true
                        }
                    }
                },
                new Flag
                {
                    AccountId = "acc_123",
                    EnvironmentId = "env_123",
                    Id = "flag_456",
                    Key = "another-feature",
                    DefaultValue = true,
                    Rules = new List<Rule>()
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
