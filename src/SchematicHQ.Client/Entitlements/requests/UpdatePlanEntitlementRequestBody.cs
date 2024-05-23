using System.Text.Json.Serialization;
using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class UpdatePlanEntitlementRequestBody
{
    [JsonPropertyName("metric_period")]
    public UpdatePlanEntitlementRequestBodyMetricPeriod? MetricPeriod { get; init; }

    [JsonPropertyName("value_bool")]
    public bool? ValueBool { get; init; }

    [JsonPropertyName("value_numeric")]
    public int? ValueNumeric { get; init; }

    [JsonPropertyName("value_trait_id")]
    public string? ValueTraitId { get; init; }

    [JsonPropertyName("value_type")]
    public UpdatePlanEntitlementRequestBodyValueType ValueType { get; init; }
}
