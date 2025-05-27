using System.Diagnostics;
using System.Text.Json.Serialization;

namespace RulesEngine.Models
{
  public class Company
  {
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("account_id")]
    public required string AccountId { get; set; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; set; }

    [JsonPropertyName("plan_ids")]
    public List<string> PlanIds { get; set; } = new List<string>();

    [JsonPropertyName("base_plan_id")]
    public string? BasePlanId { get; set; }

    [JsonPropertyName("billing_product_ids")]
    public List<string> BillingProductIds { get; set; } = new List<string>();

    [JsonPropertyName("crm_product_ids")]
    public List<string> CrmProductIds { get; set; } = new List<string>();

    [JsonPropertyName("keys")]
    public IDictionary<string, string> Keys { get; set; } = new Dictionary<string, string>();

    [JsonPropertyName("traits")]
    public List<Trait> Traits { get; set; } = new List<Trait>();

    [JsonPropertyName("metrics")]
    public List<CompanyMetric> Metrics { get; set; } = new List<CompanyMetric>();

    [JsonPropertyName("subscription")]
    public Subscription? Subscription { get; set; }

    private Mutex mu = new Mutex();

    public Trait? GetTraitByDefinitionId(string definitionId)
    {
      return Traits.Find(t => t.TraitDefinition?.Id == definitionId);
    }

    public void AddMetric(CompanyMetric? metric)
    {
      if (metric == null)
      {
        return;
      }

      if (Metrics == null)
      {
        Metrics = new List<CompanyMetric>();
      }

      mu.WaitOne();

      var existingMetricIndex = Metrics.FindIndex(m =>
          m.EventSubtype == metric.EventSubtype &&
          m.Period == metric.Period &&
          m.MonthReset == metric.MonthReset);

      if (existingMetricIndex != -1)
      {
        Metrics[existingMetricIndex] = metric;
      }
      else
      {
        Metrics.Add(metric);
      }

      mu.ReleaseMutex();
    }
  }
}