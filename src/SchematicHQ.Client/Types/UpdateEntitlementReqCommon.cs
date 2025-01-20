using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record UpdateEntitlementReqCommon
{
    [JsonPropertyName("metric_period")]
    public UpdateEntitlementReqCommonMetricPeriod? MetricPeriod { get; init; }

    [JsonPropertyName("metric_period_month_reset")]
    public UpdateEntitlementReqCommonMetricPeriodMonthReset? MetricPeriodMonthReset { get; init; }

    [JsonPropertyName("value_bool")]
    public bool? ValueBool { get; init; }

    [JsonPropertyName("value_numeric")]
    public int? ValueNumeric { get; init; }

    [JsonPropertyName("value_trait_id")]
    public string? ValueTraitId { get; init; }

    [JsonPropertyName("value_type")]
    public required UpdateEntitlementReqCommonValueType ValueType { get; init; }
}
