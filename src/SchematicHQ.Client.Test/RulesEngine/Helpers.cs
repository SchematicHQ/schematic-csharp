using SchematicHQ.Client;
using SchematicHQ.Client.RulesEngine;
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

    public static RulesengineCompany CreateTestCompany()
    {
      return new RulesengineCompany
      {
        Id = GenerateTestId("comp"),
        AccountId = GenerateTestId("acct"),
        EnvironmentId = GenerateTestId("env"),
        PlanIds = new List<string> { GenerateTestId("plan") },
        Traits = new List<RulesengineTrait>(),
        Metrics = new List<RulesengineCompanyMetric>(),
        BillingProductIds = new List<string>(),
        CrmProductIds = new List<string>()
      };
    }

    public static RulesengineUser CreateTestUser()
    {
      return new RulesengineUser
      {
        Id = GenerateTestId("user"),
        AccountId = GenerateTestId("acct"),
        EnvironmentId = GenerateTestId("env"),
        Traits = new List<RulesengineTrait>()
      };
    }

    public static RulesengineRule CreateTestRule()
    {
      return new RulesengineRule
      {
        Id = GenerateTestId("rule"),
        AccountId = GenerateTestId("acct"),
        EnvironmentId = GenerateTestId("env"),
        RuleType = RulesengineRuleRuleType.Standard,
        Name = $"Test Rule {Random.Next(1000)}",
        Priority = 1,
        Conditions = new List<RulesengineCondition>(),
        ConditionGroups = new List<RulesengineConditionGroup>(),
        Value = true
      };
    }

    public static RulesengineFlag CreateTestFlag()
    {
      return new RulesengineFlag
      {
        Id = GenerateTestId("flag"),
        AccountId = GenerateTestId("acct"),
        EnvironmentId = GenerateTestId("env"),
        Key = $"test_flag_{Random.Next(1000)}",
        Rules = new List<RulesengineRule>(),
        DefaultValue = Random.Next(2) == 0 // Random boolean
      };
    }

    public static RulesengineCondition CreateTestCondition(RulesengineConditionConditionType conditionType)
    {
      var condition = new RulesengineCondition
      {
        Id = GenerateTestId("cond"),
        AccountId = GenerateTestId("acct"),
        EnvironmentId = GenerateTestId("env"),
        ConditionType = conditionType,
        ResourceIds = new List<string>(),
        Operator = RulesengineConditionOperator.Eq,
        TraitValue = ""
      };

      if (conditionType == RulesengineConditionConditionType.Metric)
      {
        condition.EventSubtype = $"test_event_{Random.Next(1000)}";
        condition.MetricPeriod = RulesengineConditionMetricPeriod.AllTime;
        condition.MetricValue = 10;
      }

      return condition;
    }

    public static RulesengineCompanyMetric CreateTestMetric(RulesengineCompany company, string eventSubtype, RulesengineCompanyMetricPeriod period, int value)
    {
      return new RulesengineCompanyMetric
      {
        AccountId = company?.AccountId,
        EnvironmentId = company?.EnvironmentId,
        CompanyId = company?.Id,
        EventSubtype = eventSubtype,
        Period = period,
        MonthReset = RulesengineCompanyMetricMonthReset.FirstOfMonth,
        Value = value,
        CreatedAt = DateTime.UtcNow,
      };
    }


    public static RulesengineTrait CreateTestTrait(string value,RulesengineTraitDefinition? traitDefinition)
    {
      if (traitDefinition == null)
      {
        traitDefinition = CreateTestTraitDefinition(RulesengineTraitDefinitionComparableType.String, RulesengineTraitDefinitionEntityType.Company);
      }

      return new RulesengineTrait
      {
        Value = value,
        TraitDefinition = traitDefinition,
      };
    }

    public static RulesengineTraitDefinition CreateTestTraitDefinition(RulesengineTraitDefinitionComparableType comparableType, RulesengineTraitDefinitionEntityType entityType)
    {
      return new RulesengineTraitDefinition
      {
        Id = GenerateTestId("traitdef"),
        ComparableType = comparableType,
        EntityType = entityType
      };
    }
  }
}
