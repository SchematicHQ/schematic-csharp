using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record PlanEntitlementResponseData
{
    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; init; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; init; }

    [JsonPropertyName("feature")]
    public FeatureResponseData? Feature { get; init; }

    [JsonPropertyName("feature_id")]
    public required string FeatureId { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("metric_period")]
    public string? MetricPeriod { get; init; }

    [JsonPropertyName("plan")]
    public PlanResponseData? Plan { get; init; }

    [JsonPropertyName("plan_id")]
    public required string PlanId { get; init; }

    [JsonPropertyName("rule_id")]
    public required string RuleId { get; init; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; init; }

    [JsonPropertyName("value_bool")]
    public bool? ValueBool { get; init; }

    [JsonPropertyName("value_numeric")]
    public int? ValueNumeric { get; init; }

    [JsonPropertyName("value_trait")]
    public EntityTraitDefinitionResponseData? ValueTrait { get; init; }

    [JsonPropertyName("value_trait_id")]
    public string? ValueTraitId { get; init; }

    [JsonPropertyName("value_type")]
    public required string ValueType { get; init; }
}
