using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record CreatePlanEntitlementRequestBody
{
    [JsonPropertyName("feature_id")]
    public required string FeatureId { get; init; }

    [JsonPropertyName("metric_period")]
    public CreatePlanEntitlementRequestBodyMetricPeriod? MetricPeriod { get; init; }

    [JsonPropertyName("metric_period_month_reset")]
    public CreatePlanEntitlementRequestBodyMetricPeriodMonthReset? MetricPeriodMonthReset { get; init; }

    [JsonPropertyName("monthly_metered_price_id")]
    public string? MonthlyMeteredPriceId { get; init; }

    [JsonPropertyName("plan_id")]
    public required string PlanId { get; init; }

    [JsonPropertyName("price_behavior")]
    public string? PriceBehavior { get; init; }

    [JsonPropertyName("value_bool")]
    public bool? ValueBool { get; init; }

    [JsonPropertyName("value_numeric")]
    public int? ValueNumeric { get; init; }

    [JsonPropertyName("value_trait_id")]
    public string? ValueTraitId { get; init; }

    [JsonPropertyName("value_type")]
    public required CreatePlanEntitlementRequestBodyValueType ValueType { get; init; }

    [JsonPropertyName("yearly_metered_price_id")]
    public string? YearlyMeteredPriceId { get; init; }
}
