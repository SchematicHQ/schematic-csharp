using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.RulesEngine;

namespace SchematicHQ.Client.Test.RulesEngine.Wasm
{
    /// <summary>
    /// Regression coverage for the WASM rules engine that replaced the native C# port.
    /// Asserts parity with the previous native results (value + entitlement fields) and
    /// that feature_usage_reset_at is populated on the clockless wasmtime host (SCHY-471).
    /// </summary>
    [TestFixture]
    public class WasmRulesEngineTests
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
        public void Initializes_From_Embedded_Wasm()
        {
            Assert.That(_engine.IsInitialized, Is.True);
        }

        [Test]
        public void Bounded_Metric_Period_Populates_Reset_At()
        {
            // SCHY-471: with setCurrentTimeMillis wired, a calendar metric period must
            // populate feature_usage_reset_at (rather than trapping / omitting it).
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();
            flag.DefaultValue = false;

            const string eventSubtype = "monthly-event";
            var rule = TestHelpers.CreateTestRule();
            rule.RuleType = RulesengineRuleType.PlanEntitlement;
            rule.Value = true;

            var condition = TestHelpers.CreateTestCondition(RulesengineConditionType.Metric);
            condition.EventSubtype = eventSubtype;
            condition.MetricValue = 10;
            condition.Operator = ComparableOperator.Lte;
            condition.MetricPeriod = RulesengineMetricPeriod.CurrentMonth;
            rule.Conditions = new List<RulesengineCondition> { condition };
            flag.Rules = new List<RulesengineRule> { rule };

            company.Metrics = new List<RulesengineCompanyMetric>
            {
                TestHelpers.CreateTestMetric(company, eventSubtype, RulesengineMetricPeriod.CurrentMonth, 5),
            };

            var result = _engine.CheckFlag(company, null, flag);

            Assert.That(result.Value, Is.True);
            Assert.That(result.FeatureUsageResetAt, Is.Not.Null);
            Assert.That(result.FeatureUsageResetAt, Is.GreaterThan(DateTime.UtcNow));
        }

        private bool CreditFlagAllowed(double balance, Dictionary<string, RulesengineCreditPostpaidConfig>? postpaid)
        {
            const string creditId = "cred-1";
            var company = TestHelpers.CreateTestCompany();
            company.CreditBalances = new Dictionary<string, double> { [creditId] = balance };
            company.CreditPostpaid = postpaid;

            var condition = TestHelpers.CreateTestCondition(RulesengineConditionType.Credit);
            condition.CreditId = creditId;
            condition.Operator = ComparableOperator.Lt;
            var rule = TestHelpers.CreateTestRule();
            rule.Conditions = new List<RulesengineCondition> { condition };
            var flag = TestHelpers.CreateTestFlag();
            flag.DefaultValue = false;
            flag.Rules = new List<RulesengineRule> { rule };

            return _engine.CheckFlag(company, null, flag).Value;
        }

        [Test]
        public void Credit_Postpaid_Absent_Or_Null_Is_Off()
        {
            Assert.That(CreditFlagAllowed(0, null), Is.False);
            Assert.That(CreditFlagAllowed(0, new Dictionary<string, RulesengineCreditPostpaidConfig>()), Is.False);
            Assert.That(CreditFlagAllowed(0, new Dictionary<string, RulesengineCreditPostpaidConfig> { ["cred-1"] = null! }), Is.False);
        }

        [Test]
        public void Credit_Postpaid_Config_Moves_The_Floor()
        {
            var unlimited = new Dictionary<string, RulesengineCreditPostpaidConfig> { ["cred-1"] = new() };
            Assert.That(CreditFlagAllowed(0, unlimited), Is.True);

            var limited = new Dictionary<string, RulesengineCreditPostpaidConfig>
            {
                ["cred-1"] = new() { OverdraftLimit = 100 }
            };
            Assert.That(CreditFlagAllowed(-40, limited), Is.True);
            Assert.That(CreditFlagAllowed(-150, limited), Is.False);
        }
    }
}
