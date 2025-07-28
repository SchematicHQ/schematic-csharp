using NUnit.Framework;
using SchematicHQ.Client.RulesEngine;
using SchematicHQ.Client.RulesEngine.Models;
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
  }
}