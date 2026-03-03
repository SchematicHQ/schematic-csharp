using SchematicHQ.Client.RulesEngine;
using System.Diagnostics;
using NUnit.Framework;

namespace SchematicHQ.Client.Test.RulesEngine
{
    [TestFixture]
    public class CompanyTests
    {
        [Test]
        public void AddMetric_Adds_New_Metric_When_None_Exists_With_Same_Constraints()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();
            int initialCount = company.Metrics.Count();

            // Act
            var metric = TestHelpers.CreateTestMetric(company, "test-event", RulesengineConditionMetricPeriod.AllTime, 5);
            company.AddMetric(metric);

            // Assert
            Assert.That(company.Metrics.Count(), Is.EqualTo(initialCount + 1));
            Assert.That(company.Metrics, Does.Contain(metric));
        }

        [Test]
        public void AddMetric_Replaces_Existing_Metric_With_Same_Constraints()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();

            // Add initial metric
            string eventSubtype = "test-event";
            var period = RulesengineConditionMetricPeriod.AllTime;
            RulesengineConditionMetricPeriodMonthReset monthReset = RulesengineConditionMetricPeriodMonthReset.FirstOfMonth;

            var initialMetric = TestHelpers.CreateTestMetric(company, eventSubtype, period, 5);
            company.AddMetric(initialMetric);
            int initialCount = company.Metrics.Count();

            // Act - Add metric with same constraints but different value
            var newMetric = TestHelpers.CreateTestMetric(company, eventSubtype, period, 10);
            company.AddMetric(newMetric);

            // Assert
            // Verify the length hasn't changed but the new metric replaced the old one
            Assert.That(company.Metrics.Count(), Is.EqualTo(initialCount));
            Assert.That(company.Metrics, Does.Contain(newMetric));
            Assert.That(company.Metrics, Does.Not.Contain(initialMetric));

            // Find the metric and verify it has the new value
            var foundMetric = CompanyMetricExtensions.Find(company.Metrics, eventSubtype, period, monthReset);
            Assert.That(foundMetric, Is.Not.Null);
            Assert.That(foundMetric!.Value, Is.EqualTo(10));
        }

        [Test]
        public void AddMetric_Handles_Concurrent_Updates_Safely()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();

            // Set up concurrent tasks to add metrics
            const int numTasks = 10;
            var tasks = new List<Task>();

            // Act
            for (int i = 0; i < numTasks; i++)
            {
                var index = i; // Capture the current index for the task
                tasks.Add(Task.Run(() =>
                {
                    // Create a metric with a unique event subtype to avoid collision
                    string uniqueSubtype = $"test-event-{DateTime.Now.Ticks}-{index}";
                    var metric = TestHelpers.CreateTestMetric(company, uniqueSubtype, RulesengineConditionMetricPeriod.AllTime, index);

                    // Add the metric
                    company.AddMetric(metric);
                }));
            }

            var taskArray = tasks.ToArray();
            Debug.WriteLine($"Starting {taskArray.Length} concurrent tasks to add metrics...");

            // Wait for all tasks to finish
            Task.WaitAll(tasks.ToArray());

            // Assert
            Assert.That(company.Metrics.Count(), Is.GreaterThanOrEqualTo(numTasks));
        }

        [Test]
        public void AddMetric_NullCompany_DoesNotThrow()
        {
            // Arrange
            RulesengineCompany? company = null;
            var metric = new RulesengineCompanyMetric
            {
                AccountId = "acct",
                EnvironmentId = "env",
                CompanyId = "comp",
                EventSubtype = "test",
                Value = 0,
                Period = new RulesengineCompanyMetricPeriod("all_time"),
                MonthReset = RulesengineCompanyMetricMonthReset.FirstOfMonth,
                CreatedAt = DateTime.UtcNow
            };

            // Act & Assert
            Assert.DoesNotThrow(() => company?.AddMetric(metric));
        }

        [Test]
        public void AddMetric_NullMetric_DoesNotThrow()
        {
            // Arrange
            var company = TestHelpers.CreateTestCompany();

            // Act & Assert
            Assert.DoesNotThrow(() => company.AddMetric(null));
        }

        [Test]
        public void AddMetric_CompanyWithNoMetrics_DoesNotThrow()
        {
            // Arrange
            var company = new RulesengineCompany
            {
                Id = TestHelpers.GenerateTestId("comp"),
                AccountId = TestHelpers.GenerateTestId("acct"),
                EnvironmentId = TestHelpers.GenerateTestId("env"),
                PlanIds = new List<string> { TestHelpers.GenerateTestId("plan"), TestHelpers.GenerateTestId("plan") },
                BillingProductIds = new List<string> { TestHelpers.GenerateTestId("bilp"), TestHelpers.GenerateTestId("bilp") },
                BasePlanId = TestHelpers.GenerateTestId("plan"),
                Traits = new List<RulesengineTrait>(),
                Subscription = new RulesengineSubscription
                {
                    Id = TestHelpers.GenerateTestId("bilsub"),
                    PeriodStart = DateTime.UtcNow.AddDays(-30),
                    PeriodEnd = DateTime.UtcNow.AddDays(30)
                },
                // Initialize with null metrics list
                Metrics = null!
            };

            var metric = TestHelpers.CreateTestMetric(company, "foo", RulesengineConditionMetricPeriod.AllTime, 1);

            // Act & Assert
            Assert.DoesNotThrow(() => company.AddMetric(metric));
            Assert.That(company.Metrics, Is.Not.Null);
            Assert.That(company.Metrics.Count(), Is.EqualTo(1));
        }
    }
}
