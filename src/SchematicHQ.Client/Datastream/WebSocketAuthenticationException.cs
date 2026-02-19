namespace SchematicHQ.Client.Datastream
{
  /// <summary>
  /// Exception thrown when the WebSocket server closes the connection due to an authentication error (close code 4001).
  /// This exception is non-retryable and indicates the API key is invalid.
  /// </summary>
  public class WebSocketAuthenticationException : SchematicException
  {
    public int CloseCode { get; }

    public WebSocketAuthenticationException(int closeCode, string? closeDescription = null)
        : base($"WebSocket authentication failed (close code {closeCode}): {closeDescription ?? "unauthorized"}. Please check your API key.")
    {
      CloseCode = closeCode;
    }
  }
}
