using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record UpdateReqCommon
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
    public required UpdateReqCommonValueType ValueType { get; init; }
}
