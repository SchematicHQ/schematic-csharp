using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record CreateOrUpdateRuleRequestBody
{
    [JsonPropertyName("condition_groups")]
    public IEnumerable<CreateOrUpdateConditionGroupRequestBody> ConditionGroups { get; init; } =
        new List<CreateOrUpdateConditionGroupRequestBody>();

    [JsonPropertyName("conditions")]
    public IEnumerable<CreateOrUpdateConditionRequestBody> Conditions { get; init; } =
        new List<CreateOrUpdateConditionRequestBody>();

    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("priority")]
    public required int Priority { get; init; }

    [JsonPropertyName("rule_type")]
    public CreateOrUpdateRuleRequestBodyRuleType? RuleType { get; init; }

    [JsonPropertyName("value")]
    public required bool Value { get; init; }
}
