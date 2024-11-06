using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record SegmentStatusResp
{
    [JsonPropertyName("connected")]
    public required bool Connected { get; set; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; set; }

    [JsonPropertyName("last_event_at")]
    public DateTime? LastEventAt { get; set; }
}
