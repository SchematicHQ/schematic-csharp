using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CreateReqCommon
{
    [JsonPropertyName("feature_id")]
    public required string FeatureId { get; set; }

    [JsonPropertyName("metric_period")]
    public CreateReqCommonMetricPeriod? MetricPeriod { get; set; }

    [JsonPropertyName("value_bool")]
    public bool? ValueBool { get; set; }

    [JsonPropertyName("value_numeric")]
    public int? ValueNumeric { get; set; }

    [JsonPropertyName("value_trait_id")]
    public string? ValueTraitId { get; set; }

    [JsonPropertyName("value_type")]
    public required CreateReqCommonValueType ValueType { get; set; }
}
