using SchematicHQ.RulesEngine.Models;
using SchematicHQ.RulesEngine.Utils;

namespace SchematicHQ.RulesEngine
{
    public class CheckFlagResult
    {
        public string? CompanyId { get; set; }
        public Exception? Error { get; set; }
        public long? FeatureAllocation { get; set; }
        public long? FeatureUsage { get; set; }
        public MetricPeriod? FeatureUsagePeriod { get; set; }
        public DateTime? FeatureUsageResetAt { get; set; }
        public string? FlagId { get; set; }
        public required string FlagKey { get; set; }
        public required string Reason { get; set; }
        public string? RuleId { get; set; }
        public RuleType? RuleType { get; set; }
        public string? UserId { get; set; }
        public bool Value { get; set; }

        public void SetRuleFields(Company? company, Rule? rule)
        {
            if (rule == null)
            {
                return;
            }

            RuleId = rule.Id;
            RuleType = rule.RuleType;
    

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
                c != null && (c.ConditionType == ConditionType.Metric || c.ConditionType == ConditionType.Trait));
                
            if (usageCondition == null)
            {
                return;
            }

            // Set usage, allocation, and other usage-related fields
            long usage = 0;
            long allocation = 0;
            
            if (usageCondition.ConditionType == ConditionType.Metric)
            {
                if (!string.IsNullOrEmpty(usageCondition.EventSubtype))
                {
                    var usageMetric = CompanyMetric.Find(
                        company.Metrics, 
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

                var metricPeriod = MetricPeriod.AllTime;
                if (usageCondition.MetricPeriod.HasValue)
                {
                    metricPeriod = usageCondition.MetricPeriod.Value;
                }
                
                FeatureUsagePeriod = metricPeriod;
                FeatureUsageResetAt = Metrics.GetNextMetricPeriodStartFromCondition(usageCondition, company);
            }
            else if (usageCondition.ConditionType == ConditionType.Trait)
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

            FeatureUsage = usage;
            FeatureAllocation = allocation;
        }
    }

    public static class Errors
    {
        public static readonly Exception ErrorUnexpected = new Exception("Unexpected error");
        public static readonly Exception ErrorFlagNotFound = new Exception("Flag not found");
    }

    public static class FlagCheckService
    {
        public const string ReasonNoCompanyOrUser = "No company or user context; default value for flag";
        public const string ReasonCompanyNotFound = "Company not found";
        public const string ReasonCompanyNotSpecified = "Must specify a company";
        public const string ReasonFlagNotFound = "Flag not found";
        public const string ReasonNoRulesMatched = "No rules matched; default value for flag";
        public const string ReasonServerError = "Server error; Schematic has been notified";
        public const string ReasonUserNotFound = "User not found";

        public static async Task<CheckFlagResult> CheckFlag(
            Company? company,
            User? user,
            Flag? flag,
            CancellationToken cancellationToken = default)
        {
            var resp = new CheckFlagResult
            {
              Reason = ReasonNoRulesMatched,
              FlagKey = "",
            };

            if (flag == null)
            {
                resp.Reason = ReasonFlagNotFound;
                resp.Error = Errors.ErrorFlagNotFound;
                return resp;
            }

            resp.FlagId = flag.Id;
            resp.FlagKey = flag.Key;
            resp.Value = flag.DefaultValue;

            if (company != null)
            {
                resp.CompanyId = company.Id;
            }
            
            if (user != null)
            {
                resp.UserId = user.Id;
            }

            var ruleChecker = RuleCheckService.NewRuleCheckService();
            foreach (var group in GroupRulesByPriority(flag.Rules))
            {
                foreach (var rule in group)
                {
                    if (rule == null)
                    {
                        continue;
                    }

                    try
                    {
                        var checkRuleResp = await ruleChecker.Check(new CheckScope
                        {
                            Company = company,
                            Rule = rule,
                            User = user
                        }, cancellationToken);
                        
                        if (checkRuleResp == null)
                        {
                            resp.Error = Errors.ErrorUnexpected;
                            return resp;
                        }

                        if (checkRuleResp.Match)
                        {
                            resp.Value = rule.Value;
                            resp.Reason = $"Matched {rule.RuleType.DisplayName()} rule \"{rule.Name}\" ({rule.Id})";
                            resp.SetRuleFields(company, rule);
                            return resp;
                        }
                    }
                    catch (Exception ex)
                    {
                        resp.Error = ex;
                        return resp;
                    }
                }
            }

            return resp;
        }

        public static List<List<Rule>> GroupRulesByPriority(List<Rule> rules)
        {
            if (rules == null || rules.Count == 0)
            {
                return new List<List<Rule>>();
            }

            // Group rules by their type
            var grouped = rules.GroupBy(rule => rule.RuleType)
                .ToDictionary(g => g.Key, g => g.ToList());

            // Prioritize rules within each type group
            foreach (var ruleType in grouped.Keys.ToList())
            {
                var rulesForType = grouped[ruleType];
                switch (ruleType.PrioritizationMethod())
                {
                    case RulePrioritizationMethod.Priority:
                        grouped[ruleType] = rulesForType.OrderBy(r => r.Priority).ToList();
                        break;
                    case RulePrioritizationMethod.Optimistic:
                        grouped[ruleType] = rulesForType.OrderByDescending(r => r.Value).ToList();
                        break;
                }
            }

            // Prioritize type groups relative to one another
            var prioritizedGroups = new List<List<Rule>>();
            foreach (var ruleType in RuleTypeExtensions.RuleTypePriority)
            {
                if (grouped.TryGetValue(ruleType, out var rules2))
                {
                    prioritizedGroups.Add(rules2);
                }
            }

            return prioritizedGroups;
        }
    }
}