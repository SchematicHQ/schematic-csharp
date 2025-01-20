using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record BillingProductPlanResponseData
{
    [JsonPropertyName("account_id")]
    public required string AccountId { get; init; }

    [JsonPropertyName("billing_product_id")]
    public required string BillingProductId { get; init; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; init; }

    [JsonPropertyName("is_trialable")]
    public required bool IsTrialable { get; init; }

    [JsonPropertyName("monthly_price_id")]
    public string? MonthlyPriceId { get; init; }

    [JsonPropertyName("plan_id")]
    public required string PlanId { get; init; }

    [JsonPropertyName("trial_days")]
    public int? TrialDays { get; init; }

    [JsonPropertyName("yearly_price_id")]
    public string? YearlyPriceId { get; init; }
}
