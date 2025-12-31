using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PlanEntitlementResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("billing_threshold")]
    public int? BillingThreshold { get; set; }

    [JsonPropertyName("consumption_rate")]
    public double? ConsumptionRate { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; set; }

    [JsonPropertyName("feature")]
    public FeatureResponseData? Feature { get; set; }

    [JsonPropertyName("feature_id")]
    public required string FeatureId { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("metered_monthly_price")]
    public BillingPriceView? MeteredMonthlyPrice { get; set; }

    [JsonPropertyName("metered_yearly_price")]
    public BillingPriceView? MeteredYearlyPrice { get; set; }

    [JsonPropertyName("metric_period")]
    public string? MetricPeriod { get; set; }

    [JsonPropertyName("metric_period_month_reset")]
    public string? MetricPeriodMonthReset { get; set; }

    [JsonPropertyName("plan")]
    public PlanResponseData? Plan { get; set; }

    [JsonPropertyName("plan_id")]
    public required string PlanId { get; set; }

    [JsonPropertyName("price_behavior")]
    public EntitlementPriceBehavior? PriceBehavior { get; set; }

    [JsonPropertyName("rule_id")]
    public required string RuleId { get; set; }

    [JsonPropertyName("rule_id_usage_exceeded")]
    public string? RuleIdUsageExceeded { get; set; }

    [JsonPropertyName("soft_limit")]
    public int? SoftLimit { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }

    [JsonPropertyName("usage_based_product")]
    public BillingProductResponseData? UsageBasedProduct { get; set; }

    [JsonPropertyName("value_bool")]
    public bool? ValueBool { get; set; }

    [JsonPropertyName("value_credit")]
    public BillingCreditResponseData? ValueCredit { get; set; }

    [JsonPropertyName("value_numeric")]
    public int? ValueNumeric { get; set; }

    [JsonPropertyName("value_trait")]
    public EntityTraitDefinitionResponseData? ValueTrait { get; set; }

    [JsonPropertyName("value_trait_id")]
    public string? ValueTraitId { get; set; }

    [JsonPropertyName("value_type")]
    public required EntitlementValueType ValueType { get; set; }

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
