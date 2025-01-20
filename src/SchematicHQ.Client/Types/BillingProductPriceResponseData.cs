using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record BillingProductPriceResponseData
{
    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; init; }

    [JsonPropertyName("currency")]
    public required string Currency { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

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

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; init; }

    [JsonPropertyName("usage_type")]
    public required string UsageType { get; init; }
}
