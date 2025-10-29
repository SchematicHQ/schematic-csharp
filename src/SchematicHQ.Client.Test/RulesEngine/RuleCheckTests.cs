using NUnit.Framework;

// Type aliases using global:: prefix to avoid conflicts with Schematic class
using Company = global::Schematic.RulesEngine.Models.Company;
using User = global::Schematic.RulesEngine.Models.User;
using Rule = global::Schematic.RulesEngine.Models.Rule;
using Condition = global::Schematic.RulesEngine.Models.Condition;
using ConditionGroup = global::Schematic.RulesEngine.Models.ConditionGroup;
using Trait = global::Schematic.RulesEngine.Models.Trait;
using TraitDefinition = global::Schematic.RulesEngine.Models.TraitDefinition;
using Subscription = global::Schematic.RulesEngine.Models.Subscription;
using CompanyMetric = global::Schematic.RulesEngine.Models.CompanyMetric;
using ConditionType = global::Schematic.RulesEngine.ConditionType;
using RuleType = global::Schematic.RulesEngine.RuleType;
using ComparableOperator = global::Schematic.RulesEngine.Utils.ComparableOperator;

using Schematic.RulesEngine;
using Schematic.RulesEngine.Models;
using Schematic.RulesEngine.Utils;

namespace SchematicHQ.Tests.RulesEngine
{
  [TestFixture]
  public class RuleCheckServiceTests
  {
    [Test]
    public async Task Check_ReturnsTrue_ForGlobalOverrideRules()
    {
      // Arrange
      var svc = RuleCheckService.NewRuleCheckService();
      var company = TestHelpers.CreateTestCompany();
      var rule = TestHelpers.CreateTestRule();
      rule.RuleType = RuleType.GlobalOverride;

      // Act
      var result = await svc.Check(new CheckScope
      {
        Company = company,
        Rule = rule
      });

      // Assert
      Assert.That(result, Is.Not.Null);
      Assert.That(result.Match, Is.True);
    }

    [Test]
    public async Task Check_ReturnsFalse_ForNilRule()
    {
      // Arrange
      var svc = RuleCheckService.NewRuleCheckService();
      var company = TestHelpers.CreateTestCompany();

      // Act
      var result = await svc.Check(new CheckScope
      {
        Company = company,
        Rule = null
      });

      // Assert
      Assert.That(result, Is.Not.Null);
      Assert.That(result.Match, Is.False);
    }

    [Test]
    public async Task Check_ReturnsTrue_ForDefaultRules()
    {
      // Arrange
      var svc = RuleCheckService.NewRuleCheckService();
      var company = TestHelpers.CreateTestCompany();
      var rule = TestHelpers.CreateTestRule();
      rule.RuleType = RuleType.Default;

      // Act
      var result = await svc.Check(new CheckScope
      {
        Company = company,
        Rule = rule
      });

      // Assert
      Assert.That(result, Is.Not.Null);
      Assert.That(result.Match, Is.True);
    }

    [Test]
    public async Task Rule_Matches_SpecificCompany()
    {
      // Arrange
      var svc = RuleCheckService.NewRuleCheckService();
      var company = TestHelpers.CreateTestCompany();
      var rule = TestHelpers.CreateTestRule();

      // Create condition targeting the company
      var condition = TestHelpers.CreateTestCondition(ConditionType.Company);
      condition.ResourceIds = new List<string> { company.Id };
      rule.Conditions = new List<Condition> { condition };

      // Act
      var result = await svc.Check(new CheckScope
      {
        Company = company,
        Rule = rule
      });

      // Assert
      Assert.That(result, Is.Not.Null);
      Assert.That(result.Match, Is.True);
    }

