using System.Text.Json.Serialization;

namespace Schematic.RulesEngine.Models
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
    public Schematic.RulesEngine.RuleType RuleType { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("priority")]
    public long Priority { get; set; }

    [JsonPropertyName("conditions")]
    public List<global::Schematic.RulesEngine.Models.Condition>? Conditions { get; set; } = new List<global::Schematic.RulesEngine.Models.Condition>();

    [JsonPropertyName("condition_groups")]
    public List<global::Schematic.RulesEngine.Models.ConditionGroup>? ConditionGroups { get; set; } = new List<global::Schematic.RulesEngine.Models.ConditionGroup>();

    [JsonPropertyName("value")]
    public bool Value { get; set; }

    [JsonPropertyName("flag_id")]
    public string? FlagID { get; set; }
  }
}