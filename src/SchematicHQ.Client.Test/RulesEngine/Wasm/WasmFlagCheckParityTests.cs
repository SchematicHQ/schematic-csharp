using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.RulesEngine;

namespace SchematicHQ.Client.Test.RulesEngine.Wasm
{
    /// <summary>
    /// Behavioral parity coverage for the WASM rules engine, ported from the former native
    /// FlagCheckService unit tests. Each case drives <see cref="WasmRulesEngine.CheckFlag"/>
    /// and asserts the same evaluation outcomes (value, matched rule, entitlement usage /
    /// allocation) the native C# port produced — the regression guard the migration needs.
    ///
    /// <para>Native-only reason strings (e.g. "No rules matched") are not asserted here; the
    /// WASM engine emits its own reasons, so these tests assert on <c>Value</c>/<c>RuleId</c>
    /// (a null <c>RuleId</c> means no rule matched → flag default).</para>
    /// </summary>
    [TestFixture]
    public class WasmFlagCheckParityTests
    {
        private WasmRulesEngine _engine = null!;

        [OneTimeSetUp]
        public void SetUp()
        {
            _engine = new WasmRulesEngine(NullLogger.Instance);
            _engine.Initialize();
        }

        [OneTimeTearDown]
        public void TearDown() => _engine.Dispose();

        [Test]
        public void Returns_DefaultValue_WhenNoRulesMatch()
        {
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();
            flag.DefaultValue = true;

            var result = _engine.CheckFlag(company, null, flag);

            Assert.That(result.Value, Is.True);
            Assert.That(result.CompanyId, Is.EqualTo(company.Id));
            Assert.That(result.RuleId, Is.Null);
        }

        [Test]
        public void GlobalOverride_TakesPrecedence()
        {
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();

            var standardRule = TestHelpers.CreateTestRule();
            standardRule.Value = false;
            var standardCondition = TestHelpers.CreateTestCondition(RulesengineConditionType.Company);
            standardCondition.ResourceIds = new List<string> { company.Id };
            standardRule.Conditions = new List<RulesengineCondition> { standardCondition };

            var overrideRule = TestHelpers.CreateTestRule();
            overrideRule.RuleType = RulesengineRuleType.GlobalOverride;
            overrideRule.Value = true;

            flag.Rules = new List<RulesengineRule> { standardRule, overrideRule };

            var result = _engine.CheckFlag(company, null, flag);

            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(overrideRule.Id));
        }