    [Test]
    public async Task Rule_Matches_When_Metric_Within_Limit()
    {
      // Arrange
      var svc = RuleCheckService.NewRuleCheckService();
      var company = TestHelpers.CreateTestCompany();

      string eventSubtype = "test-event";
      var rule = TestHelpers.CreateTestRule();
      var condition = TestHelpers.CreateTestCondition(ConditionType.Metric);
      condition.EventSubtype = eventSubtype;
      condition.MetricValue = 10;
      condition.Operator = ComparableOperator.Lte;
      rule.Conditions = new List<Condition> { condition };

      var metric = TestHelpers.CreateTestMetric(company, eventSubtype, condition.MetricPeriod!.Value, 5);
      company.Metrics = new List<CompanyMetric> { metric };

      // Act
      var result = await svc.Check(new CheckScope
      {
        Company = company,
        Rule = rule
      });

      // Assert
      Assert.That(result, Is.Not.Null);
      Assert.That(result.Match, Is.True);
    }

    [Test]
    public async Task Rule_DoesNotMatch_When_Metric_Exceeds_Limit()
    {
      // Arrange
      var svc = RuleCheckService.NewRuleCheckService();
      var company = TestHelpers.CreateTestCompany();

      string eventSubtype = "test-event";
      var rule = TestHelpers.CreateTestRule();
      var condition = TestHelpers.CreateTestCondition(ConditionType.Metric);
      condition.EventSubtype = eventSubtype;
      condition.MetricValue = 5;
      condition.Operator = ComparableOperator.Lte;
      rule.Conditions = new List<Condition> { condition };

      var metric = TestHelpers.CreateTestMetric(company, eventSubtype, condition.MetricPeriod!.Value, 6); // Value exceeds limit
      company.Metrics = new List<CompanyMetric> { metric };

      // Act
      var result = await svc.Check(new CheckScope
      {
        Company = company,
        Rule = rule
      });

      // Assert
      Assert.That(result, Is.Not.Null);
      Assert.That(result.Match, Is.False);
    }

    [Test]
    public async Task Trait_Evaluation_MatchesWhenValueMatches()
    {
      // Arrange
      var svc = RuleCheckService.NewRuleCheckService();
      var company = TestHelpers.CreateTestCompany();
      var trait = TestHelpers.CreateTestTrait("test-value", null);
      company.Traits.Add(trait);

      var rule = TestHelpers.CreateTestRule();
      var condition = TestHelpers.CreateTestCondition(ConditionType.Trait);
      condition.TraitDefinition = trait.TraitDefinition;
      condition.TraitValue = "test-value";
      condition.Operator = ComparableOperator.Eq;
      rule.Conditions = new List<Condition> { condition };

      // Act
      var result = await svc.Check(new CheckScope
      {
        Company = company,
        Rule = rule
      });

      // Assert
      Assert.That(result, Is.Not.Null);
      Assert.That(result.Match, Is.True);
    }

    [Test]
    public async Task ConditionGroup_Matches_When_Any_Condition_Matches()
    {
      // Arrange
      var svc = RuleCheckService.NewRuleCheckService();
      var company = TestHelpers.CreateTestCompany();

      var rule = TestHelpers.CreateTestRule();
      var condition1 = TestHelpers.CreateTestCondition(ConditionType.Company);
      var condition2 = TestHelpers.CreateTestCondition(ConditionType.Company);
      condition2.ResourceIds = new List<string> { company.Id };

      var group = new ConditionGroup
      {
        Conditions = new List<Condition> { condition1, condition2 }
      };
      rule.ConditionGroups = new List<ConditionGroup> { group };

      // Act
      var result = await svc.Check(new CheckScope
      {
        Company = company,
        Rule = rule
      });

      // Assert
      Assert.That(result, Is.Not.Null);
      Assert.That(result.Match, Is.True);
    }

