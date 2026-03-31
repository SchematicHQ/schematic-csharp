using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreateCompanyCreditGrant
{
    [JsonPropertyName("billing_periods_count")]
    public long? BillingPeriodsCount { get; set; }

    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    [JsonPropertyName("credit_id")]
    public required string CreditId { get; set; }

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("expires_at")]
    public DateTime? ExpiresAt { get; set; }

    [JsonPropertyName("expiry_type")]
    public BillingCreditExpiryType? ExpiryType { get; set; }

    [JsonPropertyName("expiry_unit")]
    public BillingCreditExpiryUnit? ExpiryUnit { get; set; }

    [JsonPropertyName("expiry_unit_count")]
    public long? ExpiryUnitCount { get; set; }

    [JsonPropertyName("quantity")]
    public required long Quantity { get; set; }

    [JsonPropertyName("reason")]
    public required BillingCreditGrantReason Reason { get; set; }

    [JsonPropertyName("renewal_enabled")]
    public bool? RenewalEnabled { get; set; }

    [JsonPropertyName("renewal_period")]
    public BillingPlanCreditGrantResetStart? RenewalPeriod { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
