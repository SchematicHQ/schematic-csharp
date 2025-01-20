using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record BillingMeterResponseData
{
    [JsonPropertyName("dispaly_name")]
    public required string DispalyName { get; init; }

    [JsonPropertyName("event_name")]
    public required string EventName { get; init; }

    [JsonPropertyName("event_payload_key")]
    public required string EventPayloadKey { get; init; }

    [JsonPropertyName("external_price_id")]
    public required string ExternalPriceId { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }
}