    [Test]
    public async Task ConditionGroup_DoesNotMatch_When_No_Conditions_Match()
    {
      // Arrange
      var svc = RuleCheckService.NewRuleCheckService();
      var company = TestHelpers.CreateTestCompany();

      var rule = TestHelpers.CreateTestRule();
      var condition1 = TestHelpers.CreateTestCondition(ConditionType.Company);
      var condition2 = TestHelpers.CreateTestCondition(ConditionType.Company);
      // No matching condition added to the group

      var group = new ConditionGroup
      {
        Conditions = new List<Condition> { condition1, condition2 }
      };
      rule.ConditionGroups = new List<ConditionGroup> { group };

      // Act
      var result = await svc.Check(new CheckScope
      {
        Company = company,
        Rule = rule
      });

      // Assert
      Assert.That(result, Is.Not.Null);
      Assert.That(result.Match, Is.False);
    }

    [Test]
    public async Task User_Trait_Evaluation()
    {
      // Arrange
      var svc = RuleCheckService.NewRuleCheckService();
      var user = TestHelpers.CreateTestUser();
      var traitDef = TestHelpers.CreateTestTraitDefinition(ComparableType.String, EntityType.User);
      var trait = TestHelpers.CreateTestTrait("user-trait-value", traitDef);
      user.Traits.Add(trait);

      var rule = TestHelpers.CreateTestRule();
      var condition = TestHelpers.CreateTestCondition(ConditionType.Trait);
      condition.TraitDefinition = traitDef;
      condition.TraitValue = "user-trait-value";
      condition.Operator = ComparableOperator.Eq;
      rule.Conditions = new List<Condition> { condition };

      // Act
      var result = await svc.Check(new CheckScope
      {
        User = user,
        Rule = rule
      });

      // Assert
      Assert.That(result, Is.Not.Null);
      Assert.That(result.Match, Is.True);
    }

    [Test]
    public async Task Multiple_Conditions_All_Must_Match()
    {
      // Arrange
      var svc = RuleCheckService.NewRuleCheckService();
      var company = TestHelpers.CreateTestCompany();
      var trait = TestHelpers.CreateTestTrait("test-value", null);
      company.Traits.Add(trait);

      var rule = TestHelpers.CreateTestRule();
      
      // Company condition
      var condition1 = TestHelpers.CreateTestCondition(ConditionType.Company);
      condition1.ResourceIds = new List<string> { company.Id };
      
      // Trait condition
      var condition2 = TestHelpers.CreateTestCondition(ConditionType.Trait);
      condition2.TraitDefinition = trait.TraitDefinition;
      condition2.TraitValue = "test-value";
      condition2.Operator = ComparableOperator.Eq;
      
      rule.Conditions = new List<Condition> { condition1, condition2 };

      // Act
      var result = await svc.Check(new CheckScope
      {
        Company = company,
        Rule = rule
      });

      // Assert
      Assert.That(result, Is.Not.Null);
      Assert.That(result.Match, Is.True);
    }

    [Test]
    public async Task Multiple_Conditions_Fail_If_Any_Does_Not_Match()
    {
      // Arrange
      var svc = RuleCheckService.NewRuleCheckService();
      var company = TestHelpers.CreateTestCompany();
      var trait = TestHelpers.CreateTestTrait("test-value", null);
      company.Traits.Add(trait);

      var rule = TestHelpers.CreateTestRule();
      
      // Company condition
      var condition1 = TestHelpers.CreateTestCondition(ConditionType.Company);
      condition1.ResourceIds = new List<string> { company.Id };
      
      // Trait condition that doesn't match
      var condition2 = TestHelpers.CreateTestCondition(ConditionType.Trait);
      condition2.TraitDefinition = trait.TraitDefinition;
      condition2.TraitValue = "different-value";
      condition2.Operator = ComparableOperator.Eq;
      
      rule.Conditions = new List<Condition> { condition1, condition2 };

      // Act
      var result = await svc.Check(new CheckScope
      {
        Company = company,
        Rule = rule
      });

      // Assert
      Assert.That(result, Is.Not.Null);
      Assert.That(result.Match, Is.False);
    }

