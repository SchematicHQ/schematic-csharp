using SchematicHQ.Client.RulesEngine.Utils;
using SchematicHQ.Client.RulesEngine.Models;
using SchematicHQ.Client.RulesEngine;
using NUnit.Framework;

namespace SchematicHQ.Client.Test.RulesEngine
{
    [TestFixture]
    public class FlagCheckServiceTests
    {
        [Test]
        public async Task Returns_ErrorResult_WhenFlagIsNil()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();

            // Act
            var result = await FlagCheckService.CheckFlag(company, null, null);

            // Assert
            Assert.That(result.Reason, Is.EqualTo(FlagCheckService.ReasonFlagNotFound));
            Assert.That(result.Error, Is.EqualTo(Errors.ErrorFlagNotFound));
        }

        [Test]
        public async Task Returns_DefaultValue_WhenNoRulesMatch()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();
            flag.DefaultValue = true;

            // Act
            var result = await FlagCheckService.CheckFlag(company, null, flag);

            // Assert
            Assert.That(result.Reason, Is.EqualTo(FlagCheckService.ReasonNoRulesMatched));
            Assert.That(result.Value, Is.True);
            Assert.That(result.CompanyId, Is.EqualTo(company.Id));
        }

        [Test]
        public async Task GlobalOverride_TakesPrecedence()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();

            // Create a standard rule that matches
            var standardRule = TestHelpers.CreateTestRule();
            standardRule.Value = false;
            var standardCondition = TestHelpers.CreateTestCondition(ConditionType.Company);
            standardCondition.ResourceIds = new List<string> { company.Id };
            standardRule.Conditions = new List<Condition> { standardCondition };

            // Create a global override rule
            var overrideRule = TestHelpers.CreateTestRule();
            overrideRule.RuleType = RuleType.GlobalOverride;
            overrideRule.Value = true;

            flag.Rules.Add(standardRule);
            flag.Rules.Add(overrideRule);

            // Act
            var result = await FlagCheckService.CheckFlag(company, null, flag);

            // Assert
            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(overrideRule.Id));
        }

        [Test]
        public async Task Rules_Evaluated_In_Priority_Order()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();

            // Create two matching rules with different priorities
            var rule1 = TestHelpers.CreateTestRule();
            rule1.Priority = 2;
            rule1.Value = false;
            var condition1 = TestHelpers.CreateTestCondition(ConditionType.Company);
            condition1.ResourceIds = new List<string> { company.Id };
            rule1.Conditions = new List<Condition> { condition1 };

            var rule2 = TestHelpers.CreateTestRule();
            rule2.Priority = 1; // Lower priority number = higher priority
            rule2.Value = true;
            var condition2 = TestHelpers.CreateTestCondition(ConditionType.Company);
            condition2.ResourceIds = new List<string> { company.Id };
            rule2.Conditions = new List<Condition> { condition2 };

            flag.Rules = new List<Rule> { rule1, rule2 };

            // Act
            var result = await FlagCheckService.CheckFlag(company, null, flag);

            // Assert
            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(rule2.Id));
        }

        [Test]
        public async Task Condition_Group_Matches_When_Any_Condition_Matches()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();

            var rule = TestHelpers.CreateTestRule();
            rule.Value = true;

            // Create condition group with two conditions
            var condition1 = TestHelpers.CreateTestCondition(ConditionType.Company);
            condition1.ResourceIds = new List<string> { "non-matching-id" };

            var condition2 = TestHelpers.CreateTestCondition(ConditionType.Company);
            condition2.ResourceIds = new List<string> { company.Id };

            var group = new ConditionGroup
            {
                Conditions = new List<Condition> { condition1, condition2 }
            };

            rule.ConditionGroups = new List<ConditionGroup> { group };
            flag.Rules = new List<Rule> { rule };

            // Act
            var result = await FlagCheckService.CheckFlag(company, null, flag);

            // Assert
            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(rule.Id));
        }

        [Test]
        public async Task Sets_Usage_And_Allocation_For_Metric_Condition()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();

            // Create entitlement rule with metric condition
            string eventSubtype = "test-event";
            var rule = TestHelpers.CreateTestRule();
            rule.RuleType = RuleType.PlanEntitlement;
            rule.Value = true;

            var condition = TestHelpers.CreateTestCondition(ConditionType.Metric);
            condition.EventSubtype = eventSubtype;
            condition.MetricValue = 10;
            condition.Operator = ComparableOperator.Lte;

            rule.Conditions = new List<Condition> { condition };
            flag.Rules = new List<Rule> { rule };

            // Create company metric
            var metric = TestHelpers.CreateTestMetric(company, eventSubtype, condition.MetricPeriod!.Value, 5);
            company.Metrics = new List<CompanyMetric> { metric };

            // Act
            var result = await FlagCheckService.CheckFlag(company, null, flag);

            // Assert
            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(rule.Id));
            Assert.That(result.FeatureUsage, Is.Not.Null);
            Assert.That(result.FeatureUsage, Is.EqualTo(5));
            Assert.That(result.FeatureAllocation, Is.Not.Null);
            Assert.That(result.FeatureAllocation, Is.EqualTo(10));
        }

        [Test]
        public async Task Sets_Usage_And_Allocation_For_Trait_Condition()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();

            // Create trait
            var traitDef = TestHelpers.CreateTestTraitDefinition(ComparableType.Int, EntityType.Company);
            var trait = TestHelpers.CreateTestTrait("5", traitDef);
            company.Traits = new List<Trait> { trait };

            // Create entitlement rule with trait condition
            var rule = TestHelpers.CreateTestRule();
            rule.RuleType = RuleType.PlanEntitlement;
            rule.Value = true;

            var condition = TestHelpers.CreateTestCondition(ConditionType.Trait);
            condition.TraitDefinition = traitDef;
            condition.TraitValue = "10";
            condition.Operator = ComparableOperator.Lte;

            rule.Conditions = new List<Condition> { condition };
            flag.Rules = new List<Rule> { rule };

            // Act
            var result = await FlagCheckService.CheckFlag(company, null, flag);

            // Assert
            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(rule.Id));
            Assert.That(result.FeatureUsage, Is.Not.Null);
            Assert.That(result.FeatureUsage, Is.EqualTo(5));
            Assert.That(result.FeatureAllocation, Is.Not.Null);
            Assert.That(result.FeatureAllocation, Is.EqualTo(10));
        }


        [Test]
        public async Task Matches_User_Specific_Conditions()
        {
            // Arrange
            var user = TestHelpers.CreateTestUser();
            var flag = TestHelpers.CreateTestFlag();

            var rule = TestHelpers.CreateTestRule();
            rule.Value = true;
            var condition = TestHelpers.CreateTestCondition(ConditionType.User);
            condition.ResourceIds = new List<string> { user.Id };
            rule.Conditions = new List<Condition> { condition };

            flag.Rules = new List<Rule> { rule };

            // Act
            var result = await FlagCheckService.CheckFlag(null, user, flag);

            // Assert
            Assert.That(result.Value, Is.True);
            Assert.That(result.UserId, Is.EqualTo(user.Id));
            Assert.That(result.RuleId, Is.EqualTo(rule.Id));
        }

        [Test]
        public async Task Checks_User_Traits()
        {
            // Arrange
            var user = TestHelpers.CreateTestUser();
            var traitDef = TestHelpers.CreateTestTraitDefinition(ComparableType.String, EntityType.User);
            var trait = TestHelpers.CreateTestTrait("test-value", traitDef);
            user.Traits = new List<Trait> { trait };

            var flag = TestHelpers.CreateTestFlag();
            var rule = TestHelpers.CreateTestRule();
            rule.Value = true;

            var condition = TestHelpers.CreateTestCondition(ConditionType.Trait);
            condition.TraitDefinition = traitDef;
            condition.TraitValue = "test-value";
            condition.Operator = ComparableOperator.Eq;

            rule.Conditions = new List<Condition> { condition };
            flag.Rules = new List<Rule> { rule };

            // Act
            var result = await FlagCheckService.CheckFlag(null, user, flag);

            // Assert
            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(rule.Id));
        }

        [Test]
        public async Task Handles_Multiple_Condition_Types_And_Groups()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();
            var trait = TestHelpers.CreateTestTrait("test-value", null);
            company.Traits = new List<Trait> { trait };

            var flag = TestHelpers.CreateTestFlag();
            var rule = TestHelpers.CreateTestRule();
            rule.Value = true;

            // Add direct conditions
            var condition1 = TestHelpers.CreateTestCondition(ConditionType.Company);
            condition1.ResourceIds = new List<string> { company.Id };
            rule.Conditions?.Add(condition1);

            var condition2 = TestHelpers.CreateTestCondition(ConditionType.Trait);
            condition2.TraitDefinition = trait.TraitDefinition;
            condition2.TraitValue = "test-value";
            condition2.Operator = ComparableOperator.Eq;
            rule.Conditions?.Add(condition2);

            // Add condition group
            var group = new ConditionGroup
            {
                Conditions = new List<Condition>
                {
                    TestHelpers.CreateTestCondition(ConditionType.Plan),
                    TestHelpers.CreateTestCondition(ConditionType.BasePlan)
                }
            };
            group.Conditions[0].ResourceIds = new List<string> { company.PlanIds[0] };
            if (!string.IsNullOrEmpty(company.BasePlanId))
            {
                group.Conditions[1].ResourceIds = new List<string> { company.BasePlanId };
            }

            rule.ConditionGroups = new List<ConditionGroup> { group };
            flag.Rules = new List<Rule> { rule };

            // Act
            var result = await FlagCheckService.CheckFlag(company, null, flag);

            // Assert
            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(rule.Id));
        }

        [Test]
        public async Task Handles_Missing_Or_Invalid_Data_Gracefully()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();
            var rule = TestHelpers.CreateTestRule();

            // Add condition with null fields
            var condition = new Condition
            {
                Id = "",
                AccountId = "",
                EnvironmentId = "",
                ConditionType = ConditionType.Metric
            };
            rule.Conditions = new List<Condition> { condition };

            // Add empty condition group
            var group = new ConditionGroup();
            rule.ConditionGroups = new List<ConditionGroup> { group };

            flag.Rules = new List<Rule> { rule };

            // Act
            var result = await FlagCheckService.CheckFlag(company, null, flag);

            // Assert
            Assert.That(result.Value, Is.EqualTo(flag.DefaultValue));
            Assert.That(result.Reason, Is.EqualTo(FlagCheckService.ReasonNoRulesMatched));
        }
    }
}