using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record CompanySubscriptionResponseData
{
    [JsonPropertyName("cancel_at")]
    public DateTime? CancelAt { get; set; }

    [JsonPropertyName("cancel_at_period_end")]
    public required bool CancelAtPeriodEnd { get; set; }

    [JsonPropertyName("currency")]
    public required string Currency { get; set; }

    [JsonPropertyName("customer_external_id")]
    public required string CustomerExternalId { get; set; }

    [JsonPropertyName("discounts")]
    public IEnumerable<BillingSubscriptionDiscountView> Discounts { get; set; } =
        new List<BillingSubscriptionDiscountView>();

    [JsonPropertyName("expired_at")]
    public DateTime? ExpiredAt { get; set; }

    [JsonPropertyName("interval")]
    public required string Interval { get; set; }

    [JsonPropertyName("latest_invoice")]
    public InvoiceResponseData? LatestInvoice { get; set; }

    [JsonPropertyName("payment_method")]
    public PaymentMethodResponseData? PaymentMethod { get; set; }

    [JsonPropertyName("products")]
    public IEnumerable<BillingProductForSubscriptionResponseData> Products { get; set; } =
        new List<BillingProductForSubscriptionResponseData>();

    [JsonPropertyName("status")]
    public required string Status { get; set; }

    [JsonPropertyName("subscription_external_id")]
    public required string SubscriptionExternalId { get; set; }

    [JsonPropertyName("total_price")]
    public required int TotalPrice { get; set; }

    [JsonPropertyName("trial_end")]
    public DateTime? TrialEnd { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
