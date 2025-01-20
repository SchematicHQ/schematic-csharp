using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CreateBillingPriceRequestBody
{
    [JsonPropertyName("currency")]
    public required string Currency { get; init; }

    [JsonPropertyName("interval")]
    public required string Interval { get; init; }

    [JsonPropertyName("meter_id")]
    public string? MeterId { get; init; }

    [JsonPropertyName("price")]
    public required int Price { get; init; }

    [JsonPropertyName("price_external_id")]
    public required string PriceExternalId { get; init; }

    [JsonPropertyName("product_external_id")]
    public required string ProductExternalId { get; init; }

    [JsonPropertyName("usage_type")]
    public required string UsageType { get; init; }
}
