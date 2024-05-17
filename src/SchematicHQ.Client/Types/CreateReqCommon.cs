using System.Text.Json.Serialization;
using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class CreateReqCommon
{
    [JsonPropertyName("feature_id")]
    public string FeatureId { get; init; }

    [JsonPropertyName("metric_period")]
    public CreateReqCommonMetricPeriod? MetricPeriod { get; init; }

    [JsonPropertyName("value_bool")]
    public bool? ValueBool { get; init; }

    [JsonPropertyName("value_numeric")]
    public int? ValueNumeric { get; init; }

    [JsonPropertyName("value_trait_id")]
    public string? ValueTraitId { get; init; }

    [JsonPropertyName("value_type")]
    public CreateReqCommonValueType ValueType { get; init; }
}
