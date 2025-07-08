using System.Text.Json;
using NUnit.Framework;
using SchematicHQ.Client.RulesEngine.Models;
using SchematicHQ.Client.Datastream;
using SchematicHQ.Client.Test.Datastream.Mocks;

namespace SchematicHQ.Client.Test.Datastream
{
    [TestFixture]
    public class DatastreamCacheTests
    {
        private MockWebSocket _mockWebSocket;
        private MockSchematicLogger _mockLogger;
        private DatastreamClient _client;
        
        [SetUp]
        public void Setup()
        {
            var testSetup = DatastreamClientTestFactory.CreateClientWithMocks(cacheTtl: TimeSpan.FromMilliseconds(100));
            _client = testSetup.Client;
            _mockWebSocket = testSetup.WebSocket;
            _mockLogger = testSetup.Logger;
        }
        
        [Test]
        public async Task ExpiredCache_RequestsResourcesAgain()
        {
            // Arrange
            var companyResponse = new DataStreamResponse
            {
                MessageType = MessageType.Full,
                EntityType = EntityType.Company,
                Data =JsonDocument.Parse(JsonSerializer.Serialize(new Company
                {
                    AccountId = "acc_123",
                    EnvironmentId = "env_123",
                    Id = "comp_123",
                    Keys = new Dictionary<string, string> 
                    { 
                        { "id", "company-123" } 
                    }
                })).RootElement
            };
            
            // Setup fake WebSocket responses
            _mockWebSocket.SetupToReceive(JsonSerializer.Serialize(companyResponse));
            
            // Setup second response for after cache expires
            _mockWebSocket.SetupToReceive(JsonSerializer.Serialize(companyResponse));
            
            var request = new CheckFlagRequestBody
            {
                Company = new Dictionary<string, string> { { "id", "company-123" } }
            };
            
            // Act - First request
            await _client.CheckFlagAsync(request, "test-flag");
            int initialRequestCount = _mockWebSocket.SentMessages.Count;
            
            // Wait for the cache to expire
            await Task.Delay(150);
            
            // Act - Second request should make a new WebSocket request
            await _client.CheckFlagAsync(request, "test-flag");
            
            // Assert
            Assert.That(_mockWebSocket.SentMessages.Count, Is.EqualTo(initialRequestCount + 1), 
                "A new WebSocket request should be made when cache expires");
        }       
    }
}
