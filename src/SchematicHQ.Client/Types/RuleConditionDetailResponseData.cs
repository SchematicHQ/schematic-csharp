using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record RuleConditionDetailResponseData
{
    [JsonPropertyName("comparison_trait")]
    public EntityTraitDefinitionResponseData? ComparisonTrait { get; set; }

    [JsonPropertyName("comparison_trait_id")]
    public string? ComparisonTraitId { get; set; }

    [JsonPropertyName("condition_group_id")]
    public string? ConditionGroupId { get; set; }

    [JsonPropertyName("condition_type")]
    public required string ConditionType { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; set; }

    [JsonPropertyName("event_subtype")]
    public string? EventSubtype { get; set; }

    [JsonPropertyName("flag_id")]
    public string? FlagId { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("metric_period")]
    public string? MetricPeriod { get; set; }

    [JsonPropertyName("metric_value")]
    public int? MetricValue { get; set; }

    [JsonPropertyName("operator")]
    public required string Operator { get; set; }

    [JsonPropertyName("plan_id")]
    public string? PlanId { get; set; }

    [JsonPropertyName("resource_ids")]
    public IEnumerable<string> ResourceIds { get; set; } = new List<string>();

    [JsonPropertyName("resources")]
    public IEnumerable<RuleConditionResourceResponseData> Resources { get; set; } =
        new List<RuleConditionResourceResponseData>();

    [JsonPropertyName("rule_id")]
    public required string RuleId { get; set; }

    [JsonPropertyName("trait")]
    public EntityTraitDefinitionResponseData? Trait { get; set; }

    [JsonPropertyName("trait_entity_type")]
    public string? TraitEntityType { get; set; }

    [JsonPropertyName("trait_id")]
    public string? TraitId { get; set; }

    [JsonPropertyName("trait_value")]
    public required string TraitValue { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }
}
