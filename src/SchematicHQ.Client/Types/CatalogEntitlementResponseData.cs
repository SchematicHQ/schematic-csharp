using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CatalogEntitlementResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Credits consumed per unit of usage, for credit-burndown entitlements.
    /// </summary>
    [JsonPropertyName("consumption_rate")]
    public double? ConsumptionRate { get; set; }

    /// <summary>
    /// How many units of usage the plan's fixed included credit grants cover (⌊granted credits ÷ consumption rate⌋). Null when there is no fixed grant or the grant scales per license.
    /// </summary>
    [JsonPropertyName("credit_equivalent_limit")]
    public long? CreditEquivalentLimit { get; set; }

    [JsonPropertyName("credit_icon")]
    public string? CreditIcon { get; set; }

    [JsonPropertyName("credit_id")]
    public string? CreditId { get; set; }

    [JsonPropertyName("credit_name")]
    public string? CreditName { get; set; }

    [JsonPropertyName("credit_plural_name")]
    public string? CreditPluralName { get; set; }

    [JsonPropertyName("credit_singular_name")]
    public string? CreditSingularName { get; set; }

    [JsonPropertyName("currency_prices")]
    public IEnumerable<CatalogCurrencyPricesResponseData> CurrencyPrices { get; set; } =
        new List<CatalogCurrencyPricesResponseData>();

    [JsonPropertyName("feature_description")]
    public required string FeatureDescription { get; set; }

    [JsonPropertyName("feature_icon")]
    public required string FeatureIcon { get; set; }

    [JsonPropertyName("feature_id")]
    public required string FeatureId { get; set; }

    [JsonPropertyName("feature_name")]
    public required string FeatureName { get; set; }

    [JsonPropertyName("feature_plural_name")]
    public string? FeaturePluralName { get; set; }

    [JsonPropertyName("feature_singular_name")]
    public string? FeatureSingularName { get; set; }

    [JsonPropertyName("feature_type")]
    public required FeatureType FeatureType { get; set; }

    [JsonPropertyName("license_id")]
    public string? LicenseId { get; set; }

    [JsonPropertyName("metered_monthly_price")]
    public CatalogPriceResponseData? MeteredMonthlyPrice { get; set; }

    [JsonPropertyName("metered_quarterly_price")]
    public CatalogPriceResponseData? MeteredQuarterlyPrice { get; set; }

    [JsonPropertyName("metered_yearly_price")]
    public CatalogPriceResponseData? MeteredYearlyPrice { get; set; }

    [JsonPropertyName("metric_period")]
    public MetricPeriod? MetricPeriod { get; set; }

    [JsonPropertyName("metric_period_month_reset")]
    public MetricPeriodMonthReset? MetricPeriodMonthReset { get; set; }

    [JsonPropertyName("price_behavior")]
    public EntitlementPriceBehavior? PriceBehavior { get; set; }

    [JsonPropertyName("soft_limit")]
    public long? SoftLimit { get; set; }

    [JsonPropertyName("value_bool")]
    public bool? ValueBool { get; set; }

    [JsonPropertyName("value_numeric")]
    public long? ValueNumeric { get; set; }

    [JsonPropertyName("value_type")]
    public required EntitlementValueType ValueType { get; set; }

    /// <summary>
    /// The default warning-tier value, when configured.
    /// </summary>
    [JsonPropertyName("warning_threshold")]
    public long? WarningThreshold { get; set; }

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
