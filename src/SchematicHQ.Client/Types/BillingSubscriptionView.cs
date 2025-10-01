using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record BillingSubscriptionView : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("cancel_at")]
    public int? CancelAt { get; set; }

    [JsonPropertyName("cancel_at_period_end")]
    public required bool CancelAtPeriodEnd { get; set; }

    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("currency")]
    public required string Currency { get; set; }

    [JsonPropertyName("customer_external_id")]
    public required string CustomerExternalId { get; set; }

    [JsonPropertyName("default_payment_method_id")]
    public string? DefaultPaymentMethodId { get; set; }

    [JsonPropertyName("discounts")]
    public IEnumerable<BillingSubscriptionDiscountView> Discounts { get; set; } =
        new List<BillingSubscriptionDiscountView>();

    [JsonPropertyName("expired_at")]
    public DateTime? ExpiredAt { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("interval")]
    public required string Interval { get; set; }

    [JsonPropertyName("latest_invoice")]
    public InvoiceResponseData? LatestInvoice { get; set; }

    [JsonPropertyName("metadata")]
    public Dictionary<string, object?>? Metadata { get; set; }

    [JsonPropertyName("payment_method")]
    public PaymentMethodResponseData? PaymentMethod { get; set; }

    [JsonPropertyName("period_end")]
    public required int PeriodEnd { get; set; }

    [JsonPropertyName("period_start")]
    public required int PeriodStart { get; set; }

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
    public int? TrialEnd { get; set; }

    [JsonPropertyName("trial_end_setting")]
    public string? TrialEndSetting { get; set; }

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
