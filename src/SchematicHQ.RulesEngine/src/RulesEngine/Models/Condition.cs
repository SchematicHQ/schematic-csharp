using SchematicHQ.RulesEngine.Utils;
using System.Text.Json.Serialization;

namespace SchematicHQ.RulesEngine.Models
{
  public class Condition
  {
    [JsonPropertyName("id")]
    public required string Id { get; set; } = Guid.NewGuid().ToString();
    
    [JsonPropertyName("account_id")]
    public string? AccountId { get; set; }
    
    [JsonPropertyName("environment_id")]
    public string? EnvironmentId { get; set; }
    
    [JsonPropertyName("condition_type")]
    public ConditionType ConditionType { get; set; }
    
    [JsonPropertyName("operator")]
    public ComparableOperator Operator { get; set; }
    
    // Fields for Company, User, Plan, Base Plan, Billing Product, or CRM Product conditions
    [JsonPropertyName("resource_ids")]
    public List<string> ResourceIds { get; set; } = new List<string>();
    
    // Fields for Event/Metric conditions
    [JsonPropertyName("event_subtype")]
    public string? EventSubtype { get; set; }
    
    [JsonPropertyName("metric_value")]
    public long? MetricValue { get; set; }
    
    [JsonPropertyName("metric_period")]
    public MetricPeriod? MetricPeriod { get; set; }
    
    [JsonPropertyName("metric_period_month_reset")]
    public int? MetricPeriodMonthReset { get; set; }
    
    // Fields for Trait conditions
    [JsonPropertyName("trait_definition")]
    public TraitDefinition? TraitDefinition { get; set; }
    
    [JsonPropertyName("trait_value")]
    public string? TraitValue { get; set; }
    
    // Comparison trait for Event or Trait conditions
    [JsonPropertyName("comparison_trait_definition")]
    public TraitDefinition? ComparisonTraitDefinition { get; set; }
  }
}