        [Test]
        public void Rules_Evaluated_In_Priority_Order()
        {
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();

            var rule1 = TestHelpers.CreateTestRule();
            rule1.Priority = 2;
            rule1.Value = false;
            var condition1 = TestHelpers.CreateTestCondition(RulesengineConditionType.Company);
            condition1.ResourceIds = new List<string> { company.Id };
            rule1.Conditions = new List<RulesengineCondition> { condition1 };

            var rule2 = TestHelpers.CreateTestRule();
            rule2.Priority = 1; // Lower priority number = higher priority
            rule2.Value = true;
            var condition2 = TestHelpers.CreateTestCondition(RulesengineConditionType.Company);
            condition2.ResourceIds = new List<string> { company.Id };
            rule2.Conditions = new List<RulesengineCondition> { condition2 };

            flag.Rules = new List<RulesengineRule> { rule1, rule2 };

            var result = _engine.CheckFlag(company, null, flag);

            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(rule2.Id));
        }

        [Test]
        public void Condition_Group_Matches_When_Any_Condition_Matches()
        {
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();

            var rule = TestHelpers.CreateTestRule();
            rule.Value = true;

            var condition1 = TestHelpers.CreateTestCondition(RulesengineConditionType.Company);
            condition1.ResourceIds = new List<string> { "non-matching-id" };

            var condition2 = TestHelpers.CreateTestCondition(RulesengineConditionType.Company);
            condition2.ResourceIds = new List<string> { company.Id };

            var group = new RulesengineConditionGroup
            {
                Conditions = new List<RulesengineCondition> { condition1, condition2 }
            };

            rule.ConditionGroups = new List<RulesengineConditionGroup> { group };
            flag.Rules = new List<RulesengineRule> { rule };

            var result = _engine.CheckFlag(company, null, flag);

            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(rule.Id));
        }

        [Test]
        public void Sets_Usage_And_Allocation_For_Metric_Condition()
        {
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();

            const string eventSubtype = "test-event";
            var rule = TestHelpers.CreateTestRule();
            rule.RuleType = RulesengineRuleType.PlanEntitlement;
            rule.Value = true;

            var condition = TestHelpers.CreateTestCondition(RulesengineConditionType.Metric);
            condition.EventSubtype = eventSubtype;
            condition.MetricValue = 10;
            condition.Operator = ComparableOperator.Lte;

            rule.Conditions = new List<RulesengineCondition> { condition };
            flag.Rules = new List<RulesengineRule> { rule };

            var metric = TestHelpers.CreateTestMetric(company, eventSubtype, condition.MetricPeriod!.Value, 5);
            company.Metrics = new List<RulesengineCompanyMetric> { metric };

            var result = _engine.CheckFlag(company, null, flag);

            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(rule.Id));
            Assert.That(result.FeatureUsage, Is.EqualTo(5));
            Assert.That(result.FeatureAllocation, Is.EqualTo(10));
        }

        [Test]
        public void Sets_Usage_And_Allocation_For_Trait_Condition()
        {
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();

            var traitDef = TestHelpers.CreateTestTraitDefinition(RulesengineTraitDefinitionComparableType.Int, RulesengineEntityType.Company);
            var trait = TestHelpers.CreateTestTrait("5", traitDef);
            company.Traits = new List<RulesengineTrait> { trait };

            var rule = TestHelpers.CreateTestRule();
            rule.RuleType = RulesengineRuleType.PlanEntitlement;
            rule.Value = true;

            var condition = TestHelpers.CreateTestCondition(RulesengineConditionType.Trait);
            condition.TraitDefinition = traitDef;
            condition.TraitValue = "10";
            condition.Operator = ComparableOperator.Lte;

            rule.Conditions = new List<RulesengineCondition> { condition };
            flag.Rules = new List<RulesengineRule> { rule };

            var result = _engine.CheckFlag(company, null, flag);

            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(rule.Id));
            Assert.That(result.FeatureUsage, Is.EqualTo(5));
            Assert.That(result.FeatureAllocation, Is.EqualTo(10));
        }

        [Test]
        public void Matches_User_Specific_Conditions()
        {
            var user = TestHelpers.CreateTestUser();
            var flag = TestHelpers.CreateTestFlag();

            var rule = TestHelpers.CreateTestRule();
            rule.Value = true;
            var condition = TestHelpers.CreateTestCondition(RulesengineConditionType.User);
            condition.ResourceIds = new List<string> { user.Id };
            rule.Conditions = new List<RulesengineCondition> { condition };

            flag.Rules = new List<RulesengineRule> { rule };

            var result = _engine.CheckFlag(null, user, flag);

            Assert.That(result.Value, Is.True);
            Assert.That(result.UserId, Is.EqualTo(user.Id));
            Assert.That(result.RuleId, Is.EqualTo(rule.Id));
        }

        [Test]
        public void Checks_User_Traits()
        {
            var user = TestHelpers.CreateTestUser();
            var traitDef = TestHelpers.CreateTestTraitDefinition(RulesengineTraitDefinitionComparableType.String, RulesengineEntityType.User);
            var trait = TestHelpers.CreateTestTrait("test-value", traitDef);
            user.Traits = new List<RulesengineTrait> { trait };

            var flag = TestHelpers.CreateTestFlag();
            var rule = TestHelpers.CreateTestRule();
            rule.Value = true;

            var condition = TestHelpers.CreateTestCondition(RulesengineConditionType.Trait);
            condition.TraitDefinition = traitDef;
            condition.TraitValue = "test-value";
            condition.Operator = ComparableOperator.Eq;

            rule.Conditions = new List<RulesengineCondition> { condition };
            flag.Rules = new List<RulesengineRule> { rule };

            var result = _engine.CheckFlag(null, user, flag);

            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(rule.Id));
        }

        [Test]
        public void Handles_Multiple_Condition_Types_And_Groups()
        {
            var company = TestHelpers.CreateTestCompany();
            var trait = TestHelpers.CreateTestTrait("test-value", null);
            company.Traits = new List<RulesengineTrait> { trait };

            var flag = TestHelpers.CreateTestFlag();
            var rule = TestHelpers.CreateTestRule();
            rule.Value = true;

            var condition1 = TestHelpers.CreateTestCondition(RulesengineConditionType.Company);
            condition1.ResourceIds = new List<string> { company.Id };

            var condition2 = TestHelpers.CreateTestCondition(RulesengineConditionType.Trait);
            condition2.TraitDefinition = trait.TraitDefinition;
            condition2.TraitValue = "test-value";
            condition2.Operator = ComparableOperator.Eq;

            rule.Conditions = new List<RulesengineCondition> { condition1, condition2 };

            var groupCondition1 = TestHelpers.CreateTestCondition(RulesengineConditionType.Plan);
            groupCondition1.ResourceIds = new List<string> { company.PlanIds.First() };

            var groupCondition2 = TestHelpers.CreateTestCondition(RulesengineConditionType.BasePlan);
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

            var result = _engine.CheckFlag(company, null, flag);

            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(rule.Id));
        }

        [Test]
        public void Handles_Missing_Or_Invalid_Data_Gracefully()
        {
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();
            var rule = TestHelpers.CreateTestRule();

            var condition = new RulesengineCondition
            {
                Id = "",
                AccountId = "",
                EnvironmentId = "",
                ConditionType = RulesengineConditionType.Metric,
                Operator = ComparableOperator.Eq,
                TraitValue = ""
            };
            rule.Conditions = new List<RulesengineCondition> { condition };

            var group = new RulesengineConditionGroup();
            rule.ConditionGroups = new List<RulesengineConditionGroup> { group };

            flag.Rules = new List<RulesengineRule> { rule };

            var result = _engine.CheckFlag(company, null, flag);

            Assert.That(result.Value, Is.EqualTo(flag.DefaultValue));
            Assert.That(result.RuleId, Is.Null);
        }

        [Test]
        public void CompanyProvidedRule_IsEvaluatedAlongWithFlagRules()
        {
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();
            flag.DefaultValue = false;

            var companyRule = TestHelpers.CreateTestRule();
            companyRule.FlagId = flag.Id;
            companyRule.Value = true;
            var condition = TestHelpers.CreateTestCondition(RulesengineConditionType.Company);
            condition.ResourceIds = new List<string> { company.Id };
            companyRule.Conditions = new List<RulesengineCondition> { condition };

            company.Rules = new List<RulesengineRule> { companyRule };

            var result = _engine.CheckFlag(company, null, flag);

            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(companyRule.Id));
        }

        [Test]
        public void CompanyProvidedRule_RespectsPriorityOrdering()
        {
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();

            var flagRule = TestHelpers.CreateTestRule();
            flagRule.Priority = 2;
            flagRule.Value = false;
            var condition1 = TestHelpers.CreateTestCondition(RulesengineConditionType.Company);
            condition1.ResourceIds = new List<string> { company.Id };
            flagRule.Conditions = new List<RulesengineCondition> { condition1 };

            var companyRule = TestHelpers.CreateTestRule();
            companyRule.FlagId = flag.Id;
            companyRule.Priority = 1;
            companyRule.Value = true;
            var condition2 = TestHelpers.CreateTestCondition(RulesengineConditionType.Company);
            condition2.ResourceIds = new List<string> { company.Id };
            companyRule.Conditions = new List<RulesengineCondition> { condition2 };

            flag.Rules = new List<RulesengineRule> { flagRule };
            company.Rules = new List<RulesengineRule> { companyRule };

            var result = _engine.CheckFlag(company, null, flag);

            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(companyRule.Id));
        }

        [Test]
        public void CompanyProvidedRule_WithGlobalOverrideTakesPrecedence()
        {
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();

            var flagRule = TestHelpers.CreateTestRule();
            flagRule.Value = false;
            var condition1 = TestHelpers.CreateTestCondition(RulesengineConditionType.Company);
            condition1.ResourceIds = new List<string> { company.Id };
            flagRule.Conditions = new List<RulesengineCondition> { condition1 };

            var companyRule = TestHelpers.CreateTestRule();
            companyRule.FlagId = flag.Id;
            companyRule.RuleType = RulesengineRuleType.GlobalOverride;
            companyRule.Value = true;

            flag.Rules = new List<RulesengineRule> { flagRule };
            company.Rules = new List<RulesengineRule> { companyRule };

            var result = _engine.CheckFlag(company, null, flag);

            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(companyRule.Id));
        }

        [Test]
        public void MultipleCompanyProvidedRules_AreAllEvaluated()
        {
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();
            flag.DefaultValue = false;

            var companyRule1 = TestHelpers.CreateTestRule();
            companyRule1.FlagId = flag.Id;
            companyRule1.Priority = 1;
            companyRule1.Value = true;
            var condition1 = TestHelpers.CreateTestCondition(RulesengineConditionType.Company);
            condition1.ResourceIds = new List<string> { "non-matching-id" };
            companyRule1.Conditions = new List<RulesengineCondition> { condition1 };

            var companyRule2 = TestHelpers.CreateTestRule();
            companyRule2.FlagId = flag.Id;
            companyRule2.Priority = 2;
            companyRule2.Value = true;
            var condition2 = TestHelpers.CreateTestCondition(RulesengineConditionType.Company);
            condition2.ResourceIds = new List<string> { company.Id };
            companyRule2.Conditions = new List<RulesengineCondition> { condition2 };

            company.Rules = new List<RulesengineRule> { companyRule1, companyRule2 };

            var result = _engine.CheckFlag(company, null, flag);

            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(companyRule2.Id));
        }

        [Test]
        public void UserProvidedRule_IsEvaluatedAlongWithFlagRules()
        {
            var user = TestHelpers.CreateTestUser();
            var flag = TestHelpers.CreateTestFlag();
            flag.DefaultValue = false;

            var userRule = TestHelpers.CreateTestRule();
            userRule.FlagId = flag.Id;
            userRule.Value = true;
            var condition = TestHelpers.CreateTestCondition(RulesengineConditionType.User);
            condition.ResourceIds = new List<string> { user.Id };
            userRule.Conditions = new List<RulesengineCondition> { condition };

            user.Rules = new List<RulesengineRule> { userRule };

            var result = _engine.CheckFlag(null, user, flag);

            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(userRule.Id));
        }

        [Test]
        public void UserProvidedRule_RespectsPriorityOrdering()
        {
            var user = TestHelpers.CreateTestUser();
            var flag = TestHelpers.CreateTestFlag();

            var flagRule = TestHelpers.CreateTestRule();
            flagRule.Priority = 2;
            flagRule.Value = false;
            var condition1 = TestHelpers.CreateTestCondition(RulesengineConditionType.User);
            condition1.ResourceIds = new List<string> { user.Id };
            flagRule.Conditions = new List<RulesengineCondition> { condition1 };

            var userRule = TestHelpers.CreateTestRule();
            userRule.FlagId = flag.Id;
            userRule.Priority = 1;
            userRule.Value = true;
            var condition2 = TestHelpers.CreateTestCondition(RulesengineConditionType.User);
            condition2.ResourceIds = new List<string> { user.Id };
            userRule.Conditions = new List<RulesengineCondition> { condition2 };

            flag.Rules = new List<RulesengineRule> { flagRule };
            user.Rules = new List<RulesengineRule> { userRule };

            var result = _engine.CheckFlag(null, user, flag);

            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(userRule.Id));
        }

        [Test]
        public void UserProvidedRule_WithGlobalOverrideTakesPrecedence()
        {
            var user = TestHelpers.CreateTestUser();
            var flag = TestHelpers.CreateTestFlag();

            var flagRule = TestHelpers.CreateTestRule();
            flagRule.Value = false;
            var condition1 = TestHelpers.CreateTestCondition(RulesengineConditionType.User);
            condition1.ResourceIds = new List<string> { user.Id };
            flagRule.Conditions = new List<RulesengineCondition> { condition1 };

            var userRule = TestHelpers.CreateTestRule();
            userRule.FlagId = flag.Id;
            userRule.RuleType = RulesengineRuleType.GlobalOverride;
            userRule.Value = true;

            flag.Rules = new List<RulesengineRule> { flagRule };
            user.Rules = new List<RulesengineRule> { userRule };

            var result = _engine.CheckFlag(null, user, flag);

            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(userRule.Id));
        }

        [Test]
        public void BothCompanyAndUserRules_AreEvaluated()
        {
            var company = TestHelpers.CreateTestCompany();
            var user = TestHelpers.CreateTestUser();
            var flag = TestHelpers.CreateTestFlag();
            flag.DefaultValue = false;

            var companyRule = TestHelpers.CreateTestRule();
            companyRule.FlagId = flag.Id;
            companyRule.Priority = 1;
            companyRule.Value = true;
            var condition1 = TestHelpers.CreateTestCondition(RulesengineConditionType.Company);
            condition1.ResourceIds = new List<string> { "non-matching-id" };
            companyRule.Conditions = new List<RulesengineCondition> { condition1 };

            var userRule = TestHelpers.CreateTestRule();
            userRule.FlagId = flag.Id;
            userRule.Priority = 2;
            userRule.Value = true;
            var condition2 = TestHelpers.CreateTestCondition(RulesengineConditionType.User);
            condition2.ResourceIds = new List<string> { user.Id };
            userRule.Conditions = new List<RulesengineCondition> { condition2 };

            company.Rules = new List<RulesengineRule> { companyRule };
            user.Rules = new List<RulesengineRule> { userRule };

            var result = _engine.CheckFlag(company, user, flag);

            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(userRule.Id));
        }

        [Test]
        public void AllThreeRuleSources_EvaluatedWithCorrectPriority()
        {
            var company = TestHelpers.CreateTestCompany();
            var user = TestHelpers.CreateTestUser();
            var flag = TestHelpers.CreateTestFlag();
            flag.DefaultValue = false;

            var flagRule = TestHelpers.CreateTestRule();
            flagRule.Priority = 2;
            flagRule.Value = true;
            var condition1 = TestHelpers.CreateTestCondition(RulesengineConditionType.Company);
            condition1.ResourceIds = new List<string> { company.Id };
            flagRule.Conditions = new List<RulesengineCondition> { condition1 };

            var companyRule = TestHelpers.CreateTestRule();
            companyRule.FlagId = flag.Id;
            companyRule.Priority = 3;
            companyRule.Value = true;
            var condition2 = TestHelpers.CreateTestCondition(RulesengineConditionType.Company);
            condition2.ResourceIds = new List<string> { company.Id };
            companyRule.Conditions = new List<RulesengineCondition> { condition2 };

            var userRule = TestHelpers.CreateTestRule();
            userRule.FlagId = flag.Id;
            userRule.Priority = 1; // Highest priority
            userRule.Value = true;
            var condition3 = TestHelpers.CreateTestCondition(RulesengineConditionType.User);
            condition3.ResourceIds = new List<string> { user.Id };
            userRule.Conditions = new List<RulesengineCondition> { condition3 };

            flag.Rules = new List<RulesengineRule> { flagRule };
            company.Rules = new List<RulesengineRule> { companyRule };
            user.Rules = new List<RulesengineRule> { userRule };

            var result = _engine.CheckFlag(company, user, flag);

            Assert.That(result.Value, Is.True);
            Assert.That(result.RuleId, Is.EqualTo(userRule.Id)); // user rule has highest priority
        }

        [Test]
        public void CompanyProvidedRule_WithWrongFlagId_IsNotEvaluated()
        {
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();
            flag.DefaultValue = false;

            var companyRule = TestHelpers.CreateTestRule();
            companyRule.FlagId = TestHelpers.GenerateTestId("flag"); // Different flag ID
            companyRule.Value = true;
            var condition = TestHelpers.CreateTestCondition(RulesengineConditionType.Company);
            condition.ResourceIds = new List<string> { company.Id };
            companyRule.Conditions = new List<RulesengineCondition> { condition };

            company.Rules = new List<RulesengineRule> { companyRule };

            var result = _engine.CheckFlag(company, null, flag);

            Assert.That(result.Value, Is.False); // flag default
            Assert.That(result.RuleId, Is.Null);
        }

        [Test]
        public void UserProvidedRule_WithWrongFlagId_IsNotEvaluated()
        {
            var user = TestHelpers.CreateTestUser();
            var flag = TestHelpers.CreateTestFlag();
            flag.DefaultValue = false;

            var userRule = TestHelpers.CreateTestRule();
            userRule.FlagId = TestHelpers.GenerateTestId("flag"); // Different flag ID
            userRule.Value = true;
            var condition = TestHelpers.CreateTestCondition(RulesengineConditionType.User);
            condition.ResourceIds = new List<string> { user.Id };
            userRule.Conditions = new List<RulesengineCondition> { condition };

            user.Rules = new List<RulesengineRule> { userRule };

            var result = _engine.CheckFlag(null, user, flag);

            Assert.That(result.Value, Is.False); // flag default
            Assert.That(result.RuleId, Is.Null);
        }

        [Test]
        public void CompanyProvidedRule_WithNullFlagId_IsNotEvaluated()
        {
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();
            flag.DefaultValue = false;

            var companyRule = TestHelpers.CreateTestRule();
            companyRule.FlagId = null;
            companyRule.Value = true;
            var condition = TestHelpers.CreateTestCondition(RulesengineConditionType.Company);
            condition.ResourceIds = new List<string> { company.Id };
            companyRule.Conditions = new List<RulesengineCondition> { condition };

            company.Rules = new List<RulesengineRule> { companyRule };

            var result = _engine.CheckFlag(company, null, flag);

            Assert.That(result.Value, Is.False); // flag default
            Assert.That(result.RuleId, Is.Null);
        }

        [Test]
        public void UserProvidedRule_WithNullFlagId_IsNotEvaluated()
        {
            var user = TestHelpers.CreateTestUser();
            var flag = TestHelpers.CreateTestFlag();
            flag.DefaultValue = false;

            var userRule = TestHelpers.CreateTestRule();
            userRule.FlagId = null;
            userRule.Value = true;
            var condition = TestHelpers.CreateTestCondition(RulesengineConditionType.User);
            condition.ResourceIds = new List<string> { user.Id };
            userRule.Conditions = new List<RulesengineCondition> { condition };

            user.Rules = new List<RulesengineRule> { userRule };

            var result = _engine.CheckFlag(null, user, flag);

            Assert.That(result.Value, Is.False); // flag default
            Assert.That(result.RuleId, Is.Null);
        }
    }
}
