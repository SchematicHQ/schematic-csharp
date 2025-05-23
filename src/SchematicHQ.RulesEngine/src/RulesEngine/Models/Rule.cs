using System.Text.Json.Serialization;

namespace RulesEngine.Models
{
  public class Rule
  {
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("account_id")]
    public required string AccountId { get; set; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; set; }

    [JsonPropertyName("rule_type")]
    public RuleType RuleType { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("priority")]
    public long Priority { get; set; }

    [JsonPropertyName("conditions")]
    public List<Condition> Conditions { get; set; } = new List<Condition>();

    [JsonPropertyName("condition_groups")]
    public List<ConditionGroup> ConditionGroups { get; set; } = new List<ConditionGroup>();

    [JsonPropertyName("value")]
    public bool Value { get; set; }
  }
}