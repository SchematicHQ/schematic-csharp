using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record UpsertBillingProductRequestBody
{
    [JsonPropertyName("billing_product_id")]
    public required string BillingProductId { get; init; }

    [JsonPropertyName("monthly_price_id")]
    public string? MonthlyPriceId { get; init; }

    [JsonPropertyName("yearly_price_id")]
    public string? YearlyPriceId { get; init; }
}
