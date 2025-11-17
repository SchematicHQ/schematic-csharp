using SchematicHQ.Client.RulesEngine.Utils;
using SchematicHQ.Client.RulesEngine.Extensions;

namespace SchematicHQ.Client.RulesEngine
{
    public class CheckScope
    {
        public RulesengineCompany? Company { get; set; }
        public RulesengineRule? Rule { get; set; }
        public RulesengineUser? User { get; set; }
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

            if (scope.Rule.RuleType == RulesengineRuleRuleType.Default.Value || scope.Rule.RuleType == RulesengineRuleRuleType.GlobalOverride.Value)
            {
                res.Match = true;
                return res;
            }

            bool match;
            foreach (var condition in scope.Rule.Conditions ?? Enumerable.Empty<RulesengineCondition>())
            {
                match = await CheckCondition(scope.Company, scope.User, condition, cancellationToken);
                if (!match)
                {
                    return res;
                }
            }

            foreach (var group in scope.Rule.ConditionGroups ?? Enumerable.Empty<RulesengineConditionGroup>())
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

        private async Task<bool> CheckCondition(RulesengineCompany? company, RulesengineUser? user, RulesengineCondition condition, CancellationToken cancellationToken)
        {
            if (condition == null)
            {
                return false;
            }

            // Use generated enum values directly
            if (condition.ConditionType == RulesengineConditionConditionType.Company)
                return await CheckCompanyCondition(company, condition, cancellationToken);
            if (condition.ConditionType == RulesengineConditionConditionType.Metric.Value)
                return await CheckMetricCondition(company, condition, cancellationToken);
            if (condition.ConditionType == RulesengineConditionConditionType.BasePlan.Value)
                return await CheckBasePlanCondition(company, condition, cancellationToken);
            if (condition.ConditionType == RulesengineConditionConditionType.Plan.Value)
                return await CheckPlanCondition(company, condition, cancellationToken);
            if (condition.ConditionType == RulesengineConditionConditionType.Trait.Value)
                return await CheckTraitCondition(company, user, condition, cancellationToken);
            if (condition.ConditionType == RulesengineConditionConditionType.User.Value)
                return await CheckUserCondition(user, condition, cancellationToken);
            if (condition.ConditionType == RulesengineConditionConditionType.BillingProduct.Value)
                return await CheckBillingProductCondition(company, condition, cancellationToken);
            if (condition.ConditionType == RulesengineConditionConditionType.CrmProduct.Value)
                return await CheckCrmProductCondition(company, condition, cancellationToken);
            if (condition.ConditionType == RulesengineConditionConditionType.Credit.Value)
                return await CheckCreditBalanceCondition(company, condition, cancellationToken);

            return false;
        }

        private async Task<bool> CheckConditionGroup(RulesengineCompany? company, RulesengineUser? user, RulesengineConditionGroup group, CancellationToken cancellationToken)
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

        private Task<bool> CheckCompanyCondition(RulesengineCompany? company, RulesengineCondition condition, CancellationToken cancellationToken)
        {
            if (condition.ConditionType != RulesengineConditionConditionType.Company || company == null)
            {
              return Task.FromResult(false);
            }

            var resourceMatch = Set<string>.NewSet(condition.ResourceIds.ToArray()).Contains(company.Id);
            if (condition.Operator == RulesengineConditionOperator.Ne)
            {
                return Task.FromResult(!resourceMatch);
            }

            return Task.FromResult(resourceMatch);
        }

