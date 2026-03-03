using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreditEventLedgerResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("amount")]
    public required double Amount { get; set; }

    [JsonPropertyName("auto_topup_log_id")]
    public string? AutoTopupLogId { get; set; }

    [JsonPropertyName("billing_credit_bundle_id")]
    public string? BillingCreditBundleId { get; set; }

    [JsonPropertyName("billing_credit_id")]
    public required string BillingCreditId { get; set; }

    [JsonPropertyName("company")]
    public CompanyLedgerResponseData? Company { get; set; }

    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    [JsonPropertyName("credit")]
    public BillingCreditLedgerResponseData? Credit { get; set; }

    [JsonPropertyName("credit_name")]
    public required string CreditName { get; set; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; set; }

    [JsonPropertyName("event_at")]
    public required DateTime EventAt { get; set; }

    [JsonPropertyName("event_id")]
    public required string EventId { get; set; }

    [JsonPropertyName("event_type")]
    public required CreditEventType EventType { get; set; }

    [JsonPropertyName("expiry_type")]
    public BillingCreditExpiryType? ExpiryType { get; set; }

    [JsonPropertyName("expiry_unit")]
    public BillingCreditExpiryUnit? ExpiryUnit { get; set; }

    [JsonPropertyName("expiry_unit_count")]
    public int? ExpiryUnitCount { get; set; }

    [JsonPropertyName("feature")]
    public FeatureLedgerResponseData? Feature { get; set; }

    [JsonPropertyName("feature_id")]
    public string? FeatureId { get; set; }

    [JsonPropertyName("from_grant_id")]
    public string? FromGrantId { get; set; }

    [JsonPropertyName("grant_expires_at")]
    public DateTime? GrantExpiresAt { get; set; }

    [JsonPropertyName("grant_id")]
    public string? GrantId { get; set; }

    [JsonPropertyName("grant_quantity")]
    public int? GrantQuantity { get; set; }

    [JsonPropertyName("grant_quantity_remaining")]
    public double? GrantQuantityRemaining { get; set; }

    [JsonPropertyName("grant_reason")]
    public BillingCreditGrantReason? GrantReason { get; set; }

    [JsonPropertyName("grant_valid_from")]
    public DateTime? GrantValidFrom { get; set; }

    [JsonPropertyName("plan_id")]
    public string? PlanId { get; set; }

    [JsonPropertyName("quantity_consumed")]
    public double? QuantityConsumed { get; set; }

    [JsonPropertyName("quantity_remaining_at_zero_out")]
    public double? QuantityRemainingAtZeroOut { get; set; }

    [JsonPropertyName("source_id")]
    public required int SourceId { get; set; }

    [JsonPropertyName("to_grant_id")]
    public string? ToGrantId { get; set; }

    [JsonPropertyName("usage_event_id")]
    public string? UsageEventId { get; set; }

    [JsonPropertyName("zeroed_out_reason")]
    public BillingCreditGrantZeroedOutReason? ZeroedOutReason { get; set; }

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
