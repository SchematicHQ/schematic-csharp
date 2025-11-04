using SchematicHQ.Client;
using SchematicHQ.Client.RulesEngine;
using SchematicHQ.Client.RulesEngine.Models;
using SchematicHQ.Client.RulesEngine.Utils;

namespace SchematicHQ.Client.Test.RulesEngine
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
        RuleType = RuleRuleType.Standard,
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

    public static Condition CreateTestCondition(ConditionConditionType conditionType)
    {
      var condition = new Condition
      {
        Id = GenerateTestId("cond"),
        AccountId = GenerateTestId("acct"),
        EnvironmentId = GenerateTestId("env"),
        ConditionType = conditionType,
        ResourceIds = new List<string>(),
        Operator = ConditionOperator.Eq,
        TraitValue = ""
      };

      if (conditionType == ConditionConditionType.Metric)
      {
        condition.EventSubtype = $"test_event_{Random.Next(1000)}";
        condition.MetricPeriod = ConditionMetricPeriod.AllTime;
        condition.MetricValue = 10;
      }

      return condition;
    }

    public static CompanyMetric CreateTestMetric(Company company, string eventSubtype, ConditionMetricPeriod period, long value)
    {
      return new CompanyMetric
      {
        EventSubtype = eventSubtype,
        Period = period,
        MonthReset = ConditionMetricPeriodMonthReset.FirstOfMonth,
        Value = value,
        CreatedAt = DateTime.UtcNow,
      };
    }


    public static Trait CreateTestTrait(string value, TraitDefinition? traitDefinition)
    {
      if (traitDefinition == null)
      {
        traitDefinition = CreateTestTraitDefinition(TraitDefinitionComparableType.String, TraitDefinitionEntityType.Company);
      }

      return new Trait
      {
        Value = value,
        TraitDefinition = traitDefinition,
      };
    }

    public static TraitDefinition CreateTestTraitDefinition(TraitDefinitionComparableType comparableType, TraitDefinitionEntityType entityType)
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
