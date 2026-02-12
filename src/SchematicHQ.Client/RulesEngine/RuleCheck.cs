using SchematicHQ.Client.RulesEngine.Models;
using SchematicHQ.Client.RulesEngine.Utils;

namespace SchematicHQ.Client.RulesEngine
{
    public class CheckScope
    {
        public Models.Company? Company { get; set; }
        public Rule? Rule { get; set; }
        public Models.User? User { get; set; }
    }

    public class CheckResult
    {
        public CheckScope? CheckScope { get; set; }
        public bool Match { get; set; }
    }

    public class RuleCheckService
    {
        public static RuleCheckService NewRuleCheckService()
        {
            return new RuleCheckService();
        }

        public async Task<CheckResult> Check(CheckScope scope, CancellationToken cancellationToken = default)
        {
            var res = new CheckResult
            {
                CheckScope = scope
            };

            if (scope.Rule == null)
            {
                return res;
            }

            if (scope.Rule.RuleType == RuleRuleType.Default.Value || scope.Rule.RuleType == RuleRuleType.GlobalOverride.Value)
            {
                res.Match = true;
                return res;
            }

            bool match;
            foreach (var condition in scope.Rule.Conditions ?? Enumerable.Empty<Condition>())
            {
                match = await CheckCondition(scope.Company, scope.User, condition, cancellationToken);
                if (!match)
                {
                    return res;
                }
            }

            foreach (var group in scope.Rule.ConditionGroups ?? Enumerable.Empty<ConditionGroup>())
            {
                match = await CheckConditionGroup(scope.Company, scope.User, group, cancellationToken);
                if (!match)
                {
                    return res;
                }
            }

            res.Match = true;
            return res;
        }

        private async Task<bool> CheckCondition(Models.Company? company, Models.User? user, Condition condition, CancellationToken cancellationToken)
        {
            if (condition == null)
            {
                return false;
            }

            // Use generated enum values directly
            if (condition.ConditionType == ConditionConditionType.Company)
                return await CheckCompanyCondition(company, condition, cancellationToken);
            if (condition.ConditionType == ConditionConditionType.Metric.Value)
                return await CheckMetricCondition(company, condition, cancellationToken);
            if (condition.ConditionType == ConditionConditionType.BasePlan.Value)
                return await CheckBasePlanCondition(company, condition, cancellationToken);
            if (condition.ConditionType == ConditionConditionType.Plan.Value)
                return await CheckPlanCondition(company, condition, cancellationToken);
            if (condition.ConditionType == ConditionConditionType.Trait.Value)
                return await CheckTraitCondition(company, user, condition, cancellationToken);
            if (condition.ConditionType == ConditionConditionType.User.Value)
                return await CheckUserCondition(user, condition, cancellationToken);
            if (condition.ConditionType == ConditionConditionType.BillingProduct.Value)
                return await CheckBillingProductCondition(company, condition, cancellationToken);
            if (condition.ConditionType == ConditionConditionType.Credit.Value)
                return await CheckCreditBalanceCondition(company, condition, cancellationToken);

            return false;
        }

        private async Task<bool> CheckConditionGroup(Models.Company? company, Models.User? user, ConditionGroup group, CancellationToken cancellationToken)
        {
            if (group == null || !group.Conditions.Any())
            {
                return false;
            }

            foreach (var condition in group.Conditions)
            {
                if (await CheckCondition(company, user, condition, cancellationToken))
                {
                    return true;
                }
            }

            return false;
        }

        private Task<bool> CheckCompanyCondition(Models.Company? company, Condition condition, CancellationToken cancellationToken)
        {
            if (condition.ConditionType != ConditionConditionType.Company || company == null)
            {
              return Task.FromResult(false);
            }

            var resourceMatch = Set<string>.NewSet(condition.ResourceIds.ToArray()).Contains(company.Id);
            if (condition.Operator.ToComparableOperator() == ComparableOperator.Ne)
            {
                return Task.FromResult(!resourceMatch);
            }

            return Task.FromResult(resourceMatch);
        }

        private Task<bool> CheckBillingProductCondition(Models.Company? company, Condition condition, CancellationToken cancellationToken)
        {
            if (condition.ConditionType != ConditionConditionType.BillingProduct || company == null)
            {
                return Task.FromResult(false);
            }

            var companyBillingProductIds = Set<string>.NewSet(company.BillingProductIds.ToArray());
            var resourceMatch = Set<string>.NewSet(condition.ResourceIds.ToArray()).Intersection(companyBillingProductIds).Len > 0;
            if (condition.Operator.ToComparableOperator() == ComparableOperator.Ne)
            {
                return Task.FromResult(!resourceMatch);
            }

            return Task.FromResult(resourceMatch);
        }

        private Task<bool> CheckCreditBalanceCondition(Models.Company? company, Condition condition, CancellationToken cancellationToken)
        {
            if (condition.ConditionType != ConditionConditionType.Credit || company == null || string.IsNullOrEmpty(condition.CreditId))
            {
                return Task.FromResult(false);
            }

            double consumptionCost = 1.0;
            if (condition.ConsumptionRate.HasValue)
            {
                consumptionCost = condition.ConsumptionRate.Value;
            }

            double creditBalance = 0.0;
            if (company.CreditBalances != null && company.CreditBalances.TryGetValue(condition.CreditId, out double balance))
            {
                creditBalance = balance;
            }

            return Task.FromResult(creditBalance >= consumptionCost);
        }

