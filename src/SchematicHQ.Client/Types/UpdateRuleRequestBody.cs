using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record UpdateRuleRequestBody
{
    [JsonPropertyName("condition_groups")]
    public IEnumerable<CreateOrUpdateConditionGroupRequestBody> ConditionGroups { get; init; } =
        new List<CreateOrUpdateConditionGroupRequestBody>();

    [JsonPropertyName("conditions")]
    public IEnumerable<CreateOrUpdateConditionRequestBody> Conditions { get; init; } =
        new List<CreateOrUpdateConditionRequestBody>();

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("priority")]
    public required int Priority { get; init; }

    [JsonPropertyName("value")]
    public required bool Value { get; init; }
}
