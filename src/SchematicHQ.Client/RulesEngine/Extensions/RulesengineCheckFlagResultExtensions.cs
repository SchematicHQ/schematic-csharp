using SchematicHQ.Client.RulesEngine.Utils;

namespace SchematicHQ.Client.RulesEngine.Extensions
{
    /// <summary>
    /// Extension methods to add custom functionality to RulesengineCheckFlagResult
    /// without modifying the generated code.
    /// </summary>
    public static class RulesengineCheckFlagResultExtensions
    {
        /// <summary>
        /// Sets rule-related fields on the check flag result based on the company and rule.
        /// </summary>
        public static void SetRuleFields(this RulesengineCheckFlagResult result, RulesengineCompany? company, RulesengineRule? rule)
        {
            if (rule == null)
            {
                return;
            }

            result.RuleId = rule.Id;
            result.RuleType = ConvertRuleType(rule.RuleType);

            if (company == null)
            {
                return;
            }

            // Only set entitlement fields if the matched rule is an entitlement rule
            if (!rule.RuleType.IsEntitlement())
            {
                return;
            }

            // For a numeric entitlement rule, there will be a metric or trait condition;
            // for a boolean or unlimited entitlement rule, we don't need to set these fields
            var usageCondition = rule.Conditions.FirstOrDefault(c =>
                c != null && (c.ConditionType == RulesengineConditionConditionType.Metric || c.ConditionType == RulesengineConditionConditionType.Trait));
                
            if (usageCondition == null)
            {
                return;
            }

            // Set usage, allocation, and other usage-related fields
            long usage = 0;
            long allocation = 0;

            if (usageCondition.ConditionType == RulesengineConditionConditionType.Metric)
            {
                if (!string.IsNullOrEmpty(usageCondition.EventSubtype))
                {
                    var usageMetric = company.Metrics.Find(
                        usageCondition.EventSubtype,
                        usageCondition.MetricPeriod,
                        usageCondition.MetricPeriodMonthReset
                    );

                    if (usageMetric != null)
                    {
                        usage = usageMetric.Value;
                    }
                }

                if (usageCondition.MetricValue.HasValue)
                {
                    allocation = usageCondition.MetricValue.Value;
                }

                var metricPeriod = usageCondition.MetricPeriod;

                result.FeatureUsagePeriod = ConvertMetricPeriod(metricPeriod);
                result.FeatureUsageResetAt = Metrics.GetNextMetricPeriodStartFromCondition(usageCondition, company);
            }
            else if (usageCondition.ConditionType == RulesengineConditionConditionType.Trait)
            {
                if (usageCondition.TraitDefinition != null)
                {
                    var companyUsageTrait = company.GetTraitByDefinitionId(usageCondition.TraitDefinition.Id);
                    if (companyUsageTrait != null)
                    {
                        usage = TypeConverter.StringToInt64(companyUsageTrait.Value);
                    }
                }

                allocation = TypeConverter.StringToInt64(usageCondition.TraitValue);
            }

            // If there is a comparison trait, this takes precedence for allocation over the numeric value
            if (usageCondition.ComparisonTraitDefinition != null)
            {
                var companyAllocationTrait = company.GetTraitByDefinitionId(usageCondition.ComparisonTraitDefinition.Id);
                if (companyAllocationTrait != null)
                {
                    allocation = TypeConverter.StringToInt64(companyAllocationTrait.Value);
                }
            }

            result.FeatureUsage = (int)usage;
            result.FeatureAllocation = (int)allocation;
        }

        /// <summary>
        /// Converts RulesengineRuleRuleType to RulesengineCheckFlagResultRuleType.
        /// </summary>
        private static RulesengineCheckFlagResultRuleType? ConvertRuleType(RulesengineRuleRuleType ruleType)
        {
            // Convert the string value to the appropriate enum type
            return new RulesengineCheckFlagResultRuleType(ruleType.Value);
        }

        /// <summary>
        /// Converts RulesengineConditionMetricPeriod to RulesengineCheckFlagResultFeatureUsagePeriod.
        /// </summary>
        private static RulesengineCheckFlagResultFeatureUsagePeriod? ConvertMetricPeriod(RulesengineConditionMetricPeriod? metricPeriod)
        {
            if (metricPeriod == null)
                return null;

            return new RulesengineCheckFlagResultFeatureUsagePeriod(metricPeriod.Value.Value);
        }
    }
}