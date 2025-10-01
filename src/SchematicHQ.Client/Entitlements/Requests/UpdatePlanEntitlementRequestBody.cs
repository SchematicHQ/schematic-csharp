using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdatePlanEntitlementRequestBody
{
    [JsonPropertyName("billing_product_id")]
    public string? BillingProductId { get; set; }

    [JsonPropertyName("billing_threshold")]
    public int? BillingThreshold { get; set; }

    [JsonPropertyName("credit_consumption_rate")]
    public double? CreditConsumptionRate { get; set; }

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("metric_period")]
    public UpdatePlanEntitlementRequestBodyMetricPeriod? MetricPeriod { get; set; }

    [JsonPropertyName("metric_period_month_reset")]
    public UpdatePlanEntitlementRequestBodyMetricPeriodMonthReset? MetricPeriodMonthReset { get; set; }

    [JsonPropertyName("monthly_metered_price_id")]
    public string? MonthlyMeteredPriceId { get; set; }

    [JsonPropertyName("monthly_price_tiers")]
    public IEnumerable<CreatePriceTierRequestBody>? MonthlyPriceTiers { get; set; }

    [JsonPropertyName("monthly_unit_price")]
    public int? MonthlyUnitPrice { get; set; }

    [JsonPropertyName("monthly_unit_price_decimal")]
    public string? MonthlyUnitPriceDecimal { get; set; }

    [JsonPropertyName("overage_billing_product_id")]
    public string? OverageBillingProductId { get; set; }

    [JsonPropertyName("price_behavior")]
    public UpdatePlanEntitlementRequestBodyPriceBehavior? PriceBehavior { get; set; }

    /// <summary>
    /// Use MonthlyPriceTiers or YearlyPriceTiers instead
    /// </summary>
    [JsonPropertyName("price_tiers")]
    public IEnumerable<CreatePriceTierRequestBody>? PriceTiers { get; set; }

    [JsonPropertyName("soft_limit")]
    public int? SoftLimit { get; set; }

    [JsonPropertyName("tier_mode")]
    public string? TierMode { get; set; }

    [JsonPropertyName("value_bool")]
    public bool? ValueBool { get; set; }

    [JsonPropertyName("value_credit_id")]
    public string? ValueCreditId { get; set; }

    [JsonPropertyName("value_numeric")]
    public int? ValueNumeric { get; set; }

    [JsonPropertyName("value_trait_id")]
    public string? ValueTraitId { get; set; }

    [JsonPropertyName("value_type")]
    public required UpdatePlanEntitlementRequestBodyValueType ValueType { get; set; }

    [JsonPropertyName("yearly_metered_price_id")]
    public string? YearlyMeteredPriceId { get; set; }

    [JsonPropertyName("yearly_price_tiers")]
    public IEnumerable<CreatePriceTierRequestBody>? YearlyPriceTiers { get; set; }

    [JsonPropertyName("yearly_unit_price")]
    public int? YearlyUnitPrice { get; set; }

    [JsonPropertyName("yearly_unit_price_decimal")]
    public string? YearlyUnitPriceDecimal { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