        private Task<bool> CheckBillingProductCondition(RulesengineCompany? company, RulesengineCondition condition, CancellationToken cancellationToken)
        {
            if (condition.ConditionType != RulesengineConditionConditionType.BillingProduct || company == null)
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

        private Task<bool> CheckCrmProductCondition(RulesengineCompany? company, RulesengineCondition condition, CancellationToken cancellationToken)
        {
            if (condition.ConditionType != RulesengineConditionConditionType.CrmProduct || company == null)
            {
                return Task.FromResult(false);
            }

            var companyCrmProductIds = Set<string>.NewSet(company.CrmProductIds.ToArray());
            var resourceMatch = Set<string>.NewSet(condition.ResourceIds.ToArray()).Intersection(companyCrmProductIds).Len > 0;
            if (condition.Operator == RulesengineConditionOperator.Ne)
            {
                return Task.FromResult(!resourceMatch);
            }

            return Task.FromResult(resourceMatch);
        }

        private Task<bool> CheckCreditBalanceCondition(RulesengineCompany? company, RulesengineCondition condition, CancellationToken cancellationToken)
        {
            if (condition.ConditionType != RulesengineConditionConditionType.Credit || company == null || string.IsNullOrEmpty(condition.CreditId))
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

        private Task<bool> CheckPlanCondition(RulesengineCompany? company, RulesengineCondition condition, CancellationToken cancellationToken)
        {
            if (condition.ConditionType != RulesengineConditionConditionType.Plan || company == null)
            {
                return Task.FromResult(false);
            }

            var companyPlanIds = Set<string>.NewSet(company.PlanIds.ToArray());
            var resourceMatch = Set<string>.NewSet(condition.ResourceIds.ToArray()).Intersection(companyPlanIds).Len > 0;
            if (condition.Operator == RulesengineConditionOperator.Ne)
            {
                return Task.FromResult(!resourceMatch);
            }

            return Task.FromResult(resourceMatch);
        }

        private Task<bool> CheckBasePlanCondition(RulesengineCompany? company, RulesengineCondition condition, CancellationToken cancellationToken)
        {
            if (condition.ConditionType != RulesengineConditionConditionType.BasePlan || company == null || string.IsNullOrEmpty(company.BasePlanId))
            {
                return Task.FromResult(false);
            }

            var resourceMatch = Set<string>.NewSet(condition.ResourceIds.ToArray()).Contains(company.BasePlanId);
            if (condition.Operator == RulesengineConditionOperator.Ne)
            {
                return Task.FromResult(!resourceMatch);
            }

            return Task.FromResult(resourceMatch);
        }

        private Task<bool> CheckMetricCondition(RulesengineCompany? company, RulesengineCondition condition, CancellationToken cancellationToken)
        {
            if (condition == null || condition.ConditionType != RulesengineConditionConditionType.Metric || company == null || string.IsNullOrEmpty(condition.EventSubtype))
            {
                return Task.FromResult(false);
            }

            long leftVal = 0;
            var metric = company.Metrics.Find(condition.EventSubtype, condition.MetricPeriod, condition.MetricPeriodMonthReset);
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

        private Task<bool> CheckTraitCondition(RulesengineCompany? company, RulesengineUser? user, RulesengineCondition condition, CancellationToken cancellationToken)
        {
            if (condition == null || condition.ConditionType != RulesengineConditionConditionType.Trait || condition.TraitDefinition == null)
            {
                return Task.FromResult(false);
            }

            var traitDef = condition.TraitDefinition;
            RulesengineTrait? trait;
            RulesengineTrait? comparisonTrait;

            if (traitDef.EntityType == RulesengineTraitDefinitionEntityType.Company && company != null)
            {
                trait = FindTrait(traitDef, company.Traits);
                comparisonTrait = FindTrait(condition.ComparisonTraitDefinition, company.Traits);
            }
            else if (traitDef.EntityType == RulesengineTraitDefinitionEntityType.User && user != null)
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

        private Task<bool> CheckUserCondition(RulesengineUser? user, RulesengineCondition condition, CancellationToken cancellationToken)
        {
            if (condition.ConditionType != RulesengineConditionConditionType.User || user == null)
            {
                return Task.FromResult(false);
            }

            var resourceMatch = Set<string>.NewSet(condition.ResourceIds.ToArray()).Contains(user.Id);
            if (condition.Operator == RulesengineConditionOperator.Ne)
            {
                return Task.FromResult(!resourceMatch);
            }

            return  Task.FromResult(resourceMatch);
        }

        static private bool CompareTraits(RulesengineCondition condition, RulesengineTrait? trait, RulesengineTrait? comparisonTrait)
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

            var comparableType = RulesengineTraitDefinitionComparableType.String;
            if (trait != null && trait.TraitDefinition != null)
            {
                comparableType = trait.TraitDefinition.ComparableType;
            }

            return TypeConverter.Compare(leftVal, rightVal, comparableType, condition.Operator);
        }

        static private RulesengineTrait? FindTrait(RulesengineTraitDefinition? traitDef, IEnumerable<RulesengineTrait>? traits)
        {
            if (traitDef == null || traits == null)
            {
                return null;
            }

            return traits.FirstOrDefault(t => t.TraitDefinition?.Id == traitDef.Id);
        }
    }
}
