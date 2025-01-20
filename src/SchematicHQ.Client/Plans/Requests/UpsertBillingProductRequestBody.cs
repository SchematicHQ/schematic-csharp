using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record UpsertBillingProductRequestBody
{
    [JsonPropertyName("billing_product_id")]
    public string? BillingProductId { get; init; }

    [JsonPropertyName("is_free_plan")]
    public required bool IsFreePlan { get; init; }

    [JsonPropertyName("is_trialable")]
    public required bool IsTrialable { get; init; }

    [JsonPropertyName("monthly_price_id")]
    public string? MonthlyPriceId { get; init; }

    [JsonPropertyName("trial_days")]
    public int? TrialDays { get; init; }

    [JsonPropertyName("yearly_price_id")]
    public string? YearlyPriceId { get; init; }
}
