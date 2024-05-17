using System.Text.Json.Serialization;
using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class UpdateRuleRequestBody
{
    [JsonPropertyName("condition_groups")]
    public List<CreateOrUpdateConditionGroupRequestBody> ConditionGroups { get; init; }

    [JsonPropertyName("conditions")]
    public List<CreateOrUpdateConditionRequestBody> Conditions { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("priority")]
    public int Priority { get; init; }

    [JsonPropertyName("value")]
    public bool Value { get; init; }
}
