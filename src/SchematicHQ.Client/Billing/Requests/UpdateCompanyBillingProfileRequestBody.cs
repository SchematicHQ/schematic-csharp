using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdateCompanyBillingProfileRequestBody
{
    [JsonPropertyName("collection_method")]
    public required BillingCollectionMethod CollectionMethod { get; set; }

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

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
