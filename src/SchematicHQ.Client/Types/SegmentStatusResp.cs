using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class SegmentStatusResp
{
    [JsonPropertyName("connected")]
    public bool Connected { get; init; }

    [JsonPropertyName("environment_id")]
    public string EnvironmentId { get; init; }

    [JsonPropertyName("last_event_at")]
    public DateTime? LastEventAt { get; init; }
}
