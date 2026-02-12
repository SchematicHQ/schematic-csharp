using System.Text.Json.Serialization;

namespace SchematicHQ.Client.RulesEngine.Models
{
    public class FeatureEntitlement
    {
        [JsonPropertyName("feature_id")]
        public required string FeatureID { get; set; }

        [JsonPropertyName("feature_key")]
        public required string FeatureKey { get; set; }

        [JsonPropertyName("value_type")]
        public required string ValueType { get; set; }

        [JsonPropertyName("allocation")]
        public long? Allocation { get; set; }

        [JsonPropertyName("usage")]
        public long? Usage { get; set; }

        [JsonPropertyName("event_name")]
        public string? EventName { get; set; }

        [JsonPropertyName("metric_period")]
        public ConditionMetricPeriod? MetricPeriod { get; set; }

        [JsonPropertyName("month_reset")]
        public ConditionMetricPeriodMonthReset? MonthReset { get; set; }

        [JsonPropertyName("metric_reset_at")]
        public DateTime? MetricResetAt { get; set; }

        [JsonPropertyName("credit_id")]
        public string? CreditID { get; set; }

        [JsonPropertyName("credit_total")]
        public double? CreditTotal { get; set; }

        [JsonPropertyName("credit_used")]
        public double? CreditUsed { get; set; }

        [JsonPropertyName("credit_remaining")]
        public double? CreditRemaining { get; set; }
    }
}
