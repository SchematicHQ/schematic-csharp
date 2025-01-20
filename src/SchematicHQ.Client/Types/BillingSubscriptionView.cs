using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record BillingSubscriptionView
{
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; init; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; init; }

    [JsonPropertyName("currency")]
    public required string Currency { get; init; }

    [JsonPropertyName("customer_external_id")]
    public required string CustomerExternalId { get; init; }

    [JsonPropertyName("discounts")]
    public IEnumerable<BillingSubscriptionDiscountView> Discounts { get; init; } =
        new List<BillingSubscriptionDiscountView>();

    [JsonPropertyName("expired_at")]
    public DateTime? ExpiredAt { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("interval")]
    public required string Interval { get; init; }

    [JsonPropertyName("latest_invoice")]
    public InvoiceResponseData? LatestInvoice { get; init; }

    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; init; }

    [JsonPropertyName("payment_method")]
    public PaymentMethodResponseData? PaymentMethod { get; init; }

    [JsonPropertyName("period_end")]
    public required int PeriodEnd { get; init; }

    [JsonPropertyName("period_start")]
    public required int PeriodStart { get; init; }

    [JsonPropertyName("products")]
    public IEnumerable<BillingProductForSubscriptionResponseData> Products { get; init; } =
        new List<BillingProductForSubscriptionResponseData>();

    [JsonPropertyName("status")]
    public required string Status { get; init; }

    [JsonPropertyName("subscription_external_id")]
    public required string SubscriptionExternalId { get; init; }

    [JsonPropertyName("total_price")]
    public required int TotalPrice { get; init; }

    [JsonPropertyName("trial_end")]
    public int? TrialEnd { get; init; }

    [JsonPropertyName("trial_end_setting")]
    public string? TrialEndSetting { get; init; }
}
