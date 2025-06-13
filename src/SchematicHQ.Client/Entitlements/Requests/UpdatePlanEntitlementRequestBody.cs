using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record UpdatePlanEntitlementRequestBody
{
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("metric_period")]
    public UpdatePlanEntitlementRequestBodyMetricPeriod? MetricPeriod { get; set; }

    [JsonPropertyName("metric_period_month_reset")]
    public UpdatePlanEntitlementRequestBodyMetricPeriodMonthReset? MetricPeriodMonthReset { get; set; }

    [JsonPropertyName("monthly_metered_price_id")]
    public string? MonthlyMeteredPriceId { get; set; }

    [JsonPropertyName("monthly_unit_price")]
    public int? MonthlyUnitPrice { get; set; }

    [JsonPropertyName("monthly_unit_price_decimal")]
    public string? MonthlyUnitPriceDecimal { get; set; }

    [JsonPropertyName("overage_billing_product_id")]
    public string? OverageBillingProductId { get; set; }

    [JsonPropertyName("price_behavior")]
    public string? PriceBehavior { get; set; }

    [JsonPropertyName("soft_limit")]
    public int? SoftLimit { get; set; }

    [JsonPropertyName("value_bool")]
    public bool? ValueBool { get; set; }

    [JsonPropertyName("value_numeric")]
    public int? ValueNumeric { get; set; }

    [JsonPropertyName("value_trait_id")]
    public string? ValueTraitId { get; set; }

    [JsonPropertyName("value_type")]
    public required UpdatePlanEntitlementRequestBodyValueType ValueType { get; set; }

    [JsonPropertyName("yearly_metered_price_id")]
    public string? YearlyMeteredPriceId { get; set; }

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
