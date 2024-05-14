using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class CreateOrUpdateRuleRequestBody
{
    [JsonPropertyName("condition_groups")]
    public List<CreateOrUpdateConditionGroupRequestBody> ConditionGroups { get; init; }

    [JsonPropertyName("conditions")]
    public List<CreateOrUpdateConditionRequestBody> Conditions { get; init; }

    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("priority")]
    public int Priority { get; init; }

    [JsonPropertyName("rule_type")]
    public CreateOrUpdateRuleRequestBodyRuleType? RuleType { get; init; }

    [JsonPropertyName("value")]
    public bool Value { get; init; }
}
