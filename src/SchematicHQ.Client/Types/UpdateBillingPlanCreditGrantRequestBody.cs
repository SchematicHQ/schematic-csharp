using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdateBillingPlanCreditGrantRequestBody : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("apply_to_existing")]
    public bool? ApplyToExisting { get; set; }

    [JsonPropertyName("auto_topup_amount")]
    public long? AutoTopupAmount { get; set; }

    [JsonPropertyName("auto_topup_amount_type")]
    public string? AutoTopupAmountType { get; set; }

    [JsonPropertyName("auto_topup_availability")]
    public BillingCreditAutoTopupAvailability? AutoTopupAvailability { get; set; }

    [JsonPropertyName("auto_topup_enabled")]
    public bool? AutoTopupEnabled { get; set; }

    [JsonPropertyName("auto_topup_expiry_type")]
    public BillingCreditExpiryType? AutoTopupExpiryType { get; set; }

    [JsonPropertyName("auto_topup_expiry_unit")]
    public BillingCreditExpiryUnit? AutoTopupExpiryUnit { get; set; }

    [JsonPropertyName("auto_topup_expiry_unit_count")]
    public long? AutoTopupExpiryUnitCount { get; set; }

    [JsonPropertyName("auto_topup_self_service")]
    public bool? AutoTopupSelfService { get; set; }

    [JsonPropertyName("auto_topup_threshold_credits")]
    public long? AutoTopupThresholdCredits { get; set; }

    [JsonPropertyName("auto_topup_threshold_percent")]
    public long? AutoTopupThresholdPercent { get; set; }

    [JsonPropertyName("can_buy_bundles")]
    public bool? CanBuyBundles { get; set; }

    [JsonPropertyName("credit_amount")]
    public long? CreditAmount { get; set; }

    [JsonPropertyName("expiry_type")]
    public BillingCreditExpiryType? ExpiryType { get; set; }

    [JsonPropertyName("expiry_unit")]
    public BillingCreditExpiryUnit? ExpiryUnit { get; set; }

    [JsonPropertyName("expiry_unit_count")]
    public long? ExpiryUnitCount { get; set; }

    [JsonPropertyName("reset_cadence")]
    public required BillingPlanCreditGrantResetCadence ResetCadence { get; set; }

    [JsonPropertyName("reset_start")]
    public required BillingPlanCreditGrantResetStart ResetStart { get; set; }

    [JsonPropertyName("reset_type")]
    public BillingPlanCreditGrantResetType? ResetType { get; set; }

    /// <summary>
    /// Percentage of unused credits that carry over when this grant resets. Only applies when reset_type is plan_period. Rolled-over credits expire at the next reset and are not rolled again.
    /// </summary>
    [JsonPropertyName("rollover_percentage")]
    public long? RolloverPercentage { get; set; }

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
