using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class UpdateReqCommon
{
    [JsonPropertyName("metric_period")]
    public UpdateReqCommonMetricPeriod? MetricPeriod { get; init; }

    [JsonPropertyName("value_bool")]
    public bool? ValueBool { get; init; }

    [JsonPropertyName("value_numeric")]
    public int? ValueNumeric { get; init; }

    [JsonPropertyName("value_trait_id")]
    public string? ValueTraitId { get; init; }

    [JsonPropertyName("value_type")]
    public UpdateReqCommonValueType ValueType { get; init; }
}
