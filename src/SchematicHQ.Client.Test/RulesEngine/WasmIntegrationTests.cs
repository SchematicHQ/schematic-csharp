using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using SchematicHQ.Client.RulesEngine.Wasm;
using SchematicHQ.Client.RulesEngine;

namespace SchematicHQ.Client.Test.RulesEngine
{
    [TestFixture]
    public class WasmIntegrationTests
    {
        [Test]
        public void WasmEngine_Initializes_Successfully()
        {
            // Arrange & Act
            using var wasmEngine = new WasmRulesEngine();
            
            // Assert
            Assert.That(wasmEngine, Is.Not.Null);
        }

        [Test]
        public void WasmEngine_GetVersionKey_ReturnsExpectedFormat()
        {
            // Arrange
            using var wasmEngine = new WasmRulesEngine();
            
            // Act
            var version = wasmEngine.GetVersionKey();
            
            // Assert
            Assert.That(version, Is.Not.Null);
            Assert.That(version, Is.Not.Empty);
            // Version should be either the schema hash or a test/error indicator
            Assert.That(version.Length, Is.GreaterThan(0));
        }

        [Test]
        public async Task WasmEngine_CheckFlag_FallsBackToTraditional_WhenNeeded()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();
            var flag = TestHelpers.CreateTestFlag();
            flag.DefaultValue = true;

            // Act - Use the public FlagCheckService which tries WASM then falls back
            var result = await FlagCheckService.CheckFlag(company, null, flag);

            // Assert - Should fall back to traditional implementation for detailed results
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.True); // Should use default value
            Assert.That(result.FlagId, Is.EqualTo(flag.Id));
            Assert.That(result.FlagKey, Is.EqualTo(flag.Key));
            Assert.That(result.CompanyId, Is.EqualTo(company.Id));
            Assert.That(result.Reason, Is.EqualTo(FlagCheckService.ReasonNoRulesMatched));
        }

        [Test]
        public void WasmEngine_EvaluateRule_FallsBackToTraditional()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();
            var user = TestHelpers.CreateTestUser();
            var rule = TestHelpers.CreateTestRule();
            
            // Create a condition that should not match
            var condition = TestHelpers.CreateTestCondition(RulesengineConditionConditionType.Company);
            condition.ResourceIds = new List<string> { "different-company-id" };
            rule.Conditions = new List<RulesengineCondition> { condition };

            // Act - Test that rule evaluation logic works (indirectly testing WASM fallback)
            var ruleChecker = RuleCheckService.NewRuleCheckService();
            var checkScope = new CheckScope { Company = company, Rule = rule, User = user };
            var result = ruleChecker.Check(checkScope).Result;

            // Assert - Rule should not match because company ID doesn't match
            Assert.That(result.Match, Is.False);
        }

        [Test]
        public void WasmEngine_MetricPeriodCalculations_WorkCorrectly()
        {
            // Arrange
            using var wasmEngine = new WasmRulesEngine();
            var metricPeriod = RulesengineConditionMetricPeriod.CurrentMonth;

            // Act
            var currentStart = wasmEngine.GetCurrentMetricPeriodStartForCalendarMetricPeriod(metricPeriod);
            var nextStart = wasmEngine.GetNextMetricPeriodStartForCalendarMetricPeriod(metricPeriod);

            // Assert
            Assert.That(currentStart, Is.Not.Null);
            Assert.That(nextStart, Is.Not.Null);
            Assert.That(nextStart, Is.GreaterThan(currentStart));
        }

        [Test]
        public void WasmEngine_CompanyBillingSubscription_HandlesNullCompany()
        {
            // Arrange
            using var wasmEngine = new WasmRulesEngine();

            // Act
            var currentStart = wasmEngine.GetCurrentMetricPeriodStartForCompanyBillingSubscription(null);
            var nextStart = wasmEngine.GetNextMetricPeriodStartForCompanyBillingSubscription(null);

            // Assert - Should fall back to traditional implementation for null company
            // The traditional implementation returns current month start for null company
            Assert.That(currentStart, Is.Not.Null);
            Assert.That(nextStart, Is.Not.Null);
            Assert.That(nextStart, Is.GreaterThan(currentStart));
        }

        [Test] 
        public void WasmEngine_Dispose_WorksCorrectly()
        {
            // Arrange
            var wasmEngine = new WasmRulesEngine();
            
            // Act & Assert - Should not throw
            Assert.DoesNotThrow(() => wasmEngine.Dispose());
            
            // Should throw ObjectDisposedException on subsequent calls
            Assert.Throws<ObjectDisposedException>(() => wasmEngine.GetVersionKey());
        }
    }
}