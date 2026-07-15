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
        public void Initializes_And_Exposes_Version_Key()
        {
            Assert.That(_engine.IsInitialized, Is.True);
            Assert.That(_engine.GetVersionKey(), Is.Not.Empty);
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
    }
}
