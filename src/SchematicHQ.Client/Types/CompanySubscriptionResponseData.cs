using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record CompanySubscriptionResponseData
{
    [JsonPropertyName("currency")]
    public required string Currency { get; init; }

    [JsonPropertyName("customer_external_id")]
    public required string CustomerExternalId { get; init; }

    [JsonPropertyName("discounts")]
    public IEnumerable<BillingSubscriptionDiscountView> Discounts { get; init; } =
        new List<BillingSubscriptionDiscountView>();

    [JsonPropertyName("expired_at")]
    public DateTime? ExpiredAt { get; init; }

    [JsonPropertyName("interval")]
    public required string Interval { get; init; }

    [JsonPropertyName("latest_invoice")]
    public InvoiceResponseData? LatestInvoice { get; init; }

    [JsonPropertyName("payment_method")]
    public PaymentMethodResponseData? PaymentMethod { get; init; }

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
    public DateTime? TrialEnd { get; init; }
}
