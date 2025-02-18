using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record CreateBillingSubscriptionsRequestBody
{
    [JsonPropertyName("currency")]
    public required string Currency { get; set; }

    [JsonPropertyName("customer_external_id")]
    public required string CustomerExternalId { get; set; }

    [JsonPropertyName("discounts")]
    public IEnumerable<BillingSubscriptionDiscount> Discounts { get; set; } =
        new List<BillingSubscriptionDiscount>();

    [JsonPropertyName("expired_at")]
    public required DateTime ExpiredAt { get; set; }

    [JsonPropertyName("interval")]
    public string? Interval { get; set; }

    [JsonPropertyName("metadata")]
    public object? Metadata { get; set; }

    [JsonPropertyName("period_end")]
    public int? PeriodEnd { get; set; }

    [JsonPropertyName("period_start")]
    public int? PeriodStart { get; set; }

    [JsonPropertyName("product_external_ids")]
    public IEnumerable<BillingProductPricing> ProductExternalIds { get; set; } =
        new List<BillingProductPricing>();

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("subscription_external_id")]
    public required string SubscriptionExternalId { get; set; }

    [JsonPropertyName("total_price")]
    public required int TotalPrice { get; set; }

    [JsonPropertyName("trial_end")]
    public int? TrialEnd { get; set; }

    [JsonPropertyName("trial_end_setting")]
    public string? TrialEndSetting { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
