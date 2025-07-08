using System.Text.Json.Serialization;

namespace SchematicHQ.Client.RulesEngine.Models
{
  public class Subscription
  {
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("period_start")]
    public DateTime PeriodStart { get; set; }

    [JsonPropertyName("period_end")]
    public DateTime PeriodEnd { get; set; }
  }
}