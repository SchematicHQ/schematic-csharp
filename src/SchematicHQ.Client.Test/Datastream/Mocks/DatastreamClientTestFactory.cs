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
        public static (DatastreamClient Client, MockWebSocket WebSocket, MockSchematicLogger Logger, Action<bool> ConnectionCallback) 
            CreateClientWithMocks(string apiKey = "test-api-key", TimeSpan? cacheTtl = null, Action<bool>? connectionCallback = null)
        {
            var logger = new MockSchematicLogger();
            var mockWebSocket = new MockWebSocket();
            mockWebSocket.SetState(WebSocketState.Open);
            
            var monitorCallback = connectionCallback ?? (isConnected => {});
            var client = new DatastreamClient("wss://test.example.com", logger, apiKey, monitorCallback, cacheTtl, mockWebSocket);
            
            return (client, mockWebSocket, logger, monitorCallback);
        }
    }
}
