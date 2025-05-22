using SchematicHQ.RulesEngine.Models;
using SchematicHQ.RulesEngine.Utils;

namespace SchematicHQ.RulesEngine.Test
{
    public static class TestHelpers
    {
        private static readonly Random Random = new Random();
        
        public static string GenerateTestId(string prefix)
        {
            return $"{prefix}_{Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8)}";
        }
        
        public static Company CreateTestCompany()
        {
            return new Company
            {
                Id = GenerateTestId("comp"),
                AccountId = GenerateTestId("acct"),
                EnvironmentId = GenerateTestId("env"),
                PlanIds = new List<string> { GenerateTestId("plan") },
                Traits = new List<Trait>(),
                Metrics = new List<CompanyMetric>(),
                BillingProductIds = new List<string>(),
                CrmProductIds = new List<string>()
            };
        }
        
        public static User CreateTestUser()
        {
            return new User
            {
                Id = GenerateTestId("user"),
                AccountId = GenerateTestId("acct"),
                EnvironmentId = GenerateTestId("env"),
                Traits = new List<Trait>()
            };
        }
        
        public static Rule CreateTestRule()
        {
            return new Rule
            {
                Id = GenerateTestId("rule"),
                AccountId = GenerateTestId("acct"),
                EnvironmentId = GenerateTestId("env"),
                RuleType = RuleType.Standard,
                Name = $"Test Rule {Random.Next(1000)}",
                Priority = 1,
                Conditions = new List<Condition>(),
                ConditionGroups = new List<ConditionGroup>(),
                Value = true
            };
        }
        
        public static Flag CreateTestFlag()
        {
            return new Flag
            {
                Id = GenerateTestId("flag"),
                AccountId = GenerateTestId("acct"),
                EnvironmentId = GenerateTestId("env"),
                Key = $"test_flag_{Random.Next(1000)}",
                Rules = new List<Rule>(),
                DefaultValue = Random.Next(2) == 0 // Random boolean
            };
        }
        
        public static Condition CreateTestCondition(ConditionType conditionType)
        {
            var condition = new Condition
            {
                Id = GenerateTestId("cond"),
                AccountId = GenerateTestId("acct"),
                EnvironmentId = GenerateTestId("env"),
                ConditionType = conditionType,
                ResourceIds = new List<string>(),
                Operator = ComparableOperator.Equals
            };
            
            if (conditionType == ConditionType.Metric)
            {
                condition.EventSubtype = $"test_event_{Random.Next(1000)}";
                condition.MetricPeriod = MetricPeriod.AllTime;
                condition.MetricValue = 10;
            }
            
            return condition;
        }
        
        public static CompanyMetric CreateTestMetric(Company company, string eventSubtype, MetricPeriod period, long value)
        {
            return new CompanyMetric
            {
                EventSubtype = eventSubtype,
                Period = period,
                Value = value,
                CreatedAt = DateTime.UtcNow,
            };
        }
        

    public static Trait CreateTestTrait(string value, TraitDefinition? traitDefinition)
    {
      if (traitDefinition == null)
      {
        traitDefinition = CreateTestTraitDefinition(ComparableType.String, EntityType.Company);
      }

      return new Trait{
        Value = value,
        TraitDefinition = traitDefinition,
        };
    }
        
        public static TraitDefinition CreateTestTraitDefinition(ComparableType comparableType, EntityType entityType)
        {
            return new TraitDefinition
            {
                Id = GenerateTestId("traitdef"),
                ComparableType = comparableType,
                EntityType = entityType
            };
        }
    }
}