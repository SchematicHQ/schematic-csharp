using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using NUnit.Framework;
using SchematicHQ.Client.Datastream;
using SchematicHQ.Client.Test.Datastream.Mocks;

namespace SchematicHQ.Client.Test.Datastream
{
    [TestFixture]
    public class DatastreamConnectionTests
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
        public void ErrorMessage_IsHandledCorrectly()
        {
            // Arrange
            var errorMessage = new DataStreamResponse
            {
                MessageType = MessageType.Error,
                Data = JsonDocument.Parse(JsonSerializer.Serialize(new DataStreamError { Error = "Test error message" })).RootElement
            };
            
            _mockWebSocket.SetupToReceive(JsonSerializer.Serialize(errorMessage));
            
            // Act - Start processing messages
            _client.Start();
            
            // Give time for async operations to complete
            Task.Delay(100).Wait();
            
            // Assert
            Assert.That(_mockLogger.HasLogEntry(LogLevel.Error, "Received error from server"), Is.True);
        }
        
        [Test]
        public async Task GetCompanyAsync_WithTimeout_ThrowsTimeoutException()
        {
            // Arrange - We don't setup any response, so the request will time out
            
            // Act & Assert
            var request = new Dictionary<string, string> { { "id", "company-timeout" } };
            
            // Use a short timeout for testing
            var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(50));
            
            // Get the method using reflection since it's private
            var methodInfo = _client.GetType().GetMethod("GetCompanyAsync", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            
            // The test is expected to throw a TimeoutException
            Assert.ThrowsAsync<TimeoutException>(async () => 
            {
                await (Task)methodInfo.Invoke(_client, new object[] { request, cts.Token });
            });
        }
        
        [Test]
        public async Task GetUserAsync_WithTimeout_ThrowsTimeoutException()
        {
            // Arrange - We don't setup any response, so the request will time out
            
            // Act & Assert
            var request = new Dictionary<string, string> { { "id", "user-timeout" } };
            
            // Use a short timeout for testing
            var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(50));
            
            // Get the method using reflection since it's private
            var methodInfo = _client.GetType().GetMethod("GetUserAsync", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            
            // The test is expected to throw a TimeoutException
            Assert.ThrowsAsync<TimeoutException>(async () => 
            {
                await (Task)methodInfo.Invoke(_client, new object[] { request, cts.Token });
            });
        }
        
        [Test]
        public async Task GetAllFlagsAsync_HandlesTimeout()
        {
            // Arrange - We don't setup any response, so the request will time out
            
            // Act & Assert
            // Use a short timeout for testing
            var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(50));
            
            // Get the method using reflection since it's private
            var methodInfo = _client.GetType().GetMethod("GetAllFlagsAsync", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            
            // The test is expected to throw a TimeoutException
            Assert.ThrowsAsync<TimeoutException>(async () => 
            {
                await (Task)methodInfo.Invoke(_client, new object[] { cts.Token });
            });
        }
    }
}
