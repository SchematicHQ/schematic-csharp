using SchematicHQ.Client.RulesEngine.Utils;
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
}