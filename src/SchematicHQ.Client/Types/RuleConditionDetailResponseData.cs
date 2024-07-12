using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record RuleConditionDetailResponseData
{
    [JsonPropertyName("comparison_trait")]
    public EntityTraitDefinitionResponseData? ComparisonTrait { get; init; }

    [JsonPropertyName("comparison_trait_id")]
    public string? ComparisonTraitId { get; init; }

    [JsonPropertyName("condition_group_id")]
    public string? ConditionGroupId { get; init; }

    [JsonPropertyName("condition_type")]
    public required string ConditionType { get; init; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; init; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; init; }

    [JsonPropertyName("event_subtype")]
    public string? EventSubtype { get; init; }

    [JsonPropertyName("flag_id")]
    public string? FlagId { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("metric_period")]
    public string? MetricPeriod { get; init; }

    [JsonPropertyName("metric_value")]
    public int? MetricValue { get; init; }

    [JsonPropertyName("operator")]
    public required string Operator { get; init; }

    [JsonPropertyName("plan_id")]
    public string? PlanId { get; init; }

    [JsonPropertyName("resource_ids")]
    public IEnumerable<string> ResourceIds { get; init; } = new List<string>();

    [JsonPropertyName("resources")]
    public IEnumerable<RuleConditionResourceResponseData> Resources { get; init; } =
        new List<RuleConditionResourceResponseData>();

    [JsonPropertyName("rule_id")]
    public required string RuleId { get; init; }

    [JsonPropertyName("trait")]
    public EntityTraitDefinitionResponseData? Trait { get; init; }

    [JsonPropertyName("trait_entity_type")]
    public string? TraitEntityType { get; init; }

    [JsonPropertyName("trait_id")]
    public string? TraitId { get; init; }

    [JsonPropertyName("trait_value")]
    public required string TraitValue { get; init; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; init; }
}
