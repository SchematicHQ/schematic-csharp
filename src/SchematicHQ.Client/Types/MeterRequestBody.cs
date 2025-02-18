using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record MeterRequestBody
{
    [JsonPropertyName("display_name")]
    public required string DisplayName { get; set; }

    [JsonPropertyName("event_name")]
    public required string EventName { get; set; }

    [JsonPropertyName("event_payload_key")]
    public required string EventPayloadKey { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
