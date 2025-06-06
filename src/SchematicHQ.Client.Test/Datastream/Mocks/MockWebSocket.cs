using System.Net.WebSockets;
using System.Text;
using SchematicHQ.Client.Datastream;

namespace SchematicHQ.Client.Test.Datastream.Mocks
{
    public class MockWebSocket : IWebSocketClient
    {
        private readonly List<byte[]> _receivedMessages = new();
        private readonly Queue<byte[]> _messagesToSend = new();
        private readonly Queue<WebSocketReceiveResult> _receiveResults = new();
        private WebSocketState _state = WebSocketState.None;
        private readonly MockWebSocketOptions _options = new();

        public WebSocketState State => _state;
        public WebSocketCloseStatus? CloseStatus => null;
        public string? CloseStatusDescription => null;
        public IWebSocketOptions Options => _options;
        
        public List<byte[]> SentMessages { get; } = new();
        public Action<ArraySegment<byte>, WebSocketMessageType, bool, CancellationToken>? OnSendAsync { get; set; }

        public void SetupToReceive(string message)
        {
            var bytes = Encoding.UTF8.GetBytes(message);
            _messagesToSend.Enqueue(bytes);
            _receiveResults.Enqueue(new WebSocketReceiveResult(
                bytes.Length,
                WebSocketMessageType.Text,
                true));
        }

        public void SetupToReceiveBinary(byte[] data)
        {
            _messagesToSend.Enqueue(data);
            _receiveResults.Enqueue(new WebSocketReceiveResult(
                data.Length,
                WebSocketMessageType.Binary,
                true));
        }

        public void SetupToReceiveClose()
        {
            _messagesToSend.Enqueue(Array.Empty<byte>());
            _receiveResults.Enqueue(new WebSocketReceiveResult(
                0,
                WebSocketMessageType.Close,
                true,
                WebSocketCloseStatus.NormalClosure,
                "Closed"));
        }

        public void SetState(WebSocketState state)
        {
            _state = state;
        }
        
        public void Abort()
        {
            _state = WebSocketState.Aborted;
        }

        public Task ConnectAsync(Uri uri, CancellationToken cancellationToken)
        {
            _state = WebSocketState.Open;
            return Task.CompletedTask;
        }

        public Task CloseAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
        {
            _state = WebSocketState.Closed;
            return Task.CompletedTask;
        }

        public Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _state = WebSocketState.Aborted;
        }

        public Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
        {
            if (_messagesToSend.Count == 0)
            {
                throw new WebSocketException("Connection closed");
            }

            var message = _messagesToSend.Dequeue();
            var result = _receiveResults.Dequeue();

            Array.Copy(message, 0, buffer.Array!, buffer.Offset, Math.Min(message.Length, buffer.Count));
            return Task.FromResult(result);
        }

        public Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
        {
            OnSendAsync?.Invoke(buffer, messageType, endOfMessage, cancellationToken);

            if (buffer.Count > 0)
            {
                var bytes = new byte[buffer.Count];
                Array.Copy(buffer.Array!, buffer.Offset, bytes, 0, buffer.Count);
                SentMessages.Add(bytes);
            }
            
            return Task.CompletedTask;
        }

        private class MockWebSocketOptions : IWebSocketOptions
        {
            private readonly Dictionary<string, string> _headers = new();
            
            public TimeSpan KeepAliveInterval { get; set; } = TimeSpan.FromSeconds(30);
            
            public void SetRequestHeader(string headerName, string headerValue)
            {
                _headers[headerName] = headerValue;
            }
            
            public string GetRequestHeader(string name)
            {
                return _headers.TryGetValue(name, out var value) ? value : string.Empty;
            }
        }
    }
}