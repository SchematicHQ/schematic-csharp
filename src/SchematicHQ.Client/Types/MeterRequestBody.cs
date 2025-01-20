using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record MeterRequestBody
{
    [JsonPropertyName("display_name")]
    public required string DisplayName { get; init; }

    [JsonPropertyName("event_name")]
    public required string EventName { get; init; }

    [JsonPropertyName("event_payload_key")]
    public required string EventPayloadKey { get; init; }
}
