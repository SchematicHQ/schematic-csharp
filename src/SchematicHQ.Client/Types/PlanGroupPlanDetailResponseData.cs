using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PlanGroupPlanDetailResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("audience_type")]
    public string? AudienceType { get; set; }

    [JsonPropertyName("billing_product")]
    public BillingProductDetailResponseData? BillingProduct { get; set; }

    [JsonPropertyName("charge_type")]
    public required ChargeType ChargeType { get; set; }

    [JsonPropertyName("company_count")]
    public required int CompanyCount { get; set; }

    [JsonPropertyName("compatible_plan_ids")]
    public IEnumerable<string> CompatiblePlanIds { get; set; } = new List<string>();

    [JsonPropertyName("controlled_by")]
    public required PlanControlledByType ControlledBy { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("custom_plan_config")]
    public CustomPlanViewConfigResponseData? CustomPlanConfig { get; set; }

    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("entitlements")]
    public IEnumerable<PlanEntitlementResponseData> Entitlements { get; set; } =
        new List<PlanEntitlementResponseData>();

    [JsonPropertyName("features")]
    public IEnumerable<FeatureDetailResponseData> Features { get; set; } =
        new List<FeatureDetailResponseData>();

    [JsonPropertyName("icon")]
    public required string Icon { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("included_credit_grants")]
    public IEnumerable<BillingPlanCreditGrantResponseData>? IncludedCreditGrants { get; set; }

    [JsonPropertyName("is_custom")]
    public required bool IsCustom { get; set; }

    [JsonPropertyName("is_default")]
    public required bool IsDefault { get; set; }

    [JsonPropertyName("is_free")]
    public required bool IsFree { get; set; }

    [JsonPropertyName("is_trialable")]
    public required bool IsTrialable { get; set; }

    [JsonPropertyName("monthly_price")]
    public BillingPriceResponseData? MonthlyPrice { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("one_time_price")]
    public BillingPriceResponseData? OneTimePrice { get; set; }

    [JsonPropertyName("plan_type")]
    public required PlanType PlanType { get; set; }

    [JsonPropertyName("trial_days")]
    public int? TrialDays { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }

    [JsonPropertyName("yearly_price")]
    public BillingPriceResponseData? YearlyPrice { get; set; }

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
