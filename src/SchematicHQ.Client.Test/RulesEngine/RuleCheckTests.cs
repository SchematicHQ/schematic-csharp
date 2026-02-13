using NUnit.Framework;
using SchematicHQ.Client.RulesEngine;
using SchematicHQ.Client.RulesEngine.Utils;

namespace SchematicHQ.Client.Test.RulesEngine
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
      rule.RuleType = RulesengineRuleRuleType.GlobalOverride;

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
      rule.RuleType = RulesengineRuleRuleType.Default;

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
      var condition = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
      condition.ResourceIds = new List<string> { company.Id };
      rule.Conditions = new List<RulesengineCondition> { condition };

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
      var condition = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Metric);
      condition.EventSubtype = eventSubtype;
      condition.MetricValue = 10;
      condition.Operator = RulesengineConditionOperator.Lte;
      rule.Conditions = new List<RulesengineCondition> { condition };

      var metric = TestHelpers.CreateTestMetric(company, eventSubtype, condition.MetricPeriod!.Value, 5);
      company.Metrics = new List<RulesengineCompanyMetric> { metric };

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
      var condition = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Metric);
      condition.EventSubtype = eventSubtype;
      condition.MetricValue = 5;
      condition.Operator = RulesengineConditionOperator.Lte;
      rule.Conditions = new List<RulesengineCondition> { condition };

      var metric = TestHelpers.CreateTestMetric(company, eventSubtype, condition.MetricPeriod!.Value, 6); // Value exceeds limit
      company.Metrics = new List<RulesengineCompanyMetric> { metric };

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
      company.Traits = new List<RulesengineTrait> { trait };

      var rule = TestHelpers.CreateTestRule();
      var condition = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Trait);
      condition.TraitDefinition = trait.TraitDefinition;
      condition.TraitValue = "test-value";
      condition.Operator = RulesengineConditionOperator.Eq;
      rule.Conditions = new List<RulesengineCondition> { condition };

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
      var condition1 = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
      var condition2 = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
      condition2.ResourceIds = new List<string> { company.Id };

      var group = new RulesengineConditionGroup
      {
        Conditions = new List<RulesengineCondition> { condition1, condition2 }
      };
      rule.ConditionGroups = new List<RulesengineConditionGroup> { group };

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
      var condition1 = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
      var condition2 = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
      // No matching condition added to the group

      var group = new RulesengineConditionGroup
      {
        Conditions = new List<RulesengineCondition> { condition1, condition2 }
      };
      rule.ConditionGroups = new List<RulesengineConditionGroup> { group };

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
      var traitDef = TestHelpers.CreateTestTraitDefinition(RulesengineTraitDefinitionComparableType.String, RulesengineEntityType.User);
      var trait = TestHelpers.CreateTestTrait("user-trait-value", traitDef);
      user.Traits = new List<RulesengineTrait> { trait };

      var rule = TestHelpers.CreateTestRule();
      var condition = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Trait);
      condition.TraitDefinition = traitDef;
      condition.TraitValue = "user-trait-value";
      condition.Operator = RulesengineConditionOperator.Eq;
      rule.Conditions = new List<RulesengineCondition> { condition };

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
      company.Traits = new List<RulesengineTrait> { trait };

      var rule = TestHelpers.CreateTestRule();

      // Company condition
      var condition1 = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
      condition1.ResourceIds = new List<string> { company.Id };

      // Trait condition
      var condition2 = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Trait);
      condition2.TraitDefinition = trait.TraitDefinition;
      condition2.TraitValue = "test-value";
      condition2.Operator = RulesengineConditionOperator.Eq;

      rule.Conditions = new List<RulesengineCondition> { condition1, condition2 };

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
      company.Traits = new List<RulesengineTrait> { trait };

      var rule = TestHelpers.CreateTestRule();

      // Company condition
      var condition1 = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
      condition1.ResourceIds = new List<string> { company.Id };

      // Trait condition that doesn't match
      var condition2 = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Trait);
      condition2.TraitDefinition = trait.TraitDefinition;
      condition2.TraitValue = "different-value";
      condition2.Operator = RulesengineConditionOperator.Eq;

      rule.Conditions = new List<RulesengineCondition> { condition1, condition2 };

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
            var condition = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Credit);
      condition.CreditId = creditId;
      condition.ConsumptionRate = 50.0; // Consumption rate less than the balance
      rule.Conditions = new List<RulesengineCondition> { condition };

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
            var condition = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Credit);
      condition.CreditId = creditId;
      condition.ConsumptionRate = 50.0; // Consumption rate more than the balance
      rule.Conditions = new List<RulesengineCondition> { condition };

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
            var condition = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Credit);
      condition.CreditId = creditId;
      condition.ConsumptionRate = null; // Default consumption rate is 1.0
      rule.Conditions = new List<RulesengineCondition> { condition };

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
            var condition = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Credit);
      condition.CreditId = "non-existent-credit-id"; // Different from the one in company
      condition.ConsumptionRate = 1.0;
      rule.Conditions = new List<RulesengineCondition> { condition };

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
