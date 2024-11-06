using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record BillingProductPlanResponseData
{
    [JsonPropertyName("account_id")]
    public required string AccountId { get; set; }

    [JsonPropertyName("billing_product_id")]
    public required string BillingProductId { get; set; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; set; }

    [JsonPropertyName("monthly_price_id")]
    public string? MonthlyPriceId { get; set; }

    [JsonPropertyName("plan_id")]
    public required string PlanId { get; set; }

    [JsonPropertyName("yearly_price_id")]
    public string? YearlyPriceId { get; set; }
}
