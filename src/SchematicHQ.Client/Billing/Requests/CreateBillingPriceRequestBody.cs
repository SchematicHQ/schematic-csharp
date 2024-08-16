using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CreateBillingPriceRequestBody
{
    [JsonPropertyName("interval")]
    public required string Interval { get; init; }

    [JsonPropertyName("price")]
    public required int Price { get; init; }

    [JsonPropertyName("price_external_id")]
    public required string PriceExternalId { get; init; }

    [JsonPropertyName("product_external_id")]
    public required string ProductExternalId { get; init; }
}
