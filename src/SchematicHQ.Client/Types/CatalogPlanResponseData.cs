using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CatalogPlanResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("available_periods")]
    public IEnumerable<PlanPriceCadence> AvailablePeriods { get; set; } =
        new List<PlanPriceCadence>();

    [JsonPropertyName("billing_strategy")]
    public required BillingStrategy BillingStrategy { get; set; }

    [JsonPropertyName("charge_type")]
    public required ChargeType ChargeType { get; set; }

    /// <summary>
    /// For add-ons, the plans this add-on may be purchased with. Null means compatible with every plan.
    /// </summary>
    [JsonPropertyName("compatible_plan_ids")]
    public IEnumerable<string>? CompatiblePlanIds { get; set; }

    [JsonPropertyName("currency_prices")]
    public IEnumerable<CatalogCurrencyPricesResponseData> CurrencyPrices { get; set; } =
        new List<CatalogCurrencyPricesResponseData>();

    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("entitlements")]
    public IEnumerable<CatalogPlanEntitlementResponseData> Entitlements { get; set; } =
        new List<CatalogPlanEntitlementResponseData>();

    [JsonPropertyName("icon")]
    public required string Icon { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("included_credit_grants")]
    public IEnumerable<CatalogCreditGrantResponseData> IncludedCreditGrants { get; set; } =
        new List<CatalogCreditGrantResponseData>();

    [JsonPropertyName("is_trialable")]
    public required bool IsTrialable { get; set; }

    [JsonPropertyName("monthly_price")]
    public CatalogPriceResponseData? MonthlyPrice { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("one_time_price")]
    public CatalogPriceResponseData? OneTimePrice { get; set; }

    [JsonPropertyName("quarterly_price")]
    public CatalogPriceResponseData? QuarterlyPrice { get; set; }

    [JsonPropertyName("trial_days")]
    public long? TrialDays { get; set; }

    [JsonPropertyName("yearly_price")]
    public CatalogPriceResponseData? YearlyPrice { get; set; }

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
