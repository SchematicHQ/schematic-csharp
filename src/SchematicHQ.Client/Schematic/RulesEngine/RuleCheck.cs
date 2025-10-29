using Schematic.RulesEngine.Models;
using Schematic.RulesEngine.Utils;

namespace Schematic.RulesEngine
{
    public class CheckScope
    {
        public Company? Company { get; set; }
        public Rule? Rule { get; set; }
        public User? User { get; set; }
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

            if (scope.Rule.RuleType == RuleType.Default || scope.Rule.RuleType == RuleType.GlobalOverride)
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

        private async Task<bool> CheckCondition(Company? company, User? user, Condition condition, CancellationToken cancellationToken)
        {
            if (condition == null)
            {
                return false;
            }

            switch (condition.ConditionType)
            {
                case ConditionType.Company:
                    return await CheckCompanyCondition(company, condition, cancellationToken);
                case ConditionType.Metric:
                    return await CheckMetricCondition(company, condition, cancellationToken);
                case ConditionType.BasePlan:
                    return await CheckBasePlanCondition(company, condition, cancellationToken);
                case ConditionType.Plan:
                    return await CheckPlanCondition(company, condition, cancellationToken);
                case ConditionType.Trait:
                    return await CheckTraitCondition(company, user, condition, cancellationToken);
                case ConditionType.User:
                    return await CheckUserCondition(user, condition, cancellationToken);
                case ConditionType.BillingProduct:
                    return await CheckBillingProductCondition(company, condition, cancellationToken);
                case ConditionType.CrmProduct:
                    return await CheckCrmProductCondition(company, condition, cancellationToken);
                case ConditionType.Credit:
                    return await CheckCreditBalanceCondition(company, condition, cancellationToken);
                default:
                    return false;
            }
        }

        private async Task<bool> CheckConditionGroup(Company? company, User? user, ConditionGroup group, CancellationToken cancellationToken)
        {
            if (group == null || group.Conditions.Count == 0)
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

        private Task<bool> CheckCompanyCondition(Company? company, Condition condition, CancellationToken cancellationToken)
        {
            if (condition.ConditionType != ConditionType.Company || company == null)
            {
              return Task.FromResult(false);
            }

            var resourceMatch = Set<string>.NewSet(condition.ResourceIds.ToArray()).Contains(company.Id);
            if (condition.Operator == ComparableOperator.Ne)
            {
                return Task.FromResult(!resourceMatch);
            }

            return Task.FromResult(resourceMatch);
        }

        private Task<bool> CheckBillingProductCondition(Company? company, Condition condition, CancellationToken cancellationToken)
        {
            if (condition.ConditionType != ConditionType.BillingProduct || company == null)
            {
                return Task.FromResult(false);
            }

            var companyBillingProductIds = Set<string>.NewSet(company.BillingProductIds.ToArray());
            var resourceMatch = Set<string>.NewSet(condition.ResourceIds.ToArray()).Intersection(companyBillingProductIds).Len > 0;
            if (condition.Operator == ComparableOperator.Ne)
            {
                return Task.FromResult(!resourceMatch);
            }

            return Task.FromResult(resourceMatch);
        }

        private Task<bool> CheckCrmProductCondition(Company? company, Condition condition, CancellationToken cancellationToken)
        {
            if (condition.ConditionType != ConditionType.CrmProduct || company == null)
            {
                return Task.FromResult(false);
            }

            var companyCrmProductIds = Set<string>.NewSet(company.CrmProductIds.ToArray());
            var resourceMatch = Set<string>.NewSet(condition.ResourceIds.ToArray()).Intersection(companyCrmProductIds).Len > 0;
            if (condition.Operator == ComparableOperator.Ne)
            {
                return Task.FromResult(!resourceMatch);
            }

            return Task.FromResult(resourceMatch);
        }

        private Task<bool> CheckCreditBalanceCondition(Company? company, Condition condition, CancellationToken cancellationToken)
        {
            if (condition.ConditionType != ConditionType.Credit || company == null || string.IsNullOrEmpty(condition.CreditId))
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

        private Task<bool> CheckPlanCondition(Company? company, Condition condition, CancellationToken cancellationToken)
        {
            if (condition.ConditionType != ConditionType.Plan || company == null)
            {
                return Task.FromResult(false);
            }

            var companyPlanIds = Set<string>.NewSet(company.PlanIds.ToArray());
            var resourceMatch = Set<string>.NewSet(condition.ResourceIds.ToArray()).Intersection(companyPlanIds).Len > 0;
            if (condition.Operator == ComparableOperator.Ne)
            {
                return Task.FromResult(!resourceMatch);
            }

            return Task.FromResult(resourceMatch);
        }

        private Task<bool> CheckBasePlanCondition(Company? company, Condition condition, CancellationToken cancellationToken)
        {
            if (condition.ConditionType != ConditionType.BasePlan || company == null || string.IsNullOrEmpty(company.BasePlanId))
            {
                return Task.FromResult(false);
            }

            var resourceMatch = Set<string>.NewSet(condition.ResourceIds.ToArray()).Contains(company.BasePlanId);
            if (condition.Operator == ComparableOperator.Ne)
            {
                return Task.FromResult(!resourceMatch);
            }

            return Task.FromResult(resourceMatch);
        }

        private Task<bool> CheckMetricCondition(Company? company, Condition condition, CancellationToken cancellationToken)
        {
            if (condition == null || condition.ConditionType != ConditionType.Metric || company == null || string.IsNullOrEmpty(condition.EventSubtype))
            {
                return Task.FromResult(false);
            }

            long leftVal = 0;
            var metric = CompanyMetric.Find(company.Metrics, condition.EventSubtype, condition.MetricPeriod, condition.MetricPeriodMonthReset);
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

            bool resourceMatch = TypeConverter.CompareInt64(leftVal, rightVal, condition.Operator);

            return Task.FromResult(resourceMatch);
        }

        private Task<bool> CheckTraitCondition(Company? company, User? user, Condition condition, CancellationToken cancellationToken)
        {
            if (condition == null || condition.ConditionType != ConditionType.Trait || condition.TraitDefinition == null)
            {
                return Task.FromResult(false);
            }

            var traitDef = condition.TraitDefinition;
            Trait? trait;
            Trait? comparisonTrait;

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

        private Task<bool> CheckUserCondition(User? user, Condition condition, CancellationToken cancellationToken)
        {
            if (condition.ConditionType != ConditionType.User || user == null)
            {
                return Task.FromResult(false);
            }

            var resourceMatch = Set<string>.NewSet(condition.ResourceIds.ToArray()).Contains(user.Id);
            if (condition.Operator == ComparableOperator.Ne)
            {
                return Task.FromResult(!resourceMatch);
            }

            return  Task.FromResult(resourceMatch);
        }

        static private bool CompareTraits(Condition condition, Trait? trait, Trait? comparisonTrait)
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
                comparableType = trait.TraitDefinition.ComparableType;
            }

            return TypeConverter.Compare(leftVal, rightVal, comparableType, condition.Operator);
        }

        static private Trait? FindTrait(TraitDefinition? traitDef, List<Trait>? traits)
        {
            if (traitDef == null || traits == null)
            {
                return null;
            }

            return traits.Find(t => t.TraitDefinition?.Id == traitDef.Id);
        }
    }
}
