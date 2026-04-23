using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PlanDetailResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("active_version")]
    public PlanVersionResponseData? ActiveVersion { get; set; }

    [JsonPropertyName("audience_type")]
    public string? AudienceType { get; set; }

    [JsonPropertyName("billing_linked_resource")]
    public BillingLinkedResourceResponseData? BillingLinkedResource { get; set; }

    [JsonPropertyName("billing_product")]
    public BillingProductDetailResponseData? BillingProduct { get; set; }

    [JsonPropertyName("charge_type")]
    public required ChargeType ChargeType { get; set; }

    [JsonPropertyName("company_count")]
    public required long CompanyCount { get; set; }

    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    [JsonPropertyName("company_name")]
    public string? CompanyName { get; set; }

    [JsonPropertyName("controlled_by")]
    public required BillingProviderType ControlledBy { get; set; }

    [JsonPropertyName("copied_from_plan_id")]
    public string? CopiedFromPlanId { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("currency_prices")]
    public IEnumerable<PlanCurrencyPricesResponseData> CurrencyPrices { get; set; } =
        new List<PlanCurrencyPricesResponseData>();

    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("draft_version")]
    public PlanVersionResponseData? DraftVersion { get; set; }

    [JsonPropertyName("features")]
    public IEnumerable<FeatureInPlanResponseData> Features { get; set; } =
        new List<FeatureInPlanResponseData>();

    [JsonPropertyName("icon")]
    public required PlanIcon Icon { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("included_credit_grants")]
    public IEnumerable<BillingPlanCreditGrantResponseData>? IncludedCreditGrants { get; set; }

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
    public long? TrialDays { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }

    [JsonPropertyName("versions")]
    public IEnumerable<PlanVersionResponseData> Versions { get; set; } =
        new List<PlanVersionResponseData>();

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
