using SchematicHQ.Client;

namespace SchematicHQ.Client.RulesEngine.Extensions
{
    /// <summary>
    /// Extension methods to add custom functionality to collections of RulesengineCompanyMetric
    /// without modifying the generated code.
    /// </summary>
    public static class RulesengineCompanyMetricExtensions
    {
        /// <summary>
        /// Extension method to find a metric in a collection based on event subtype, period, and month reset.
        /// Usage: metrics.Find(eventSubtype, conditionPeriod, conditionMonthReset)
        /// </summary>
        public static SchematicHQ.Client.RulesengineCompanyMetric? Find(
            this IEnumerable<SchematicHQ.Client.RulesengineCompanyMetric> metrics, 
            string eventSubtype, 
            RulesengineConditionMetricPeriod? conditionPeriod, 
            RulesengineConditionMetricPeriodMonthReset? conditionMonthReset = null)
        {
            if (metrics == null || string.IsNullOrEmpty(eventSubtype) || conditionPeriod == null)
                return null;

            return metrics.FirstOrDefault(m =>
                m.EventSubtype == eventSubtype &&
                m.Period.Value == conditionPeriod.Value.Value &&
                (conditionMonthReset == null || m.MonthReset.Value == conditionMonthReset.Value.Value));
        }
    }
}