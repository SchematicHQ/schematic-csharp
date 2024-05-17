using System.Text.Json.Serialization;
using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class PlanEntitlementResponseData
{
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("environment_id")]
    public string EnvironmentId { get; init; }

    [JsonPropertyName("feature")]
    public FeatureResponseData? Feature { get; init; }

    [JsonPropertyName("feature_id")]
    public string FeatureId { get; init; }

    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("metric_period")]
    public string? MetricPeriod { get; init; }

    [JsonPropertyName("plan")]
    public PlanResponseData? Plan { get; init; }

    [JsonPropertyName("plan_id")]
    public string PlanId { get; init; }

    [JsonPropertyName("rule_id")]
    public string RuleId { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; init; }

    [JsonPropertyName("value_bool")]
    public bool? ValueBool { get; init; }

    [JsonPropertyName("value_numeric")]
    public int? ValueNumeric { get; init; }

    [JsonPropertyName("value_trait")]
    public EntityTraitDefinitionResponseData? ValueTrait { get; init; }

    [JsonPropertyName("value_trait_id")]
    public string? ValueTraitId { get; init; }

    [JsonPropertyName("value_type")]
    public string ValueType { get; init; }
}
