using System.Collections.Concurrent;
using SchematicHQ.Client;

namespace SchematicHQ.Client.RulesEngine.Extensions
{
    /// <summary>
    /// Extension methods to add custom functionality to RulesengineCompany
    /// without modifying the generated code.
    /// </summary>
    public static class RulesengineCompanyExtensions
    {
        /// <summary>
        /// Finds a trait by its definition ID.
        /// </summary>
        public static RulesengineTrait? GetTraitByDefinitionId(this RulesengineCompany company, string definitionId)
        {
            return company.Traits.FirstOrDefault(t => t.TraitDefinition?.Id == definitionId);
        }

        /// <summary>
        /// Returns a new RulesengineCompany with the metric added or updated.
        /// Thread-safe implementation using immutable collections.
        /// Usage: company = company.AddMetric(metric);
        /// </summary>
        public static RulesengineCompany AddMetric(this RulesengineCompany company, RulesengineCompanyMetric? metric)
        {
            if (metric == null)
            {
                return company;
            }

            // Use immutable operations for thread safety
            var currentMetrics = company.Metrics?.ToArray() ?? Array.Empty<RulesengineCompanyMetric>();
            
            // Create a concurrent dictionary for O(1) lookups by composite key
            var metricsDict = new ConcurrentDictionary<string, RulesengineCompanyMetric>();
            
            // Populate dictionary with existing metrics (thread-safe enumeration)
            foreach (var existingMetric in currentMetrics)
            {
                var key = GetMetricKey(existingMetric);
                metricsDict.TryAdd(key, existingMetric);
            }
            
            // Add or update the new metric (atomic operation)
            var newMetricKey = GetMetricKey(metric);
            metricsDict.AddOrUpdate(newMetricKey, metric, (key, existing) => metric);
            
            // Convert back to list (creates new collection)
            var updatedMetrics = metricsDict.Values.ToList();

            return company with { Metrics = updatedMetrics };
        }

        /// <summary>
        /// Creates a composite key for metric identification to ensure uniqueness.
        /// </summary>
        private static string GetMetricKey(RulesengineCompanyMetric metric)
        {
            return $"{metric.EventSubtype}|{metric.Period}|{metric.MonthReset}";
        }
    }
}