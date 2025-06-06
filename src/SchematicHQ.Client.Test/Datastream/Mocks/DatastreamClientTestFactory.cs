using System.Net.WebSockets;
using System.Reflection;
using SchematicHQ.Client.Datastream;

namespace SchematicHQ.Client.Test.Datastream.Mocks
{
    /// <summary>
    /// Helper to create instances of DatastreamClient with mock dependencies for testing
    /// </summary>
    public class DatastreamClientTestFactory
    {
        public static (DatastreamClient Client, MockWebSocket WebSocket, MockSchematicLogger Logger) CreateClientWithMocks(string apiKey = "test-api-key", TimeSpan? cacheTtl = null)
        {
            var logger = new MockSchematicLogger();
            var mockWebSocket = new MockWebSocket();
            mockWebSocket.SetState(WebSocketState.Open);
            
            var monitorSource = new TaskCompletionSource<bool>();
            var client = new DatastreamClient("wss://test.example.com", logger, apiKey, monitorSource, cacheTtl, mockWebSocket);
            
            return (client, mockWebSocket, logger);
        }
    }
}
