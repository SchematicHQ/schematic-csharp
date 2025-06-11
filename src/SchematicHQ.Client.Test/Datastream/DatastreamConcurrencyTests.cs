using System.Text;
using System.Text.Json;
using NUnit.Framework;
using RulesEngine;
using RulesEngine.Models;
using RulesEngine.Utils;
using SchematicHQ.Client.Datastream;
using SchematicHQ.Client.Test.Datastream.Mocks;

namespace SchematicHQ.Client.Test.Datastream
{
    [TestFixture]
    public class DatastreamConcurrencyTests
    {
        private MockWebSocket _mockWebSocket;
        private MockSchematicLogger _mockLogger;
        private DatastreamClient _client;
        
        [SetUp]
        public void Setup()
        {
            var testSetup = DatastreamClientTestFactory.CreateClientWithMocks();
            _client = testSetup.Client;
            _mockWebSocket = testSetup.WebSocket;
            _mockLogger = testSetup.Logger;
        }
        
        [Test]
        public async Task MultipleConcurrentRequests_ForSameCompany_OnlySendsOneWebSocketRequest()
        {
            // Arrange
            var companyResponse = new DataStreamResponse
            {
                MessageType = MessageType.Full,
                EntityType = Client.Datastream.EntityType.Company,
                Data = JsonDocument.Parse(JsonSerializer.Serialize(new Company
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
            
            // Setup fake WebSocket response that will come after all requests are made
            _mockWebSocket.SetupToReceive(JsonSerializer.Serialize(companyResponse));
            
            var request = new CheckFlagRequestBody
            {
                Company = new Dictionary<string, string> { { "id", "company-123" } }
            };
            
            // Act - Create multiple concurrent requests
            var tasks = new List<Task>();
            for (int i = 0; i < 5; i++)
            {
                tasks.Add(Task.Run(async () => 
                {
                    await _client.CheckFlagAsync(request, "test-flag");
                }));
            }
            
            // Wait for all tasks to complete
            await Task.WhenAll(tasks);
            
            // Assert
            // Even though we made 5 concurrent requests, we should only see 1 WebSocket request
            Assert.That(_mockWebSocket.SentMessages.Count, Is.EqualTo(1),
                "Multiple concurrent requests for the same resource should only result in one WebSocket message");
        }
        
        [Test]
        public async Task EmptyResponse_LogsWarning()
        {
            // Arrange - Set up a response with null data
            var companyResponse = new DataStreamResponse
            {
                MessageType = MessageType.Full,
                EntityType = Client.Datastream.EntityType.Company,
                Data = null
            };
            
            // Setup fake WebSocket response
            _mockWebSocket.SetupToReceive(JsonSerializer.Serialize(companyResponse));
            
            // Act
            _client.Start();
            
            // Wait for message processing
            await Task.Delay(100);
            
            // Assert
            Assert.That(_mockLogger.HasLogEntry(LogLevel.Warn, "Received empty company data"), Is.True);
        }
        
        [Test]
        public void MessageDeserialization_HandlesErrors()
        {
            // Arrange - Set up an invalid JSON response
            _mockWebSocket.SetupToReceive("{ invalid json }");
            
            // Act
            _client.Start();
            
            // Wait for message processing
            Task.Delay(100).Wait();
            
            // Assert
            Assert.That(_mockLogger.HasLogEntry(LogLevel.Error, "Failed to process WebSocket message"), Is.True);
        }
        
        [Test]
        public async Task RequestsPendingWhenResourceArrives_AllAreNotified()
        {
            // Arrange
            var companyResponse = new DataStreamResponse
            {
                MessageType = MessageType.Full,
                EntityType = Client.Datastream.EntityType.Company,
                Data = JsonDocument.Parse(JsonSerializer.Serialize(new Company
                {
                    Id = "comp_123",
                    AccountId = "acc_123",
                    EnvironmentId = "env_123",
                    Keys = new Dictionary<string, string> 
                    { 
                        { "id", "company-123" } 
                    }
                })).RootElement
            };
            
            // Don't set up the response yet - we'll do it after initiating requests
            
            var request = new CheckFlagRequestBody
            {
                Company = new Dictionary<string, string> { { "id", "company-123" } }
            };
            
            // Act - Create multiple concurrent requests
            var tasks = new List<Task<CheckFlagResult>>();
            for (int i = 0; i < 3; i++)
            {
                tasks.Add(_client.CheckFlagAsync(request, "test-flag"));
            }
            
            // Now set up the response that will fulfill all requests
            _mockWebSocket.SetupToReceive(JsonSerializer.Serialize(companyResponse));
            
            // Start the client to process the response
            _client.Start();
            
            // Wait for all tasks to complete
            await Task.WhenAll(tasks);
            
            // Assert
            // We should have made only 1 WebSocket request
            Assert.That(_mockWebSocket.SentMessages.Count, Is.EqualTo(1));
            
            // All tasks should have completed successfully
            foreach (var task in tasks)
            {
                Assert.That(task.IsCompletedSuccessfully, Is.True);
            }
        }
    }
}
