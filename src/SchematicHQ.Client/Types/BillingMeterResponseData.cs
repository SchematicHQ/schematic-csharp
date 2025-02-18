using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record BillingMeterResponseData
{
    [JsonPropertyName("dispaly_name")]
    public required string DispalyName { get; set; }

    [JsonPropertyName("event_name")]
    public required string EventName { get; set; }

    [JsonPropertyName("event_payload_key")]
    public required string EventPayloadKey { get; set; }

    [JsonPropertyName("external_price_id")]
    public required string ExternalPriceId { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
