using System.Text.Json.Serialization;
using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class RuleConditionDetailResponseData
{
    [JsonPropertyName("comparison_trait")]
    public EntityTraitDefinitionResponseData? ComparisonTrait { get; init; }

    [JsonPropertyName("comparison_trait_id")]
    public string? ComparisonTraitId { get; init; }

    [JsonPropertyName("condition_group_id")]
    public string? ConditionGroupId { get; init; }

    [JsonPropertyName("condition_type")]
    public string ConditionType { get; init; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("environment_id")]
    public string EnvironmentId { get; init; }

    [JsonPropertyName("event_subtype")]
    public string? EventSubtype { get; init; }

    [JsonPropertyName("flag_id")]
    public string? FlagId { get; init; }

    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("metric_period")]
    public string? MetricPeriod { get; init; }

    [JsonPropertyName("metric_value")]
    public int MetricValue { get; init; }

    [JsonPropertyName("operator")]
    public string Operator { get; init; }

    [JsonPropertyName("plan_id")]
    public string? PlanId { get; init; }

    [JsonPropertyName("resource_ids")]
    public List<string> ResourceIds { get; init; }

    [JsonPropertyName("resources")]
    public List<RuleConditionResourceResponseData> Resources { get; init; }

    [JsonPropertyName("rule_id")]
    public string RuleId { get; init; }

    [JsonPropertyName("trait")]
    public EntityTraitDefinitionResponseData? Trait { get; init; }

    [JsonPropertyName("trait_entity_type")]
    public string? TraitEntityType { get; init; }

    [JsonPropertyName("trait_id")]
    public string? TraitId { get; init; }

    [JsonPropertyName("trait_value")]
    public string TraitValue { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; init; }
}
