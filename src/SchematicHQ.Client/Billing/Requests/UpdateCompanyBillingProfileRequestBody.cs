using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdateCompanyBillingProfileRequestBody
{
    [JsonPropertyName("billing_details")]
    public ContractBillingDetailsRequestBody? BillingDetails { get; set; }

    [JsonPropertyName("collection_method")]
    public required BillingCollectionMethod CollectionMethod { get; set; }

    [JsonPropertyName("collector")]
    public Collector? Collector { get; set; }

    [JsonPropertyName("currency")]
    public Currency? Currency { get; set; }

    [JsonPropertyName("days_until_due")]
    public long? DaysUntilDue { get; set; }

    [JsonPropertyName("gating_days")]
    public long? GatingDays { get; set; }

    [JsonPropertyName("is_default")]
    public bool? IsDefault { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("overdraft_limit")]
    public long? OverdraftLimit { get; set; }

    [JsonPropertyName("payment_gating")]
    public PaymentGating? PaymentGating { get; set; }

    [JsonPropertyName("payment_method_id")]
    public string? PaymentMethodId { get; set; }

    [JsonPropertyName("proration_behavior")]
    public ProrationBehavior? ProrationBehavior { get; set; }

    [JsonPropertyName("purchase_order")]
    public string? PurchaseOrder { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
