using System.Runtime.CompilerServices;

namespace SchematicHQ.Client.RulesEngine
{
    public static class RulesengineCompanyExtensions
    {
        private static readonly ConditionalWeakTable<RulesengineCompany, object> MetricsLocks = new ConditionalWeakTable<RulesengineCompany, object>();

        public static RulesengineTrait? GetTraitByDefinitionId(this RulesengineCompany company, string definitionId)
        {
            return company.Traits?.FirstOrDefault(t => t.TraitDefinition?.Id == definitionId);
        }

        public static void AddMetric(this RulesengineCompany company, RulesengineCompanyMetric? metric)
        {
            if (metric == null)
            {
                return;
            }

            var metricsList = company.Metrics as List<RulesengineCompanyMetric>;
            if (metricsList == null)
            {
                metricsList = company.Metrics != null
                    ? new List<RulesengineCompanyMetric>(company.Metrics)
                    : new List<RulesengineCompanyMetric>();
                company.Metrics = metricsList;
            }

            var metricsLock = MetricsLocks.GetOrCreateValue(company);
            lock (metricsLock)
            {
                var existingMetricIndex = metricsList.FindIndex(m =>
                    m.EventSubtype == metric.EventSubtype &&
                    m.Period == metric.Period &&
                    m.MonthReset == metric.MonthReset);

                if (existingMetricIndex != -1)
                {
                    metricsList[existingMetricIndex] = metric;
                }
                else
                {
                    metricsList.Add(metric);
                }
            }
        }
    }

    public static class CompanyMetricExtensions
    {
        public static RulesengineCompanyMetric? Find(
            IEnumerable<RulesengineCompanyMetric>? metrics,
            string eventSubtype,
            RulesengineConditionMetricPeriod? period,
            RulesengineConditionMetricPeriodMonthReset? monthReset)
        {
            if (metrics == null || string.IsNullOrEmpty(eventSubtype) || period == null)
                return null;

            return metrics.FirstOrDefault(m =>
                m.EventSubtype == eventSubtype &&
                m.Period.Value == period.Value.Value &&
                (monthReset == null || m.MonthReset.Value == monthReset.Value.Value));
        }
    }
}
