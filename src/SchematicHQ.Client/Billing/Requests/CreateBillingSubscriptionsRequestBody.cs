using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record CreateBillingSubscriptionsRequestBody
{
    [JsonPropertyName("currency")]
    public required string Currency { get; init; }

    [JsonPropertyName("customer_external_id")]
    public required string CustomerExternalId { get; init; }

    [JsonPropertyName("discounts")]
    public IEnumerable<BillingSubscriptionDiscount> Discounts { get; init; } =
        new List<BillingSubscriptionDiscount>();

    [JsonPropertyName("expired_at")]
    public required DateTime ExpiredAt { get; init; }

    [JsonPropertyName("interval")]
    public string? Interval { get; init; }

    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; init; }

    [JsonPropertyName("period_end")]
    public int? PeriodEnd { get; init; }

    [JsonPropertyName("period_start")]
    public int? PeriodStart { get; init; }

    [JsonPropertyName("product_external_ids")]
    public IEnumerable<BillingProductPricing> ProductExternalIds { get; init; } =
        new List<BillingProductPricing>();

    [JsonPropertyName("status")]
    public string? Status { get; init; }

    [JsonPropertyName("subscription_external_id")]
    public required string SubscriptionExternalId { get; init; }

    [JsonPropertyName("total_price")]
    public required int TotalPrice { get; init; }

    [JsonPropertyName("trial_end")]
    public int? TrialEnd { get; init; }

    [JsonPropertyName("trial_end_setting")]
    public string? TrialEndSetting { get; init; }
}
