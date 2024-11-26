using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record CreateEntitlementReqCommon
{
    [JsonPropertyName("feature_id")]
    public required string FeatureId { get; set; }

    [JsonPropertyName("metered_monthly_price_id")]
    public string? MeteredMonthlyPriceId { get; set; }

    [JsonPropertyName("metered_yearly_price_id")]
    public string? MeteredYearlyPriceId { get; set; }

    [JsonPropertyName("metric_period")]
    public CreateEntitlementReqCommonMetricPeriod? MetricPeriod { get; set; }

    [JsonPropertyName("metric_period_month_reset")]
    public CreateEntitlementReqCommonMetricPeriodMonthReset? MetricPeriodMonthReset { get; set; }

    [JsonPropertyName("value_bool")]
    public bool? ValueBool { get; set; }

    [JsonPropertyName("value_numeric")]
    public int? ValueNumeric { get; set; }

    [JsonPropertyName("value_trait_id")]
    public string? ValueTraitId { get; set; }

    [JsonPropertyName("value_type")]
    public required CreateEntitlementReqCommonValueType ValueType { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
