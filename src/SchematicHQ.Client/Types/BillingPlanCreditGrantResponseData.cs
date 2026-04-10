using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record BillingPlanCreditGrantResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("auto_topup_amount")]
    public long? AutoTopupAmount { get; set; }

    [JsonPropertyName("auto_topup_amount_type")]
    public string? AutoTopupAmountType { get; set; }

    [JsonPropertyName("auto_topup_enabled")]
    public required bool AutoTopupEnabled { get; set; }

    [JsonPropertyName("auto_topup_expiry_type")]
    public BillingCreditExpiryType? AutoTopupExpiryType { get; set; }

    [JsonPropertyName("auto_topup_expiry_unit")]
    public BillingCreditExpiryUnit? AutoTopupExpiryUnit { get; set; }

    [JsonPropertyName("auto_topup_expiry_unit_count")]
    public long? AutoTopupExpiryUnitCount { get; set; }

    [JsonPropertyName("auto_topup_threshold_credits")]
    public long? AutoTopupThresholdCredits { get; set; }

    [JsonPropertyName("auto_topup_threshold_percent")]
    public long? AutoTopupThresholdPercent { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("credit")]
    public BillingCreditResponseData? Credit { get; set; }

    [JsonPropertyName("credit_amount")]
    public required long CreditAmount { get; set; }

    [JsonPropertyName("credit_id")]
    public required string CreditId { get; set; }

    /// <summary>
    /// Use credit.name from the nested credit object instead
    /// </summary>
    [JsonPropertyName("credit_name")]
    public required string CreditName { get; set; }

    /// <summary>
    /// Use plural_name from the nested credit object instead
    /// </summary>
    [JsonPropertyName("credit_plural_name")]
    public string? CreditPluralName { get; set; }

    /// <summary>
    /// Use singular_name from the nested credit object instead
    /// </summary>
    [JsonPropertyName("credit_singular_name")]
    public string? CreditSingularName { get; set; }

    [JsonPropertyName("expiry_type")]
    public BillingCreditExpiryType? ExpiryType { get; set; }

    [JsonPropertyName("expiry_unit")]
    public BillingCreditExpiryUnit? ExpiryUnit { get; set; }

    [JsonPropertyName("expiry_unit_count")]
    public long? ExpiryUnitCount { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("plan")]
    public PreviewObjectResponseData? Plan { get; set; }

    [JsonPropertyName("plan_id")]
    public required string PlanId { get; set; }

    /// <summary>
    /// Use plan.name from the nested plan object instead
    /// </summary>
    [JsonPropertyName("plan_name")]
    public required string PlanName { get; set; }

    [JsonPropertyName("plan_version_id")]
    public string? PlanVersionId { get; set; }

    [JsonPropertyName("reset_cadence")]
    public BillingPlanCreditGrantResetCadence? ResetCadence { get; set; }

    [JsonPropertyName("reset_start")]
    public BillingPlanCreditGrantResetStart? ResetStart { get; set; }

    [JsonPropertyName("reset_type")]
    public BillingPlanCreditGrantResetType? ResetType { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }

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
