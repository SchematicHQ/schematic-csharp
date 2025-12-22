using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PlanCreditGrantView : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("billing_credit_auto_topup_amount")]
    public int? BillingCreditAutoTopupAmount { get; set; }

    [JsonPropertyName("billing_credit_auto_topup_amount_type")]
    public string? BillingCreditAutoTopupAmountType { get; set; }

    [JsonPropertyName("billing_credit_auto_topup_enabled")]
    public required bool BillingCreditAutoTopupEnabled { get; set; }

    [JsonPropertyName("billing_credit_auto_topup_expiry_type")]
    public BillingCreditExpiryType? BillingCreditAutoTopupExpiryType { get; set; }

    [JsonPropertyName("billing_credit_auto_topup_expiry_unit")]
    public BillingCreditExpiryUnit? BillingCreditAutoTopupExpiryUnit { get; set; }

    [JsonPropertyName("billing_credit_auto_topup_expiry_unit_count")]
    public int? BillingCreditAutoTopupExpiryUnitCount { get; set; }

    [JsonPropertyName("billing_credit_auto_topup_threshold_percent")]
    public int? BillingCreditAutoTopupThresholdPercent { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("credit_amount")]
    public required int CreditAmount { get; set; }

    [JsonPropertyName("credit_description")]
    public required string CreditDescription { get; set; }

    [JsonPropertyName("credit_icon")]
    public string? CreditIcon { get; set; }

    [JsonPropertyName("credit_id")]
    public required string CreditId { get; set; }

    [JsonPropertyName("credit_name")]
    public required string CreditName { get; set; }

    [JsonPropertyName("expiry_type")]
    public BillingCreditExpiryType? ExpiryType { get; set; }

    [JsonPropertyName("expiry_unit")]
    public BillingCreditExpiryUnit? ExpiryUnit { get; set; }

    [JsonPropertyName("expiry_unit_count")]
    public int? ExpiryUnitCount { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("plan_id")]
    public required string PlanId { get; set; }

    [JsonPropertyName("plan_name")]
    public required string PlanName { get; set; }

    [JsonPropertyName("plural_name")]
    public string? PluralName { get; set; }

    [JsonPropertyName("reset_cadence")]
    public required BillingPlanCreditGrantResetCadence ResetCadence { get; set; }

    [JsonPropertyName("reset_start")]
    public required BillingPlanCreditGrantResetStart ResetStart { get; set; }

    [JsonPropertyName("reset_type")]
    public required BillingPlanCreditGrantResetType ResetType { get; set; }

    [JsonPropertyName("singular_name")]
    public string? SingularName { get; set; }

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
