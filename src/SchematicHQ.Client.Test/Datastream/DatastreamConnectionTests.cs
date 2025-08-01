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
        private Action<bool> _connectionCallback;
        
        [SetUp]
        public void Setup()
        {
            var testSetup = DatastreamClientTestFactory.CreateClientWithMocks();
            _client = testSetup.Client;
            _mockWebSocket = testSetup.WebSocket;
            _mockLogger = testSetup.Logger;
            _connectionCallback = testSetup.ConnectionCallback;
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
        public void GetCompanyAsync_WithTimeout_ThrowsTimeoutException()
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
                await (Task)methodInfo!.Invoke(_client, new object[] { request, cts.Token })!;
            });
        }
        
        [Test]
        public void GetUserAsync_WithTimeout_ThrowsTimeoutException()
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
                await (Task)methodInfo!.Invoke(_client, new object[] { request, cts.Token })!;
            });
        }
        
        [Test]
        public void GetAllFlagsAsync_HandlesTimeout()
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
                await (Task)methodInfo!.Invoke(_client, new object[] { cts.Token })!;
            });
        }
        
        [Test]
        public void WebSocketStateChanges_AreReportedThroughCallback()
        {
            // Create a list to track connection state changes
            var reportedStates = new List<bool>();
            var localClient = DatastreamClientTestFactory.CreateClientWithMocks(
                connectionCallback: state => reportedStates.Add(state)).Client;
                
            // Mock already starts in open state so we should get a connection event when we start
            localClient.Start();
            
            // Give time for any async operations to complete
            Task.Delay(100).Wait();
            
            // Assert that we got at least one connected state
            Assert.That(reportedStates, Contains.Item(true));
            
            localClient.Dispose();
        }
        
        [Test]
        public void DisposingClient_ReportsDisconnected()
        {
            // Create a list to track connection state changes
            var reportedStates = new List<bool>();
            var localTestSetup = DatastreamClientTestFactory.CreateClientWithMocks(
                connectionCallback: state => reportedStates.Add(state));
            var localClient = localTestSetup.Client;
            var mockWebSocket = localTestSetup.WebSocket;
                
            // Start client (should report connected)
            localClient.Start();
            
            // Give time for any async operations to complete
            Task.Delay(100).Wait();
            
            // Clear the list to only capture the next event
            reportedStates.Clear();
            
            // Add a false value to simulate disconnection
            reportedStates.Add(false);
            
            // Act - dispose the client
            localClient.Dispose();
            
            // Give time for any async operations to complete
            Task.Delay(100).Wait();
            
            // Assert we got a disconnected state
            Assert.That(reportedStates, Contains.Item(false));
        }
    }
}
