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

    [JsonPropertyName("plan_id")]
    public required string PlanId { get; init; }

    [JsonPropertyName("value_bool")]
    public bool? ValueBool { get; init; }

    [JsonPropertyName("value_numeric")]
    public int? ValueNumeric { get; init; }

    [JsonPropertyName("value_trait_id")]
    public string? ValueTraitId { get; init; }

    [JsonPropertyName("value_type")]
    public required CreatePlanEntitlementRequestBodyValueType ValueType { get; init; }
}
