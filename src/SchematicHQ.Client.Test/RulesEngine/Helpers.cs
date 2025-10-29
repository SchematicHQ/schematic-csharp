using Schematic.RulesEngine;
using Schematic.RulesEngine.Models;
using Schematic.RulesEngine.Utils;

namespace SchematicHQ.Tests.RulesEngine
{
  public static class TestHelpers
  {
    private static readonly Random Random = new Random();

    public static string GenerateTestId(string prefix)
    {
      return $"{prefix}_{Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8)}";
    }

    public static global::Schematic.RulesEngine.Models.Company CreateTestCompany()
    {
      return new global::Schematic.RulesEngine.Models.Company
      {
        Id = GenerateTestId("comp"),
        AccountId = GenerateTestId("acct"),
        EnvironmentId = GenerateTestId("env"),
        PlanIds = new List<string> { GenerateTestId("plan") },
        Traits = new List<global::Schematic.RulesEngine.Models.Trait>(),
        Metrics = new List<global::Schematic.RulesEngine.Models.CompanyMetric>(),
        BillingProductIds = new List<string>(),
        CrmProductIds = new List<string>()
      };
    }

    public static global::Schematic.RulesEngine.Models.User CreateTestUser()
    {
      return new global::Schematic.RulesEngine.Models.User
      {
        Id = GenerateTestId("user"),
        AccountId = GenerateTestId("acct"),
        EnvironmentId = GenerateTestId("env"),
        Traits = new List<global::Schematic.RulesEngine.Models.Trait>()
      };
    }

    public static global::Schematic.RulesEngine.Models.Rule CreateTestRule()
    {
      return new global::Schematic.RulesEngine.Models.Rule
      {
        Id = GenerateTestId("rule"),
        AccountId = GenerateTestId("acct"),
        EnvironmentId = GenerateTestId("env"),
        RuleType = global::Schematic.RulesEngine.RuleType.Standard,
        Name = $"Test Rule {Random.Next(1000)}",
        Priority = 1,
        Conditions = new List<global::Schematic.RulesEngine.Models.Condition>(),
        ConditionGroups = new List<global::Schematic.RulesEngine.Models.ConditionGroup>(),
        Value = true
      };
    }

    public static global::Schematic.RulesEngine.Models.Flag CreateTestFlag()
    {
      return new global::Schematic.RulesEngine.Models.Flag
      {
        Id = GenerateTestId("flag"),
        AccountId = GenerateTestId("acct"),
        EnvironmentId = GenerateTestId("env"),
        Key = $"test_flag_{Random.Next(1000)}",
        Rules = new List<global::Schematic.RulesEngine.Models.Rule>(),
        DefaultValue = Random.Next(2) == 0 // Random boolean
      };
    }

    public static global::Schematic.RulesEngine.Models.Condition CreateTestCondition(global::Schematic.RulesEngine.ConditionType conditionType)
    {
      var condition = new global::Schematic.RulesEngine.Models.Condition
      {
        Id = GenerateTestId("cond"),
        AccountId = GenerateTestId("acct"),
        EnvironmentId = GenerateTestId("env"),
        ConditionType = conditionType,
        ResourceIds = new List<string>(),
        Operator = global::Schematic.RulesEngine.Utils.ComparableOperator.Eq
      };

      if (conditionType == global::Schematic.RulesEngine.ConditionType.Metric)
      {
        condition.EventSubtype = $"test_event_{Random.Next(1000)}";
        condition.MetricPeriod = global::Schematic.RulesEngine.MetricPeriod.AllTime;
        condition.MetricValue = 10;
      }

      return condition;
    }

    public static global::Schematic.RulesEngine.Models.CompanyMetric CreateTestMetric(
      global::Schematic.RulesEngine.Models.Company company,
      string eventSubtype,
      global::Schematic.RulesEngine.MetricPeriod period,
      long value)
    {
      return new global::Schematic.RulesEngine.Models.CompanyMetric
      {
        EventSubtype = eventSubtype,
        Period = period,
        Value = value,
        CreatedAt = DateTime.UtcNow,
      };
    }


    public static global::Schematic.RulesEngine.Models.Trait CreateTestTrait(string value, global::Schematic.RulesEngine.Models.TraitDefinition? traitDefinition)
    {
      if (traitDefinition == null)
      {
        traitDefinition = CreateTestTraitDefinition(global::Schematic.RulesEngine.Utils.ComparableType.String, global::Schematic.RulesEngine.EntityType.Company);
      }

      return new global::Schematic.RulesEngine.Models.Trait
      {
        Value = value,
        TraitDefinition = traitDefinition,
      };
    }

    public static global::Schematic.RulesEngine.Models.TraitDefinition CreateTestTraitDefinition(
      global::Schematic.RulesEngine.Utils.ComparableType comparableType,
      global::Schematic.RulesEngine.EntityType entityType)
    {
      return new global::Schematic.RulesEngine.Models.TraitDefinition
      {
        Id = GenerateTestId("traitdef"),
        ComparableType = comparableType,
        EntityType = entityType
      };
    }
  }
}