        private Task<bool> CheckPlanCondition(Models.Company? company, Condition condition, CancellationToken cancellationToken)
        {
            if (condition.ConditionType != ConditionConditionType.Plan || company == null)
            {
                return Task.FromResult(false);
            }

            var companyPlanIds = Set<string>.NewSet(company.PlanIds.ToArray());
            var resourceMatch = Set<string>.NewSet(condition.ResourceIds.ToArray()).Intersection(companyPlanIds).Len > 0;
            if (condition.Operator.ToComparableOperator() == ComparableOperator.Ne)
            {
                return Task.FromResult(!resourceMatch);
            }

            return Task.FromResult(resourceMatch);
        }

        private Task<bool> CheckBasePlanCondition(Models.Company? company, Condition condition, CancellationToken cancellationToken)
        {
            if (condition.ConditionType != ConditionConditionType.BasePlan || company == null || string.IsNullOrEmpty(company.BasePlanId))
            {
                return Task.FromResult(false);
            }

            var resourceMatch = Set<string>.NewSet(condition.ResourceIds.ToArray()).Contains(company.BasePlanId);
            if (condition.Operator.ToComparableOperator() == ComparableOperator.Ne)
            {
                return Task.FromResult(!resourceMatch);
            }

            return Task.FromResult(resourceMatch);
        }

        private Task<bool> CheckMetricCondition(Models.Company? company, Condition condition, CancellationToken cancellationToken)
        {
            if (condition == null || condition.ConditionType != ConditionConditionType.Metric || company == null || string.IsNullOrEmpty(condition.EventSubtype))
            {
                return Task.FromResult(false);
            }

            long leftVal = 0;
            var metric = Models.CompanyMetric.Find(company.Metrics, condition.EventSubtype, condition.MetricPeriod, condition.MetricPeriodMonthReset);
            if (metric != null)
            {
                leftVal = metric.Value;
            }

            if (!condition.MetricValue.HasValue)
            {
                throw new InvalidOperationException($"Expected metric value for condition: {condition.Id}, but received null");
            }

            long rightVal = condition.MetricValue.Value;
            if (condition.ComparisonTraitDefinition != null)
            {
                var comparisonTrait = FindTrait(condition.ComparisonTraitDefinition, company.Traits);
                if (comparisonTrait == null)
                {
                    rightVal = 0;
                }
                else
                {
                    rightVal = TypeConverter.StringToInt64(comparisonTrait.Value);
                }
            }

            bool resourceMatch = TypeConverter.CompareInt64(leftVal, rightVal, condition.Operator.ToComparableOperator());

            return Task.FromResult(resourceMatch);
        }

        private Task<bool> CheckTraitCondition(Models.Company? company, Models.User? user, Condition condition, CancellationToken cancellationToken)
        {
            if (condition == null || condition.ConditionType != ConditionConditionType.Trait || condition.TraitDefinition == null)
            {
                return Task.FromResult(false);
            }

            var traitDef = condition.TraitDefinition;
            Models.Trait? trait;
            Models.Trait? comparisonTrait;

            if (traitDef.EntityType == EntityType.Company && company != null)
            {
                trait = FindTrait(traitDef, company.Traits);
                comparisonTrait = FindTrait(condition.ComparisonTraitDefinition, company.Traits);
            }
            else if (traitDef.EntityType == EntityType.User && user != null)
            {
                trait = FindTrait(traitDef, user.Traits);
                comparisonTrait = FindTrait(condition.ComparisonTraitDefinition, user.Traits);
            }
            else
            {
                return Task.FromResult(false);
            }

            bool resourceMatch = CompareTraits(condition, trait, comparisonTrait);

            return Task.FromResult(resourceMatch);
        }

        private Task<bool> CheckUserCondition(Models.User? user, Condition condition, CancellationToken cancellationToken)
        {
            if (condition.ConditionType != ConditionConditionType.User || user == null)
            {
                return Task.FromResult(false);
            }

            var resourceMatch = Set<string>.NewSet(condition.ResourceIds.ToArray()).Contains(user.Id);
            if (condition.Operator.ToComparableOperator() == ComparableOperator.Ne)
            {
                return Task.FromResult(!resourceMatch);
            }

            return  Task.FromResult(resourceMatch);
        }

        static private bool CompareTraits(Condition condition, Models.Trait? trait, Models.Trait? comparisonTrait)
        {
            string leftVal = "";
            string rightVal = condition.TraitValue ?? "";

            if (trait != null)
            {
                leftVal = trait.Value;
            }

            if (comparisonTrait != null)
            {
                rightVal = comparisonTrait.Value;
            }

            var comparableType = ComparableType.String;
            if (trait != null && trait.TraitDefinition != null)
            {
                comparableType = trait.TraitDefinition.ComparableType.ToComparableType();
            }

            return TypeConverter.Compare(leftVal, rightVal, comparableType, condition.Operator.ToComparableOperator());
        }

        static private Models.Trait? FindTrait(TraitDefinition? traitDef, List<Models.Trait>? traits)
        {
            if (traitDef == null || traits == null)
            {
                return null;
            }

            return traits.Find(t => t.TraitDefinition?.Id == traitDef.Id);
        }
    }
}
