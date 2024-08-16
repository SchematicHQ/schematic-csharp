using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record BillingPriceResponseData
{
    [JsonPropertyName("external_price_id")]
    public required string ExternalPriceId { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("interval")]
    public required string Interval { get; init; }

    [JsonPropertyName("price")]
    public required int Price { get; init; }
}
