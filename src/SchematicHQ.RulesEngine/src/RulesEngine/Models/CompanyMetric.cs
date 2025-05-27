using System.Text.Json.Serialization;

namespace RulesEngine.Models
{
  public class CompanyMetric
  {
    [JsonPropertyName("account_id")]
    public string? AccountId { get; set; }

    [JsonPropertyName("environment_id")]
    public string? EnvironmentId { get; set; }

    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    [JsonPropertyName("event_subtype")]
    public required string EventSubtype { get; set; }

    [JsonPropertyName("period")]
    public MetricPeriod Period { get; set; }

    [JsonPropertyName("month_reset")]
    public MetricPeriodMonthReset MonthReset { get; set; }

    [JsonPropertyName("value")]
    public long Value { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("valid_until")]
    public DateTime? ValidUntil { get; set; }

    public static CompanyMetric? Find(List<CompanyMetric> metrics, string eventSubtype, MetricPeriod? period, MetricPeriodMonthReset? monthReset)
    {
      if (metrics == null || string.IsNullOrEmpty(eventSubtype) || period == null)
        return null;

      return metrics.Find(m =>
          m.EventSubtype == eventSubtype &&
          m.Period == period.Value &&
          (monthReset == null || m.MonthReset == monthReset));
    }
  }
}
