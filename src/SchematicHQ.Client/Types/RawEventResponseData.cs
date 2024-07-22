using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record RawEventResponseData
{
    [JsonPropertyName("captured_at")]
    public required DateTime CapturedAt { get; init; }

    [JsonPropertyName("event_id")]
    public string? EventId { get; init; }

    [JsonPropertyName("remote_addr")]
    public required string RemoteAddr { get; init; }

    [JsonPropertyName("remote_ip")]
    public required string RemoteIp { get; init; }

    [JsonPropertyName("user_agent")]
    public required string UserAgent { get; init; }
}
