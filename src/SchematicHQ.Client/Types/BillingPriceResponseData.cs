using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record BillingPriceResponseData
{
    [JsonPropertyName("currency")]
    public required string Currency { get; set; }

    [JsonPropertyName("external_price_id")]
    public required string ExternalPriceId { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("interval")]
    public required string Interval { get; set; }

    [JsonPropertyName("price")]
    public required int Price { get; set; }
}
