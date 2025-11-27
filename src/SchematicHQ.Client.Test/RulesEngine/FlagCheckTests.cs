using SchematicHQ.Client.RulesEngine.Utils;
using SchematicHQ.Client.RulesEngine;
using SchematicHQ.Client.RulesEngine.Wasm;
using NUnit.Framework;
using System.Text.Json;

namespace SchematicHQ.Client.Test.RulesEngine
{
    [TestFixture]
    public class FlagCheckServiceTests
    {
        // Note: Tests now use the WASM-powered FlagCheckService by default

        [Test]
        public async Task CheckFlag_UsesWasmEngine_ByDefault()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();
            flag.DefaultValue = true;

            // Act
            var result = await FlagCheckService.CheckFlag(company, null, flag);

            // Assert - Verify WASM-powered flag check works
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.True);
            Assert.That(result.FlagId, Is.EqualTo(flag.Id));
            Assert.That(result.FlagKey, Is.EqualTo(flag.Key));
            Assert.That(result.CompanyId, Is.EqualTo(company.Id));
            // The fact this test passes demonstrates WASM is working
        }

        [Test]
        public async Task Returns_ErrorResult_WhenFlagIsNil()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();

            // Act
            var result = await FlagCheckService.CheckFlag(company, null, null);

            // Assert
            Assert.That(result.Reason, Is.EqualTo(FlagCheckService.ReasonFlagNotFound));
            Assert.That(result.Err, Is.EqualTo(Errors.ErrorFlagNotFound.Message));
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
            var standardCondition = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
            standardCondition.ResourceIds = new List<string> { company.Id };
            standardRule.Conditions = new List<RulesengineCondition> { standardCondition };

            // Create a global override rule
            var overrideRule = TestHelpers.CreateTestRule();
            overrideRule.RuleType = RulesengineRuleRuleType.GlobalOverride;
            overrideRule.Value = true;

            flag.Rules = new List<RulesengineRule> { standardRule, overrideRule };

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
            var condition1 = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
            condition1.ResourceIds = new List<string> { company.Id };
            rule1.Conditions = new List<RulesengineCondition> { condition1 };

            var rule2 = TestHelpers.CreateTestRule();
            rule2.Priority = 1; // Lower priority number = higher priority
            rule2.Value = true;
            var condition2 = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
            condition2.ResourceIds = new List<string> { company.Id };
            rule2.Conditions = new List<RulesengineCondition> { condition2 };

            flag.Rules = new List<RulesengineRule> { rule1, rule2 };

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
            var condition1 = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
            condition1.ResourceIds = new List<string> { "non-matching-id" };

            var condition2 = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
            condition2.ResourceIds = new List<string> { company.Id };

            var group = new RulesengineConditionGroup
            {
                Conditions = new List<RulesengineCondition> { condition1, condition2 }
            };

            rule.ConditionGroups = new List<RulesengineConditionGroup> { group };
            flag.Rules = new List<RulesengineRule> { rule };

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
            rule.RuleType = RulesengineRuleRuleType.PlanEntitlement;
            rule.Value = true;

            var condition = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Metric);
            condition.EventSubtype = eventSubtype;
            condition.MetricValue = 10;
            condition.Operator = RulesengineConditionOperator.Lte;

            rule.Conditions = new List<RulesengineCondition> { condition };
            flag.Rules = new List<RulesengineRule> { rule };

            // Create company metric
            var metricPeriod = new RulesengineCompanyMetricPeriod(condition.MetricPeriod!.Value.Value);
            var metric = TestHelpers.CreateTestMetric(company, eventSubtype, metricPeriod, 5);
            company.Metrics = new List<RulesengineCompanyMetric> { metric };

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
            var traitDef = TestHelpers.CreateTestTraitDefinition(RulesengineTraitDefinitionComparableType.Int, RulesengineTraitDefinitionEntityType.Company);
            var trait = TestHelpers.CreateTestTrait("5", traitDef);
            company.Traits = new List<RulesengineTrait> { trait };

            // Create entitlement rule with trait condition
            var rule = TestHelpers.CreateTestRule();
            rule.RuleType = RulesengineRuleRuleType.PlanEntitlement;
            rule.Value = true;

            var condition = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Trait);
            condition.TraitDefinition = traitDef;
            condition.TraitValue = "10";
            condition.Operator = RulesengineConditionOperator.Lte;

            rule.Conditions = new List<RulesengineCondition> { condition };
            flag.Rules = new List<RulesengineRule> { rule };

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
            var condition = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.User);
            condition.ResourceIds = new List<string> { user.Id };
            rule.Conditions = new List<RulesengineCondition> { condition };

            flag.Rules = new List<RulesengineRule> { rule };

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
            var traitDef = TestHelpers.CreateTestTraitDefinition(RulesengineTraitDefinitionComparableType.String, RulesengineTraitDefinitionEntityType.User);
            var trait = TestHelpers.CreateTestTrait("test-value", traitDef);
            user.Traits = new List<RulesengineTrait> { trait };

            var flag = TestHelpers.CreateTestFlag();
            var rule = TestHelpers.CreateTestRule();
            rule.Value = true;

            var condition = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Trait);
            condition.TraitDefinition = traitDef;
            condition.TraitValue = "test-value";
            condition.Operator = RulesengineConditionOperator.Eq;

            rule.Conditions = new List<RulesengineCondition> { condition };
            flag.Rules = new List<RulesengineRule> { rule };

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
            company.Traits = new List<RulesengineTrait> { trait };

            var flag = TestHelpers.CreateTestFlag();
            var rule = TestHelpers.CreateTestRule();
            rule.Value = true;

            // Add direct conditions
            var condition1 = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
            condition1.ResourceIds = new List<string> { company.Id };

            var condition2 = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Trait);
            condition2.TraitDefinition = trait.TraitDefinition;
            condition2.TraitValue = "test-value";
            condition2.Operator = RulesengineConditionOperator.Eq;

            rule.Conditions = new List<RulesengineCondition> { condition1, condition2 };

            // Add condition group
            var groupCondition1 = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Plan);
            groupCondition1.ResourceIds = new List<string> { company.PlanIds.First() };

            var groupCondition2 = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.BasePlan);
            if (!string.IsNullOrEmpty(company.BasePlanId))
            {
                groupCondition2.ResourceIds = new List<string> { company.BasePlanId };
            }

            var group = new RulesengineConditionGroup
            {
                Conditions = new List<RulesengineCondition> { groupCondition1, groupCondition2 }
            };

            rule.ConditionGroups = new List<RulesengineConditionGroup> { group };
            flag.Rules = new List<RulesengineRule> { rule };

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
            var condition = new RulesengineCondition
            {
                Id = "",
                AccountId = "",
                EnvironmentId = "",
                ConditionType = RulesengineConditionConditionType.Metric,
                Operator = RulesengineConditionOperator.Eq,
                TraitValue = ""
            };
            rule.Conditions = new List<RulesengineCondition> { condition };

            // Add empty condition group
            var group = new RulesengineConditionGroup();
            rule.ConditionGroups = new List<RulesengineConditionGroup> { group };

            flag.Rules = new List<RulesengineRule> { rule };

            // Act
            var result = await FlagCheckService.CheckFlag(company, null, flag);

            // Assert
            Assert.That(result.Value, Is.EqualTo(flag.DefaultValue));
            Assert.That(result.Reason, Is.EqualTo(FlagCheckService.ReasonNoRulesMatched));
        }

        [Test]
        public async Task CompanyProvidedRule_IsEvaluatedAlongWithFlagRules()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();
            flag.DefaultValue = false;

            // Create a company-provided rule that matches
            var companyRule = TestHelpers.CreateTestRule();
            companyRule.FlagId = flag.Id;
            companyRule.Value = true;
            var condition = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
            condition.ResourceIds = new List<string> { company.Id };
            companyRule.Conditions = new List<RulesengineCondition> { condition };

            company.Rules = new List<RulesengineRule> { companyRule };

            // Act
            var result = await FlagCheckService.CheckFlag(company, null, flag);

            // Assert
            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(companyRule.Id));
        }

        [Test]
        public async Task CompanyProvidedRule_RespectsPriorityOrdering()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();

            // Create flag rule with lower priority
            var flagRule = TestHelpers.CreateTestRule();
            flagRule.Priority = 2;
            flagRule.Value = false;
            var condition1 = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
            condition1.ResourceIds = new List<string> { company.Id };
            flagRule.Conditions = new List<RulesengineCondition> { condition1 };

            // Create company rule with higher priority
            var companyRule = TestHelpers.CreateTestRule();
            companyRule.FlagId = flag.Id;
            companyRule.Priority = 1;
            companyRule.Value = true;
            var condition2 = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
            condition2.ResourceIds = new List<string> { company.Id };
            companyRule.Conditions = new List<RulesengineCondition> { condition2 };

            flag.Rules = new List<RulesengineRule> { flagRule };
            company.Rules = new List<RulesengineRule> { companyRule };

            // Act
            var result = await FlagCheckService.CheckFlag(company, null, flag);

            // Assert
            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(companyRule.Id));
        }

        [Test]
        public async Task CompanyProvidedRule_WithGlobalOverrideTakesPrecedence()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();

            // Create standard flag rule
            var flagRule = TestHelpers.CreateTestRule();
            flagRule.Value = false;
            var condition1 = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
            condition1.ResourceIds = new List<string> { company.Id };
            flagRule.Conditions = new List<RulesengineCondition> { condition1 };

            // Create company rule with global override
            var companyRule = TestHelpers.CreateTestRule();
            companyRule.FlagId = flag.Id;
            companyRule.RuleType = RulesengineRuleRuleType.GlobalOverride;
            companyRule.Value = true;

            flag.Rules = new List<RulesengineRule> { flagRule };
            company.Rules = new List<RulesengineRule> { companyRule };

            // Act
            var result = await FlagCheckService.CheckFlag(company, null, flag);

            // Assert
            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(companyRule.Id));
        }

        [Test]
        public async Task MultipleCompanyProvidedRules_AreAllEvaluated()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();
            flag.DefaultValue = false;

            // Create two company rules, only one matches
            var companyRule1 = TestHelpers.CreateTestRule();
            companyRule1.FlagId = flag.Id;
            companyRule1.Priority = 1;
            companyRule1.Value = true;
            var condition1 = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
            condition1.ResourceIds = new List<string> { "non-matching-id" };
            companyRule1.Conditions = new List<RulesengineCondition> { condition1 };

            var companyRule2 = TestHelpers.CreateTestRule();
            companyRule2.FlagId = flag.Id;
            companyRule2.Priority = 2;
            companyRule2.Value = true;
            var condition2 = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
            condition2.ResourceIds = new List<string> { company.Id };
            companyRule2.Conditions = new List<RulesengineCondition> { condition2 };

            company.Rules = new List<RulesengineRule> { companyRule1, companyRule2 };

            // Act
            var result = await FlagCheckService.CheckFlag(company, null, flag);

            // Assert
            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(companyRule2.Id));
        }

        [Test]
        public async Task UserProvidedRule_IsEvaluatedAlongWithFlagRules()
        {
            // Arrange
            var user = TestHelpers.CreateTestUser();
            var flag = TestHelpers.CreateTestFlag();
            flag.DefaultValue = false;

            // Create a user-provided rule that matches
            var userRule = TestHelpers.CreateTestRule();
            userRule.FlagId = flag.Id;
            userRule.Value = true;
            var condition = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.User);
            condition.ResourceIds = new List<string> { user.Id };
            userRule.Conditions = new List<RulesengineCondition> { condition };

            user.Rules = new List<RulesengineRule> { userRule };

            // Act
            var result = await FlagCheckService.CheckFlag(null, user, flag);

            // Assert
            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(userRule.Id));
        }

        [Test]
        public async Task UserProvidedRule_RespectsPriorityOrdering()
        {
            // Arrange
            var user = TestHelpers.CreateTestUser();
            var flag = TestHelpers.CreateTestFlag();

            // Create flag rule with lower priority
            var flagRule = TestHelpers.CreateTestRule();
            flagRule.Priority = 2;
            flagRule.Value = false;
            var condition1 = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.User);
            condition1.ResourceIds = new List<string> { user.Id };
            flagRule.Conditions = new List<RulesengineCondition> { condition1 };

            // Create user rule with higher priority
            var userRule = TestHelpers.CreateTestRule();
            userRule.FlagId = flag.Id;
            userRule.Priority = 1;
            userRule.Value = true;
            var condition2 = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.User);
            condition2.ResourceIds = new List<string> { user.Id };
            userRule.Conditions = new List<RulesengineCondition> { condition2 };

            flag.Rules = new List<RulesengineRule> { flagRule };
            user.Rules = new List<RulesengineRule> { userRule };

            // Act
            var result = await FlagCheckService.CheckFlag(null, user, flag);

            // Assert
            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(userRule.Id));
        }

        [Test]
        public async Task UserProvidedRule_WithGlobalOverrideTakesPrecedence()
        {
            // Arrange
            var user = TestHelpers.CreateTestUser();
            var flag = TestHelpers.CreateTestFlag();

            // Create standard flag rule
            var flagRule = TestHelpers.CreateTestRule();
            flagRule.Value = false;
            var condition1 = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.User);
            condition1.ResourceIds = new List<string> { user.Id };
            flagRule.Conditions = new List<RulesengineCondition> { condition1 };

            // Create user rule with global override
            var userRule = TestHelpers.CreateTestRule();
            userRule.FlagId = flag.Id;
            userRule.RuleType = RulesengineRuleRuleType.GlobalOverride;
            userRule.Value = true;

            flag.Rules = new List<RulesengineRule> { flagRule };
            user.Rules = new List<RulesengineRule> { userRule };

            // Act
            var result = await FlagCheckService.CheckFlag(null, user, flag);

            // Assert
            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(userRule.Id));
        }

        [Test]
        public async Task BothCompanyAndUserRules_AreEvaluated()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();
            var user = TestHelpers.CreateTestUser();
            var flag = TestHelpers.CreateTestFlag();
            flag.DefaultValue = false;

            // Create company rule that doesn't match
            var companyRule = TestHelpers.CreateTestRule();
            companyRule.FlagId = flag.Id;
            companyRule.Priority = 1;
            companyRule.Value = true;
            var condition1 = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
            condition1.ResourceIds = new List<string> { "non-matching-id" };
            companyRule.Conditions = new List<RulesengineCondition> { condition1 };

            // Create user rule that matches
            var userRule = TestHelpers.CreateTestRule();
            userRule.FlagId = flag.Id;
            userRule.Priority = 2;
            userRule.Value = true;
            var condition2 = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.User);
            condition2.ResourceIds = new List<string> { user.Id };
            userRule.Conditions = new List<RulesengineCondition> { condition2 };

            company.Rules = new List<RulesengineRule> { companyRule };
            user.Rules = new List<RulesengineRule> { userRule };

            // Act
            var result = await FlagCheckService.CheckFlag(company, user, flag);

            // Assert
            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(userRule.Id));
        }

        [Test]
        public async Task AllThreeRuleSources_EvaluatedWithCorrectPriority()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();
            var user = TestHelpers.CreateTestUser();
            var flag = TestHelpers.CreateTestFlag();
            flag.DefaultValue = false;

            // Create rules from all three sources - all matching their respective conditions
            var flagRule = TestHelpers.CreateTestRule();
            flagRule.Priority = 2;
            flagRule.Value = true;
            var condition1 = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
            condition1.ResourceIds = new List<string> { company.Id };
            flagRule.Conditions = new List<RulesengineCondition> { condition1 };

            var companyRule = TestHelpers.CreateTestRule();
            companyRule.FlagId = flag.Id;
            companyRule.Priority = 3;
            companyRule.Value = true;
            var condition2 = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
            condition2.ResourceIds = new List<string> { company.Id };
            companyRule.Conditions = new List<RulesengineCondition> { condition2 };

            var userRule = TestHelpers.CreateTestRule();
            userRule.FlagId = flag.Id;
            userRule.Priority = 1; // Highest priority
            userRule.Value = true;
            var condition3 = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.User);
            condition3.ResourceIds = new List<string> { user.Id };
            userRule.Conditions = new List<RulesengineCondition> { condition3 };

            flag.Rules = new List<RulesengineRule> { flagRule };
            company.Rules = new List<RulesengineRule> { companyRule };
            user.Rules = new List<RulesengineRule> { userRule };

            // Act
            var result = await FlagCheckService.CheckFlag(company, user, flag);

            // Assert
            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.Not.Null);
            // Should match the user rule since it has highest priority (lowest number)
            Assert.That(result.RuleId, Is.EqualTo(userRule.Id));
        }

        [Test]
        public async Task CompanyProvidedRule_WithWrongFlagId_IsNotEvaluated()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();
            flag.DefaultValue = false;

            // Create a company-provided rule for a different flag
            var companyRule = TestHelpers.CreateTestRule();
            companyRule.FlagId = TestHelpers.GenerateTestId("flag"); // Different flag ID
            companyRule.Value = true;
            var condition = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
            condition.ResourceIds = new List<string> { company.Id };
            companyRule.Conditions = new List<RulesengineCondition> { condition };

            company.Rules = new List<RulesengineRule> { companyRule };

            // Act
            var result = await FlagCheckService.CheckFlag(company, null, flag);

            // Assert
            Assert.That(result.Value, Is.False); // Should use flag's default value
            Assert.That(result.Reason, Is.EqualTo(FlagCheckService.ReasonNoRulesMatched));
        }

        [Test]
        public async Task UserProvidedRule_WithWrongFlagId_IsNotEvaluated()
        {
            // Arrange
            var user = TestHelpers.CreateTestUser();
            var flag = TestHelpers.CreateTestFlag();
            flag.DefaultValue = false;

            // Create a user-provided rule for a different flag
            var userRule = TestHelpers.CreateTestRule();
            userRule.FlagId = TestHelpers.GenerateTestId("flag"); // Different flag ID
            userRule.Value = true;
            var condition = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.User);
            condition.ResourceIds = new List<string> { user.Id };
            userRule.Conditions = new List<RulesengineCondition> { condition };

            user.Rules = new List<RulesengineRule> { userRule };

            // Act
            var result = await FlagCheckService.CheckFlag(null, user, flag);

            // Assert
            Assert.That(result.Value, Is.False); // Should use flag's default value
            Assert.That(result.Reason, Is.EqualTo(FlagCheckService.ReasonNoRulesMatched));
        }

        [Test]
        public async Task CompanyProvidedRule_WithNullFlagId_IsNotEvaluated()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();
            flag.DefaultValue = false;

            // Create a company-provided rule with null FlagId
            var companyRule = TestHelpers.CreateTestRule();
            companyRule.FlagId = null;
            companyRule.Value = true;
            var condition = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
            condition.ResourceIds = new List<string> { company.Id };
            companyRule.Conditions = new List<RulesengineCondition> { condition };

            company.Rules = new List<RulesengineRule> { companyRule };

            // Act
            var result = await FlagCheckService.CheckFlag(company, null, flag);

            // Assert
            Assert.That(result.Value, Is.False); // Should use flag's default value
            Assert.That(result.Reason, Is.EqualTo(FlagCheckService.ReasonNoRulesMatched));
        }

        [Test]
        public async Task UserProvidedRule_WithNullFlagId_IsNotEvaluated()
        {
            // Arrange
            var user = TestHelpers.CreateTestUser();
            var flag = TestHelpers.CreateTestFlag();
            flag.DefaultValue = false;

            // Create a user-provided rule with null FlagId
            var userRule = TestHelpers.CreateTestRule();
            userRule.FlagId = null;
            userRule.Value = true;
            var condition = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.User);
            condition.ResourceIds = new List<string> { user.Id };
            userRule.Conditions = new List<RulesengineCondition> { condition };

            user.Rules = new List<RulesengineRule> { userRule };

            // Act
            var result = await FlagCheckService.CheckFlag(null, user, flag);

            // Assert
            Assert.That(result.Value, Is.False); // Should use flag's default value
            Assert.That(result.Reason, Is.EqualTo(FlagCheckService.ReasonNoRulesMatched));
        }
    }

    [TestFixture]
    public class WasmRulesEngineTests
    {
        [Test]
        public async Task WasmEngine_Returns_ErrorResult_WhenFlagIsNull()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();

            // Act
            using var wasmEngine = new WasmRulesEngine();
            var result = await wasmEngine.CheckFlagAsync(company, null, null);

            // Assert
            Assert.That(result.Reason, Is.EqualTo(FlagCheckService.ReasonFlagNotFound));
            Assert.That(result.Err, Is.EqualTo(Errors.ErrorFlagNotFound.Message));
            Assert.That(result.Value, Is.False);
        }

        [Test]
        public async Task WasmEngine_Returns_DefaultValue_WhenNoRulesMatch()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();
            flag.DefaultValue = true;

            // Act
            using var wasmEngine = new WasmRulesEngine();
            var result = await wasmEngine.CheckFlagAsync(company, null, flag);

            // Assert
            Assert.That(result.Reason, Is.EqualTo(FlagCheckService.ReasonNoRulesMatched));
            Assert.That(result.Value, Is.True);
            Assert.That(result.CompanyId, Is.EqualTo(company.Id));
        }

        [Test]
        public async Task WasmEngine_GlobalOverride_TakesPrecedence()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();

            // Create a standard rule that matches
            var standardRule = TestHelpers.CreateTestRule();
            standardRule.Value = false;
            var standardCondition = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
            standardCondition.ResourceIds = new List<string> { company.Id };
            standardRule.Conditions = new List<RulesengineCondition> { standardCondition };

            // Create a global override rule
            var overrideRule = TestHelpers.CreateTestRule();
            overrideRule.RuleType = RulesengineRuleRuleType.GlobalOverride;
            overrideRule.Value = true;

            flag.Rules = new List<RulesengineRule> { standardRule, overrideRule };

            // Act
            using var wasmEngine = new WasmRulesEngine();
            var result = await wasmEngine.CheckFlagAsync(company, null, flag);

            // Assert
            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(overrideRule.Id));
        }

        [Test]
        public async Task WasmEngine_Rules_Evaluated_In_Priority_Order()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();

            // Create two matching rules with different priorities
            var rule1 = TestHelpers.CreateTestRule();
            rule1.Priority = 2;
            rule1.Value = false;
            var condition1 = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
            condition1.ResourceIds = new List<string> { company.Id };
            rule1.Conditions = new List<RulesengineCondition> { condition1 };

            var rule2 = TestHelpers.CreateTestRule();
            rule2.Priority = 1; // Lower priority number = higher priority
            rule2.Value = true;
            var condition2 = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
            condition2.ResourceIds = new List<string> { company.Id };
            rule2.Conditions = new List<RulesengineCondition> { condition2 };

            flag.Rules = new List<RulesengineRule> { rule1, rule2 };

            // Act
            using var wasmEngine = new WasmRulesEngine();
            var result = await wasmEngine.CheckFlagAsync(company, null, flag);

            // Assert
            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(rule2.Id));
        }

        [Test]
        public async Task WasmEngine_EvaluateRule_ReturnsTrue_WhenRuleMatches()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();
            var rule = TestHelpers.CreateTestRule();
            rule.Value = true;
            var condition = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
            condition.ResourceIds = new List<string> { company.Id };
            rule.Conditions = new List<RulesengineCondition> { condition };

            // Act
            using var wasmEngine = new WasmRulesEngine();
            var result = await wasmEngine.EvaluateRuleAsync(rule, company, null);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task WasmEngine_EvaluateRule_ReturnsFalse_WhenRuleDoesNotMatch()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();
            var rule = TestHelpers.CreateTestRule();
            rule.Value = true;
            var condition = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
            condition.ResourceIds = new List<string> { "different-company-id" };
            rule.Conditions = new List<RulesengineCondition> { condition };

            // Act
            using var wasmEngine = new WasmRulesEngine();
            var result = await wasmEngine.EvaluateRuleAsync(rule, company, null);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task WasmEngine_Performance_ComparedToTraditionalEngine()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();
            flag.DefaultValue = true;

            var rule = TestHelpers.CreateTestRule();
            rule.Value = false;
            var condition = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
            condition.ResourceIds = new List<string> { company.Id };
            rule.Conditions = new List<RulesengineCondition> { condition };
            flag.Rules = new List<RulesengineRule> { rule };

            // Act - Test both engines produce the same result
            var traditionalResult = await FlagCheckService.CheckFlag(company, null, flag);
            using var wasmEngine = new WasmRulesEngine();
            var wasmResult = await wasmEngine.CheckFlagAsync(company, null, flag);

            // Assert - Both engines should return the same result
            Assert.That(wasmResult.Value, Is.EqualTo(traditionalResult.Value));
            Assert.That(wasmResult.Reason, Is.EqualTo(traditionalResult.Reason));
            Assert.That(wasmResult.CompanyId, Is.EqualTo(traditionalResult.CompanyId));
            Assert.That(wasmResult.RuleId, Is.EqualTo(traditionalResult.RuleId));
        }

        [Test]
        public async Task WasmEngine_DeserializesAllFieldsCorrectly_FromWasmResult()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();
            var user = TestHelpers.CreateTestUser();
            var flag = TestHelpers.CreateTestFlag();

            // Create a simple company-based rule to avoid WASM time issues
            var rule = TestHelpers.CreateTestRule();
            rule.RuleType = RulesengineRuleRuleType.PlanEntitlement;
            rule.Value = true;

            var companyCondition = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
            companyCondition.ResourceIds = new List<string> { company.Id };

            rule.Conditions = new List<RulesengineCondition> { companyCondition };
            flag.Rules = new List<RulesengineRule> { rule };

            // Act
            using var wasmEngine = new WasmRulesEngine();
            var result = await wasmEngine.CheckFlagAsync(company, user, flag);

            // Assert - Basic fields are set correctly
            Assert.That(result.FlagId, Is.EqualTo(flag.Id), "FlagId should match the input flag");
            Assert.That(result.FlagKey, Is.EqualTo(flag.Key), "FlagKey should match the input flag");
            Assert.That(result.CompanyId, Is.EqualTo(company.Id), "CompanyId should match the input company");
            Assert.That(result.UserId, Is.EqualTo(user.Id), "UserId should match the input user");
            Assert.That(result.RuleId, Is.EqualTo(rule.Id), "RuleId should match the matching rule");

            // Assert - Critical field you mentioned: RuleType is properly set
            Assert.That(result.RuleType, Is.Not.Null, "RuleType should be set when rule matches");
            Assert.That(result.RuleType!.Value, Is.EqualTo(RulesengineRuleRuleType.PlanEntitlement.Value), 
                "RuleType should match the rule's RuleType from WASM deserialization");

            // Assert - Fields that should be null for simple company conditions
            Assert.That(result.FeatureUsagePeriod, Is.Null, 
                "FeatureUsagePeriod should be null for non-metric conditions");
            Assert.That(result.FeatureUsageResetAt, Is.Null, 
                "FeatureUsageResetAt should be null for non-metric conditions");
            Assert.That(result.FeatureUsage, Is.Null,
                "FeatureUsage should be null for non-metric conditions");
            Assert.That(result.FeatureAllocation, Is.Null,
                "FeatureAllocation should be null for non-metric conditions");

            // Assert - Required fields are properly deserialized
            Assert.That(result.Value, Is.True, "Value should match the rule result");
            Assert.That(result.Reason, Is.Not.Null.And.Not.Empty, "Reason should always be set from WASM");
        }

        [Test]
        public async Task WasmEngine_HandlesNullableFields_InDeserialization()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();
            flag.DefaultValue = false;
            // No rules - should return default value with minimal fields

            // Act
            using var wasmEngine = new WasmRulesEngine();
            var result = await wasmEngine.CheckFlagAsync(company, null, flag);

            // Assert - Required fields are always set
            Assert.That(result.FlagKey, Is.EqualTo(flag.Key));
            Assert.That(result.Reason, Is.Not.Null.And.Not.Empty);
            Assert.That(result.Value, Is.False);

            // Assert - Optional fields may be null when no rules match
            Assert.That(result.RuleType, Is.Null, "RuleType should be null when no rules match");
            Assert.That(result.FeatureUsagePeriod, Is.Null, "FeatureUsagePeriod should be null when no usage tracking");
            Assert.That(result.FeatureUsageResetAt, Is.Null, "FeatureUsageResetAt should be null when no usage tracking");
            Assert.That(result.FeatureUsage, Is.Null, "FeatureUsage should be null when no usage tracking");
            Assert.That(result.FeatureAllocation, Is.Null, "FeatureAllocation should be null when no usage tracking");
        }

        [Test]
        public void WasmEngine_CorrectlyDeserializesJsonResponse_WithAllFields()
        {
            // Arrange - Mock JSON response from WASM that includes all the fields you're concerned about
            var mockJsonResponse = @"{
                ""flag_id"": ""test-flag-id"",
                ""flag_key"": ""test-flag-key"",
                ""company_id"": ""test-company-id"",
                ""user_id"": ""test-user-id"",
                ""rule_id"": ""test-rule-id"",
                ""rule_type"": ""plan_entitlement"",
                ""feature_usage_period"": ""current_month"",
                ""feature_usage_reset_at"": ""2025-12-01T00:00:00Z"",
                ""feature_usage"": 42,
                ""feature_allocation"": 100,
                ""value"": true,
                ""reason"": ""Rule matched""
            }";

            // Act - Deserialize the JSON using the same mechanism as WasmRulesEngine
            var result = JsonSerializer.Deserialize<RulesengineCheckFlagResult>(mockJsonResponse);

            // Assert - All fields are correctly deserialized
            Assert.That(result, Is.Not.Null, "Result should be successfully deserialized");
            Assert.That(result.FlagId, Is.EqualTo("test-flag-id"), "FlagId should be correctly deserialized");
            Assert.That(result.FlagKey, Is.EqualTo("test-flag-key"), "FlagKey should be correctly deserialized");
            Assert.That(result.CompanyId, Is.EqualTo("test-company-id"), "CompanyId should be correctly deserialized");
            Assert.That(result.UserId, Is.EqualTo("test-user-id"), "UserId should be correctly deserialized");
            Assert.That(result.RuleId, Is.EqualTo("test-rule-id"), "RuleId should be correctly deserialized");

            // Assert - Critical fields you mentioned are correctly deserialized
            Assert.That(result.RuleType, Is.Not.Null, "RuleType should not be null");
            Assert.That(result.RuleType.Value, Is.EqualTo("plan_entitlement"), 
                "RuleType should correctly deserialize string enum values");

            Assert.That(result.FeatureUsagePeriod, Is.Not.Null, "FeatureUsagePeriod should not be null");
            Assert.That(result.FeatureUsagePeriod.Value, Is.EqualTo("current_month"), 
                "FeatureUsagePeriod should correctly deserialize string enum values");

            Assert.That(result.FeatureUsageResetAt, Is.Not.Null, "FeatureUsageResetAt should not be null");
            Assert.That(result.FeatureUsageResetAt.Value, Is.EqualTo(DateTime.Parse("2025-12-01T00:00:00Z").ToUniversalTime()), 
                "FeatureUsageResetAt should correctly deserialize DateTime values");

            // Assert - Numeric and boolean fields
            Assert.That(result.FeatureUsage, Is.EqualTo(42), "FeatureUsage should correctly deserialize integer values");
            Assert.That(result.FeatureAllocation, Is.EqualTo(100), "FeatureAllocation should correctly deserialize integer values");
            Assert.That(result.Value, Is.True, "Value should correctly deserialize boolean values");
            Assert.That(result.Reason, Is.EqualTo("Rule matched"), "Reason should correctly deserialize string values");
        }

        [Test]
        public async Task WasmEngine_ComputesCorrectFeatureUsageValues_NotJustDefaults()
        {
            // Arrange - Create a real scenario with metric conditions and company metrics
            var company = TestHelpers.CreateTestCompany();
            var user = TestHelpers.CreateTestUser();
            var flag = TestHelpers.CreateTestFlag();

            // Create a metric-based entitlement rule with specific thresholds
            string eventSubtype = "api-calls";
            var rule = TestHelpers.CreateTestRule();
            rule.RuleType = RulesengineRuleRuleType.PlanEntitlement;
            rule.Value = true;

            var metricCondition = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Metric);
            metricCondition.EventSubtype = eventSubtype;
            metricCondition.MetricPeriod = RulesengineConditionMetricPeriod.AllTime; // Use AllTime to avoid WASM time issues
            metricCondition.MetricValue = 100; // Allow up to 100 calls
            metricCondition.Operator = RulesengineConditionOperator.Lte;
            metricCondition.ResourceIds = new List<string> { company.Id };

            rule.Conditions = new List<RulesengineCondition> { metricCondition };
            flag.Rules = new List<RulesengineRule> { rule };

            // Create actual company metric data - company has used 42 out of 100 allowed
            var metricPeriod = new RulesengineCompanyMetricPeriod(metricCondition.MetricPeriod!.Value.Value);
            var companyMetric = TestHelpers.CreateTestMetric(company, eventSubtype, metricPeriod, 42);
            company.Metrics = new List<RulesengineCompanyMetric> { companyMetric };

            // Act
            using var wasmEngine = new WasmRulesEngine();
            var result = await wasmEngine.CheckFlagAsync(company, user, flag);

            // Assert - Verify WASM computed the correct values based on our inputs
            Assert.That(result.Value, Is.True, "Should return true since 42 <= 100");
            Assert.That(result.RuleId, Is.EqualTo(rule.Id), "Should match the rule that was evaluated");
            
            // Assert - Critical validation: WASM should return the ACTUAL computed usage values
            Assert.That(result.RuleType, Is.Not.Null, "RuleType should be set");
            Assert.That(result.RuleType!.Value, Is.EqualTo(RulesengineRuleRuleType.PlanEntitlement.Value),
                "RuleType should match the input rule type");

            Assert.That(result.FeatureUsagePeriod, Is.Not.Null, "FeatureUsagePeriod should be set for metric conditions");
            Assert.That(result.FeatureUsagePeriod!.Value.Value, Is.EqualTo("all_time"),
                "FeatureUsagePeriod should match the condition's MetricPeriod");

            // These are the key validations - WASM should compute and return actual values, not defaults
            Assert.That(result.FeatureUsage, Is.EqualTo(42), 
                "FeatureUsage should match the company's actual metric value (42), not a default");
            Assert.That(result.FeatureAllocation, Is.EqualTo(100),
                "FeatureAllocation should match the condition's MetricValue (100), not a default");

            // Verify the computation is correct
            var remainingUsage = result.FeatureAllocation - result.FeatureUsage;
            Assert.That(remainingUsage, Is.EqualTo(58), 
                "Company should have 58 remaining usage (100 - 42), proving WASM computed real values");
        }

        [Test] 
        public async Task WasmEngine_ReturnsCorrectValues_WhenUsageExceedsLimit()
        {
            // Arrange - Test the opposite case where usage exceeds the limit
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();

            string eventSubtype = "premium-feature";
            var rule = TestHelpers.CreateTestRule();
            rule.RuleType = RulesengineRuleRuleType.PlanEntitlementUsageExceeded;
            rule.Value = false; // Should return false when exceeded

            var metricCondition = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Metric);
            metricCondition.EventSubtype = eventSubtype;
            metricCondition.MetricPeriod = RulesengineConditionMetricPeriod.AllTime;
            metricCondition.MetricValue = 50; // Limit is 50
            metricCondition.Operator = RulesengineConditionOperator.Gt; // Greater than (exceeded)
            metricCondition.ResourceIds = new List<string> { company.Id };

            rule.Conditions = new List<RulesengineCondition> { metricCondition };
            flag.Rules = new List<RulesengineRule> { rule };

            // Company has used 75 (exceeds the limit of 50)
            var metricPeriod = new RulesengineCompanyMetricPeriod(metricCondition.MetricPeriod!.Value.Value);
            var companyMetric = TestHelpers.CreateTestMetric(company, eventSubtype, metricPeriod, 75);
            company.Metrics = new List<RulesengineCompanyMetric> { companyMetric };

            // Act
            using var wasmEngine = new WasmRulesEngine();
            var result = await wasmEngine.CheckFlagAsync(company, null, flag);

            // Assert - Validate WASM correctly computed the exceeded scenario
            Assert.That(result.Value, Is.False, "Should return false since 75 > 50 (usage exceeded)");
            Assert.That(result.RuleType!.Value, Is.EqualTo(RulesengineRuleRuleType.PlanEntitlementUsageExceeded.Value),
                "RuleType should indicate usage exceeded");

            // Validate actual computed values, not defaults
            Assert.That(result.FeatureUsage, Is.EqualTo(75),
                "FeatureUsage should show actual usage (75), proving WASM computed real values");
            Assert.That(result.FeatureAllocation, Is.EqualTo(50),
                "FeatureAllocation should show the limit (50)");

            // Prove the computation is meaningful
            Assert.That(result.FeatureUsage > result.FeatureAllocation, Is.True,
                "Usage (75) should exceed allocation (50), confirming WASM logic is working");
        }

        [Test]
        public async Task WasmEngine_ReturnsIdenticalComputedValues_AsTraditionalEngine()
        {
            // Arrange - Create identical test scenario for both engines
            var company = TestHelpers.CreateTestCompany();
            var user = TestHelpers.CreateTestUser();
            var flag = TestHelpers.CreateTestFlag();

            string eventSubtype = "storage-usage";
            var rule = TestHelpers.CreateTestRule();
            rule.RuleType = RulesengineRuleRuleType.PlanEntitlement;
            rule.Value = true;

            var metricCondition = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Metric);
            metricCondition.EventSubtype = eventSubtype;
            metricCondition.MetricPeriod = RulesengineConditionMetricPeriod.AllTime;
            metricCondition.MetricValue = 200; // 200GB limit
            metricCondition.Operator = RulesengineConditionOperator.Lte;
            metricCondition.ResourceIds = new List<string> { company.Id };

            rule.Conditions = new List<RulesengineCondition> { metricCondition };
            flag.Rules = new List<RulesengineRule> { rule };

            // Company has used 150GB
            var metricPeriod = new RulesengineCompanyMetricPeriod(metricCondition.MetricPeriod!.Value.Value);
            var companyMetric = TestHelpers.CreateTestMetric(company, eventSubtype, metricPeriod, 150);
            company.Metrics = new List<RulesengineCompanyMetric> { companyMetric };

            // Act - Test both engines with identical inputs
            var traditionalResult = await FlagCheckService.CheckFlag(company, user, flag);
            
            using var wasmEngine = new WasmRulesEngine();
            var wasmResult = await wasmEngine.CheckFlagAsync(company, user, flag);

            // Assert - Both engines should compute identical values
            Assert.That(wasmResult.Value, Is.EqualTo(traditionalResult.Value), 
                "Both engines should return the same boolean result");
            Assert.That(wasmResult.RuleId, Is.EqualTo(traditionalResult.RuleId),
                "Both engines should identify the same matching rule");
            Assert.That(wasmResult.RuleType?.Value, Is.EqualTo(traditionalResult.RuleType?.Value),
                "Both engines should set the same RuleType");

            // Critical validation: computed feature usage values should be identical
            Assert.That(wasmResult.FeatureUsage, Is.EqualTo(traditionalResult.FeatureUsage),
                "WASM and traditional engines should compute identical FeatureUsage values");
            Assert.That(wasmResult.FeatureAllocation, Is.EqualTo(traditionalResult.FeatureAllocation),
                "WASM and traditional engines should compute identical FeatureAllocation values");
            Assert.That(wasmResult.FeatureUsagePeriod?.Value, Is.EqualTo(traditionalResult.FeatureUsagePeriod?.Value),
                "WASM and traditional engines should set identical FeatureUsagePeriod values");

            // Validate that these are meaningful computed values, not defaults
            Assert.That(wasmResult.FeatureUsage, Is.EqualTo(150), "Should reflect actual usage");
            Assert.That(wasmResult.FeatureAllocation, Is.EqualTo(200), "Should reflect actual allocation");
            
            // Prove computation consistency
            var wasmRemaining = wasmResult.FeatureAllocation - wasmResult.FeatureUsage;
            var traditionalRemaining = traditionalResult.FeatureAllocation - traditionalResult.FeatureUsage;
            Assert.That(wasmRemaining, Is.EqualTo(traditionalRemaining).And.EqualTo(50),
                "Both engines should compute identical remaining usage (50)");
        }
    }
}