using System.Net.WebSockets;


namespace SchematicHQ.Client.Datastream
{
  public interface IWebSocketClient
  {
    WebSocketState State { get; }
    WebSocketCloseStatus? CloseStatus { get; }
    string? CloseStatusDescription { get; }

    // WebSocket options
    IWebSocketOptions Options { get; }

    Task ConnectAsync(Uri uri, CancellationToken cancellationToken);
    Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken);
    Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken);
    Task CloseAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken);
    Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken);
    void Abort();
    void Dispose();
  }
  
      /// <summary>
    /// Interface for WebSocket options
    /// </summary>
    public interface IWebSocketOptions
    {
        TimeSpan KeepAliveInterval { get; set; }
        void SetRequestHeader(string headerName, string headerValue);
    }
  
  /// <summary>
  /// Implementation of IWebSocketClient that wraps the standard ClientWebSocket
  /// </summary>
  public class StandardWebSocketClient : IWebSocketClient
  {
    private readonly ClientWebSocket _clientWebSocket;
    private readonly StandardWebSocketOptions _options;

    public StandardWebSocketClient()
    {
      _clientWebSocket = new ClientWebSocket();
      _options = new StandardWebSocketOptions(_clientWebSocket.Options);
    }

    public WebSocketState State => _clientWebSocket.State;
    public WebSocketCloseStatus? CloseStatus => _clientWebSocket.CloseStatus;
    public string? CloseStatusDescription => _clientWebSocket.CloseStatusDescription;
    public IWebSocketOptions Options => _options;

    public Task ConnectAsync(Uri uri, CancellationToken cancellationToken)
        => _clientWebSocket.ConnectAsync(uri, cancellationToken);

    public Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
        => _clientWebSocket.SendAsync(buffer, messageType, endOfMessage, cancellationToken);

    public Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
        => _clientWebSocket.ReceiveAsync(buffer, cancellationToken);

    public Task CloseAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
        => _clientWebSocket.CloseAsync(closeStatus, statusDescription, cancellationToken);

    public Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
        => _clientWebSocket.CloseOutputAsync(closeStatus, statusDescription, cancellationToken);

    public void Abort() => _clientWebSocket.Abort();

    public void Dispose() => _clientWebSocket.Dispose();

    private class StandardWebSocketOptions : IWebSocketOptions
    {
      private readonly ClientWebSocketOptions _options;

      public StandardWebSocketOptions(ClientWebSocketOptions options)
      {
        _options = options;
      }

      public TimeSpan KeepAliveInterval
      {
        get => _options.KeepAliveInterval;
        set => _options.KeepAliveInterval = value;
      }

      public void SetRequestHeader(string headerName, string headerValue)
          => _options.SetRequestHeader(headerName, headerValue);
    }
  }
}