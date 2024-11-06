using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record RuleDetailResponseData
{
    [JsonPropertyName("condition_groups")]
    public IEnumerable<RuleConditionGroupDetailResponseData> ConditionGroups { get; set; } =
        new List<RuleConditionGroupDetailResponseData>();

    [JsonPropertyName("conditions")]
    public IEnumerable<RuleConditionDetailResponseData> Conditions { get; set; } =
        new List<RuleConditionDetailResponseData>();

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; set; }

    [JsonPropertyName("flag_id")]
    public string? FlagId { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("plan_id")]
    public string? PlanId { get; set; }

    [JsonPropertyName("priority")]
    public required int Priority { get; set; }

    [JsonPropertyName("rule_type")]
    public required string RuleType { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }

    [JsonPropertyName("value")]
    public required bool Value { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
