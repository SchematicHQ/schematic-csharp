using System.Text.Json.Serialization;

namespace RulesEngine.Models
{
  public class ConditionGroup
  {
    [JsonPropertyName("conditions")]
    public List<Condition> Conditions { get; set; } = new List<Condition>();
  }
}