using System.Text.Json;
using System.Text.Json.Serialization;
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
    public int? AutoTopupAmount { get; set; }

    [JsonPropertyName("auto_topup_amount_type")]
    public string? AutoTopupAmountType { get; set; }

    [JsonPropertyName("auto_topup_enabled")]
    public bool? AutoTopupEnabled { get; set; }

    [JsonPropertyName("auto_topup_expiry_type")]
    public BillingCreditExpiryType? AutoTopupExpiryType { get; set; }

    [JsonPropertyName("auto_topup_expiry_unit")]
    public BillingCreditExpiryUnit? AutoTopupExpiryUnit { get; set; }

    [JsonPropertyName("auto_topup_expiry_unit_count")]
    public int? AutoTopupExpiryUnitCount { get; set; }

    [JsonPropertyName("auto_topup_threshold_percent")]
    public int? AutoTopupThresholdPercent { get; set; }

    [JsonPropertyName("credit_amount")]
    public int? CreditAmount { get; set; }

    [JsonPropertyName("expiry_type")]
    public BillingCreditExpiryType? ExpiryType { get; set; }

    [JsonPropertyName("expiry_unit")]
    public BillingCreditExpiryUnit? ExpiryUnit { get; set; }

    [JsonPropertyName("expiry_unit_count")]
    public int? ExpiryUnitCount { get; set; }

    [JsonPropertyName("reset_cadence")]
    public required BillingPlanCreditGrantResetCadence ResetCadence { get; set; }

    [JsonPropertyName("reset_start")]
    public required BillingPlanCreditGrantResetStart ResetStart { get; set; }

    [JsonPropertyName("reset_type")]
    public BillingPlanCreditGrantResetType? ResetType { get; set; }

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
