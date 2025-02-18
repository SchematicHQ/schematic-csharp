using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record RawEventResponseData
{
    [JsonPropertyName("captured_at")]
    public required DateTime CapturedAt { get; set; }

    [JsonPropertyName("event_id")]
    public string? EventId { get; set; }

    [JsonPropertyName("remote_addr")]
    public required string RemoteAddr { get; set; }

    [JsonPropertyName("remote_ip")]
    public required string RemoteIp { get; set; }

    [JsonPropertyName("user_agent")]
    public required string UserAgent { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
