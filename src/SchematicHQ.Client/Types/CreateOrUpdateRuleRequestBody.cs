using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public class CreateOrUpdateRuleRequestBody
{
    [JsonPropertyName("condition_groups")]
    public IEnumerable<CreateOrUpdateConditionGroupRequestBody> ConditionGroups { get; init; }

    [JsonPropertyName("conditions")]
    public IEnumerable<CreateOrUpdateConditionRequestBody> Conditions { get; init; }

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
