using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class RawEventResponseData
{
    [JsonPropertyName("captured_at")]
    public DateTime CapturedAt { get; init; }

    [JsonPropertyName("event_id")]
    public string? EventId { get; init; }

    [JsonPropertyName("remote_addr")]
    public string RemoteAddr { get; init; }

    [JsonPropertyName("remote_ip")]
    public string RemoteIp { get; init; }

    [JsonPropertyName("user_agent")]
    public string UserAgent { get; init; }
}