    [Test]
    public async Task Rule_Matches_WithSufficientCreditBalance()
    {
      // Arrange
      var svc = RuleCheckService.NewRuleCheckService();
      
      // Create a company with a credit balance
      var company = TestHelpers.CreateTestCompany();
      var creditId = "test-credit-id";
      company.CreditBalances = new Dictionary<string, double>
      {
          { creditId, 100.0 } // Set a credit balance of 100.0
      };
      
      // Create a rule with a credit condition
      var rule = TestHelpers.CreateTestRule();
      rule.Conditions ??= new List<Condition>();
      var condition = TestHelpers.CreateTestCondition(ConditionType.Credit);
      condition.CreditId = creditId;
      condition.ConsumptionRate = 50.0; // Consumption rate less than the balance
      rule.Conditions.Add(condition);

      // Act
      var result = await svc.Check(new CheckScope
      {
        Company = company,
        Rule = rule
      });

      // Assert
      Assert.That(result, Is.Not.Null);
      Assert.That(result.Match, Is.True);
    }

    [Test]
    public async Task Rule_DoesNotMatch_WithInsufficientCreditBalance()
    {
      // Arrange
      var svc = RuleCheckService.NewRuleCheckService();
      
      // Create a company with a credit balance
      var company = TestHelpers.CreateTestCompany();
      var creditId = "test-credit-id";
      company.CreditBalances = new Dictionary<string, double>
      {
          { creditId, 20.0 } // Set a credit balance of 20.0
      };
      
      // Create a rule with a credit condition
      var rule = TestHelpers.CreateTestRule();
      rule.Conditions ??= new List<Condition>();
      var condition = TestHelpers.CreateTestCondition(ConditionType.Credit);
      condition.CreditId = creditId;
      condition.ConsumptionRate = 50.0; // Consumption rate more than the balance
      rule.Conditions.Add(condition);

      // Act
      var result = await svc.Check(new CheckScope
      {
        Company = company,
        Rule = rule
      });

      // Assert
      Assert.That(result, Is.Not.Null);
      Assert.That(result.Match, Is.False);
    }

    [Test]
    public async Task Rule_Matches_WithDefaultConsumptionRate()
    {
      // Arrange
      var svc = RuleCheckService.NewRuleCheckService();
      
      // Create a company with a credit balance
      var company = TestHelpers.CreateTestCompany();
      var creditId = "test-credit-id";
      company.CreditBalances = new Dictionary<string, double>
      {
          { creditId, 2.0 } // Set a credit balance of 2.0
      };
      
      // Create a rule with a credit condition
      var rule = TestHelpers.CreateTestRule();
      rule.Conditions ??= new List<Condition>();
      var condition = TestHelpers.CreateTestCondition(ConditionType.Credit);
      condition.CreditId = creditId;
      condition.ConsumptionRate = null; // Default consumption rate is 1.0
      rule.Conditions.Add(condition);

      // Act
      var result = await svc.Check(new CheckScope
      {
        Company = company,
        Rule = rule
      });

      // Assert
      Assert.That(result, Is.Not.Null);
      Assert.That(result.Match, Is.True);
    }

    [Test]
    public async Task Rule_DoesNotMatch_WithNonExistentCreditId()
    {
      // Arrange
      var svc = RuleCheckService.NewRuleCheckService();
      
      // Create a company with a credit balance
      var company = TestHelpers.CreateTestCompany();
      company.CreditBalances = new Dictionary<string, double>
      {
          { "existing-credit-id", 100.0 }
      };
      
      // Create a rule with a credit condition for a different credit ID
      var rule = TestHelpers.CreateTestRule();
      rule.Conditions ??= new List<Condition>();
      var condition = TestHelpers.CreateTestCondition(ConditionType.Credit);
      condition.CreditId = "non-existent-credit-id"; // Different from the one in company
      condition.ConsumptionRate = 1.0;
      rule.Conditions.Add(condition);

      // Act
      var result = await svc.Check(new CheckScope
      {
        Company = company,
        Rule = rule
      });

      // Assert
      Assert.That(result, Is.Not.Null);
      Assert.That(result.Match, Is.False);
    }
  }
}