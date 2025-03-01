using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record SegmentStatusResp
{
    [JsonPropertyName("connected")]
    public required bool Connected { get; set; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; set; }

    [JsonPropertyName("last_event_at")]
    public DateTime? LastEventAt { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
