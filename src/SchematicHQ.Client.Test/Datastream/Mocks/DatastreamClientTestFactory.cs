using System.Net.WebSockets;
using Microsoft.Extensions.Logging.Testing;
using System.Reflection;
using SchematicHQ.Client.Datastream;

namespace SchematicHQ.Client.Test.Datastream.Mocks
{
    /// <summary>
    /// Helper to create instances of DatastreamClient with mock dependencies for testing
    /// </summary>
    public class DatastreamClientTestFactory
    {
        public static (DatastreamClient Client, MockWebSocket WebSocket, FakeLogger Logger, Action<bool> ConnectionCallback) 
            CreateClientWithMocks(string apiKey = "test-api-key", TimeSpan? cacheTtl = null, Action<bool>? connectionCallback = null)
        {
            var logger = new FakeLogger();
            var mockWebSocket = new MockWebSocket();
            mockWebSocket.SetState(WebSocketState.Open);
            
            var monitorCallback = connectionCallback ?? (isConnected => {});
            var client = new DatastreamClient("wss://test.example.com", logger, apiKey, monitorCallback, cacheTtl, mockWebSocket);
            
            return (client, mockWebSocket, logger, monitorCallback);
        }
    }
}
