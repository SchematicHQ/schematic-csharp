using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CreateMeterRequestBody
{
    [JsonPropertyName("display_name")]
    public required string DisplayName { get; init; }

    [JsonPropertyName("event_name")]
    public required string EventName { get; init; }

    [JsonPropertyName("event_payload_key")]
    public required string EventPayloadKey { get; init; }

    [JsonPropertyName("external_id")]
    public required string ExternalId { get; init; }
}
