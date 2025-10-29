using System.Text.Json.Serialization;

namespace Schematic.RulesEngine.Models
{
  public class ConditionGroup
  {
    [JsonPropertyName("conditions")]
    public List<global::Schematic.RulesEngine.Models.Condition> Conditions { get; set; } = new List<global::Schematic.RulesEngine.Models.Condition>();
  }
}