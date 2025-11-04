using SchematicHQ.Client.RulesEngine.Models;
using SchematicHQ.Client.RulesEngine;
using NUnit.Framework;

namespace SchematicHQ.Client.Test.RulesEngine
{
    [TestFixture]
    public class MetricsTests
    {
        [Test]
        public void TestGetCurrentMetricPeriodStartForCalendarMetricPeriod()
        {
            // Test for CurrentDay
            {
                var result = Metrics.GetCurrentMetricPeriodStartForCalendarMetricPeriod(ConditionMetricPeriod.CurrentDay);
                Assert.That(result, Is.Not.Null);

                var expected = DateTime.UtcNow.Date;
                Assert.That(result?.Year, Is.EqualTo(expected.Year));
                Assert.That(result?.Month, Is.EqualTo(expected.Month));
                Assert.That(result?.Day, Is.EqualTo(expected.Day));
                Assert.That(result?.Hour, Is.EqualTo(0));
                Assert.That(result?.Minute, Is.EqualTo(0));
                Assert.That(result?.Second, Is.EqualTo(0));
            }

            // Test for CurrentWeek
            {
                var result = Metrics.GetCurrentMetricPeriodStartForCalendarMetricPeriod(ConditionMetricPeriod.CurrentWeek);
                Assert.That(result, Is.Not.Null);

                var now = DateTime.UtcNow;
                int daysSinceMonday = ((int)now.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
                var expected = now.Date.AddDays(-daysSinceMonday);
                Assert.That(result?.Year, Is.EqualTo(expected.Year));
                Assert.That(result?.Month, Is.EqualTo(expected.Month));
                Assert.That(result?.Day, Is.EqualTo(expected.Day));
                Assert.That(result?.Hour, Is.EqualTo(0));
                Assert.That(result?.Minute, Is.EqualTo(0));
                Assert.That(result?.Second, Is.EqualTo(0));
                Assert.That(result?.DayOfWeek, Is.EqualTo(DayOfWeek.Monday));
            }

            // Test for CurrentMonth
            {
                var result = Metrics.GetCurrentMetricPeriodStartForCalendarMetricPeriod(ConditionMetricPeriod.CurrentMonth);
                Assert.That(result, Is.Not.Null);

                var now = DateTime.UtcNow;
                var expected = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
                Assert.That(result?.Year, Is.EqualTo(expected.Year));
                Assert.That(result?.Month, Is.EqualTo(expected.Month));
                Assert.That(result?.Day, Is.EqualTo(expected.Day));
                Assert.That(result?.Hour, Is.EqualTo(0));
                Assert.That(result?.Minute, Is.EqualTo(0));
                Assert.That(result?.Second, Is.EqualTo(0));
                Assert.That(result?.Day, Is.EqualTo(1));
            }

            // Test for AllTime
            {
                var result = Metrics.GetCurrentMetricPeriodStartForCalendarMetricPeriod(ConditionMetricPeriod.AllTime);
                Assert.That(result, Is.Null);
            }
        }

        [Test]
        public void TestGetCurrentMetricPeriodStartForCompanyBillingSubscription()
        {
            // Test for null company
            {
                var result = Metrics.GetCurrentMetricPeriodStartForCompanyBillingSubscription(null);
                Assert.That(result, Is.Not.Null);

                var now = DateTime.UtcNow;
                var expected = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
                Assert.That(result?.Year, Is.EqualTo(expected.Year));
                Assert.That(result?.Month, Is.EqualTo(expected.Month));
                Assert.That(result?.Day, Is.EqualTo(1));
            }

            // Test for company with null subscription
            {
                var company = TestHelpers.CreateTestCompany();
                company.Subscription = null;
                var result = Metrics.GetCurrentMetricPeriodStartForCompanyBillingSubscription(company);
                Assert.That(result, Is.Not.Null);

                var now = DateTime.UtcNow;
                var expected = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
                Assert.That(result?.Year, Is.EqualTo(expected.Year));
                Assert.That(result?.Month, Is.EqualTo(expected.Month));
                Assert.That(result?.Day, Is.EqualTo(1));
            }

            // Test for subscription period start in future
            {
                var company = TestHelpers.CreateTestCompany();
                company.Subscription = new Subscription
                {
                    Id = "test-subscription",
                    PeriodStart = DateTime.UtcNow.AddDays(7),
                    PeriodEnd = DateTime.UtcNow.AddDays(37)
                };

                var result = Metrics.GetCurrentMetricPeriodStartForCompanyBillingSubscription(company);
                Assert.That(result, Is.Not.Null);

                var now = DateTime.UtcNow;
                var expected = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
                Assert.That(result?.Year, Is.EqualTo(expected.Year));
                Assert.That(result?.Month, Is.EqualTo(expected.Month));
                Assert.That(result?.Day, Is.EqualTo(1));
            }

            // Test for current month reset date not passed yet
            {
                var now = DateTime.UtcNow;
                var company = TestHelpers.CreateTestCompany();

                // Set subscription to start on a day later in the month than today
                int futureDay = 15;
                if (now.Day >= 15) // Avoid month boundary issues
                {
                    futureDay = 5;
                }

                company.Subscription = new Subscription
                {
                    Id = "test-subscription",
                    PeriodStart = new DateTime(
                        now.Year - 1,
                        now.Month,
                        futureDay,
                        12, 0, 0,
                        DateTimeKind.Utc),
                    PeriodEnd = now.AddMonths(6)
                };

                var result = Metrics.GetCurrentMetricPeriodStartForCompanyBillingSubscription(company);
                Assert.That(result, Is.Not.Null);

                // In this case, the result should be last month's reset date
                DateTime expected;
                if (now.Day < 15)
                {
                  expected = new DateTime(
                      now.Year,
                      now.Month,
                      futureDay,
                      12, 0, 0,
                      DateTimeKind.Utc);
                }
                else
                {
                  expected = new DateTime(
                      now.Year,
                      now.Month + 1,
                      futureDay,
                      12, 0, 0,
                      DateTimeKind.Utc);
                  if (now.Month == 12)
                  {
                      expected = new DateTime(
                          now.Year + 1,
                          1, // January
                          futureDay,
                          12, 0, 0,
                          DateTimeKind.Utc);  
                  }
                }

                Assert.That(result?.Year, Is.EqualTo(expected.Year));
                Assert.That(result?.Month, Is.EqualTo(expected.Month));
                Assert.That(result?.Day, Is.EqualTo(expected.Day));
                Assert.That(result?.Hour, Is.EqualTo(expected.Hour));
            }

            // Test for current month reset date has passed
            {
                var now = DateTime.UtcNow;
                var company = TestHelpers.CreateTestCompany();

                // Set subscription to start on a day earlier in the month than today
                int pastDay = now.Day - 5;
                if (pastDay < 1)
                {
                    pastDay = 1;
                }

                company.Subscription = new Subscription
                {
                    Id = "test-subscription",
                    PeriodStart = new DateTime(
                        now.Year - 1,
                        now.Month,
                        pastDay,
                        12, 0, 0,
                        DateTimeKind.Utc),
                    PeriodEnd = now.AddMonths(6)
                };

                var result = Metrics.GetCurrentMetricPeriodStartForCompanyBillingSubscription(company);
                Assert.That(result, Is.Not.Null);

                // In this case, the result should be this month's reset date
                var expected = new DateTime(
                    now.Year,
                    now.Month+1,
                    pastDay,
                    12, 0, 0,
                    DateTimeKind.Utc);
                
                if (now.Month == 12) // December
                {
                    expected = new DateTime(
                        now.Year + 1,
                        1, // January
                        pastDay,
                        12, 0, 0,
                        DateTimeKind.Utc);
                }

                Assert.That(result?.Year, Is.EqualTo(expected.Year));
                Assert.That(result?.Month, Is.EqualTo(expected.Month));
                Assert.That(result?.Day, Is.EqualTo(expected.Day));
                Assert.That(result?.Hour, Is.EqualTo(expected.Hour));
            }

            // Test for reset date before subscription period start
            {
                var now = DateTime.UtcNow;
                var company = TestHelpers.CreateTestCompany();

                var periodStart = now.AddMonths(-6);
                // Set a recent subscription start date (10 days ago)
                company.Subscription = new Subscription
                {
                    Id = "test-subscription",
                    PeriodStart = periodStart,
                    PeriodEnd = now.AddDays(10)
                };

                var result = Metrics.GetCurrentMetricPeriodStartForCompanyBillingSubscription(company);
                Assert.That(result, Is.Not.Null);

                // The result should be the period start date
                Assert.That(result?.Year, Is.EqualTo(company.Subscription.PeriodEnd.Year));
                Assert.That(result?.Month, Is.EqualTo(company.Subscription.PeriodEnd.Month));
                Assert.That(result?.Day, Is.EqualTo(company.Subscription.PeriodEnd.Day));
                Assert.That(result?.Hour, Is.EqualTo(company.Subscription.PeriodEnd.Hour));
            }
        }

        [Test]
        public void TestGetNextMetricPeriodStartForCalendarMetricPeriod()
        {
            // Test for CurrentDay
            {
                var result = Metrics.GetNextMetricPeriodStartForCalendarMetricPeriod(ConditionMetricPeriod.CurrentDay);
                Assert.That(result, Is.Not.Null);

                var expected = DateTime.UtcNow.Date.AddDays(1);
                Assert.That(result?.Year, Is.EqualTo(expected.Year));
                Assert.That(result?.Month, Is.EqualTo(expected.Month));
                Assert.That(result?.Day, Is.EqualTo(expected.Day));
                Assert.That(result?.Hour, Is.EqualTo(0));
                Assert.That(result?.Minute, Is.EqualTo(0));
                Assert.That(result?.Second, Is.EqualTo(0));
            }

            // Test for CurrentWeek
            {
                var result = Metrics.GetNextMetricPeriodStartForCalendarMetricPeriod(ConditionMetricPeriod.CurrentWeek);
                Assert.That(result, Is.Not.Null);

                var now = DateTime.UtcNow;
                int daysUntilMonday = ((int)DayOfWeek.Monday - (int)now.DayOfWeek + 7) % 7;
                if (daysUntilMonday == 0)
                    daysUntilMonday = 7; // If today is Monday, get next Monday
                var expected = now.Date.AddDays(daysUntilMonday);
                Assert.That(result?.Year, Is.EqualTo(expected.Year));
                Assert.That(result?.Month, Is.EqualTo(expected.Month));
                Assert.That(result?.Day, Is.EqualTo(expected.Day));
                Assert.That(result?.Hour, Is.EqualTo(0));
                Assert.That(result?.Minute, Is.EqualTo(0));
                Assert.That(result?.Second, Is.EqualTo(0));
                Assert.That(result?.DayOfWeek, Is.EqualTo(DayOfWeek.Monday));
            }

            // Test for CurrentMonth
            {
                var result = Metrics.GetNextMetricPeriodStartForCalendarMetricPeriod(ConditionMetricPeriod.CurrentMonth);
                Assert.That(result, Is.Not.Null);

                var now = DateTime.UtcNow;
                var firstDayOfCurrentMonth = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
                var expected = firstDayOfCurrentMonth.AddMonths(1);
                Assert.That(result?.Year, Is.EqualTo(expected.Year));
                Assert.That(result?.Month, Is.EqualTo(expected.Month));
                Assert.That(result?.Day, Is.EqualTo(expected.Day));
                Assert.That(result?.Hour, Is.EqualTo(0));
                Assert.That(result?.Minute, Is.EqualTo(0));
                Assert.That(result?.Second, Is.EqualTo(0));
                Assert.That(result?.Day, Is.EqualTo(1));
            }

            // Test for AllTime
            {
                var result = Metrics.GetNextMetricPeriodStartForCalendarMetricPeriod(ConditionMetricPeriod.AllTime);
                Assert.That(result, Is.Null);
            }
        }

        [Test]
        public void TestGetNextMetricPeriodStartForCompanyBillingSubscription()
        {
            // Test for null company
            {
                var result = Metrics.GetNextMetricPeriodStartForCompanyBillingSubscription(null);
                Assert.That(result, Is.Not.Null);

                var now = DateTime.UtcNow;
                var firstDayOfCurrentMonth = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
                var expected = firstDayOfCurrentMonth.AddMonths(1);
                Assert.That(result?.Year, Is.EqualTo(expected.Year));
                Assert.That(result?.Month, Is.EqualTo(expected.Month));
                Assert.That(result?.Day, Is.EqualTo(1));
            }

            // Test for company with null subscription
            {
                var company = TestHelpers.CreateTestCompany();
                company.Subscription = null;
                var result = Metrics.GetNextMetricPeriodStartForCompanyBillingSubscription(company);
                Assert.That(result, Is.Not.Null);

                var now = DateTime.UtcNow;
                var firstDayOfCurrentMonth = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
                var expected = firstDayOfCurrentMonth.AddMonths(1);
                Assert.That(result?.Year, Is.EqualTo(expected.Year));
                Assert.That(result?.Month, Is.EqualTo(expected.Month));
                Assert.That(result?.Day, Is.EqualTo(1));
            }

            // Test for subscription period start in future
            {
                var company = TestHelpers.CreateTestCompany();
                var now = DateTime.UtcNow;

                // Set subscription to start 7 days from now
                company.Subscription = new Subscription
                {
                    Id = "test-subscription",
                    PeriodStart = now.AddDays(7),
                    PeriodEnd = now.AddDays(37)
                };

                var result = Metrics.GetNextMetricPeriodStartForCompanyBillingSubscription(company);
                Assert.That(result, Is.Not.Null);

                // If period start is sooner than next month, period start should be used
                var firstDayOfCurrentMonth = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
                var startOfNextMonth = firstDayOfCurrentMonth.AddMonths(1);

                if (company.Subscription.PeriodStart > startOfNextMonth)
                {
                    // Period start is after next month, so next month should be used
                    Assert.That(result, Is.EqualTo(startOfNextMonth));
                }
                else
                {
                    // Period start is before next month, so period start should be used
                    Assert.That(result, Is.EqualTo(company.Subscription.PeriodStart));
                }
            }

            // Test for next reset date after subscription end
            {
                var company = TestHelpers.CreateTestCompany();
                var now = DateTime.UtcNow;

                // Set subscription to have started some time ago
                company.Subscription = new Subscription
                {
                    Id = "test-subscription",
                    PeriodStart = now.AddMonths(-6),
                    PeriodEnd = now.AddDays(10) // But set it to end soon (before the next monthly reset)
                };

                var result = Metrics.GetNextMetricPeriodStartForCompanyBillingSubscription(company);
                Assert.That(result, Is.Not.Null);

                // The result should be the period end date
                Assert.That(result, Is.EqualTo(company.Subscription.PeriodEnd));
            }

            // Test for current month reset date has passed
            {
                var now = DateTime.UtcNow;
                var company = TestHelpers.CreateTestCompany();

                // Set subscription to start on a day earlier in the month than today
                int pastDay = now.Day - 5;
                if (pastDay < 1)
                {
                    pastDay = 1;
                }

                company.Subscription = new Subscription
                {
                    Id = "test-subscription",
                    PeriodStart = new DateTime(
                        now.Year - 1,
                        now.Month,
                        pastDay,
                        12, 0, 0,
                        DateTimeKind.Utc),
                    PeriodEnd = now.AddYears(1) // Set end date far in the future
                };

                var result = Metrics.GetNextMetricPeriodStartForCompanyBillingSubscription(company);
                Assert.That(result, Is.Not.Null);

                // In this case, the result should be next month's reset date
                DateTime expected;
                if (now.Month == 12) // December
                {
                    expected = new DateTime(
                        now.Year + 1,
                        1, // January
                        pastDay,
                        12, 0, 0,
                        DateTimeKind.Utc);
                }
                else
                {
                    expected = new DateTime(
                        now.Year,
                        now.Month + 1,
                        Math.Min(pastDay, DateTime.DaysInMonth(now.Year, now.Month + 1)),
                        12, 0, 0,
                        DateTimeKind.Utc);
                }

                Assert.That(result?.Year, Is.EqualTo(expected.Year));
                Assert.That(result?.Month, Is.EqualTo(expected.Month));
                Assert.That(result?.Day, Is.EqualTo(expected.Day));
                Assert.That(result?.Hour, Is.EqualTo(expected.Hour));
            }

            // Test for current month reset date has not passed yet
            {
                var now = DateTime.UtcNow;
                var company = TestHelpers.CreateTestCompany();

                // Set subscription to start on a day later in the month than today
                int futureDay = now.Day + 5;
                if (futureDay > 28) // Avoid month boundary issues
                {
                    futureDay = 28;
                }

                company.Subscription = new Subscription
                {
                    Id = "test-subscription",
                    PeriodStart = new DateTime(
                        now.Year - 1,
                        now.Month,
                        futureDay,
                        12, 0, 0,
                        DateTimeKind.Utc),
                    PeriodEnd = now.AddYears(1) // Set end date far in the future
                };

                var result = Metrics.GetNextMetricPeriodStartForCompanyBillingSubscription(company);
                Assert.That(result, Is.Not.Null);

                DateTime expected;
                if (now.Day < 15)
                {
                  // In this case, the result should be this month's reset date
                  expected = new DateTime(
                              now.Year,
                              now.Month,
                              futureDay,
                              12, 0, 0,
                              DateTimeKind.Utc);
                }
                else
                {
                   expected = new DateTime(
                    now.Year,
                    now.Month + 1,
                    futureDay,
                    12, 0, 0,
                    DateTimeKind.Utc);

                      if (now.Month == 12)
                      {
                          expected = new DateTime(
                              now.Year + 1,
                              1, // January
                              futureDay,
                              12, 0, 0,
                              DateTimeKind.Utc);
                      }
                }

                Assert.That(result?.Year, Is.EqualTo(expected.Year));
                Assert.That(result?.Month, Is.EqualTo(expected.Month));
                Assert.That(result?.Day, Is.EqualTo(expected.Day));
                Assert.That(result?.Hour, Is.EqualTo(expected.Hour));
            }
        }

        [Test]
        public void TestGetNextMetricPeriodStartFromCondition()
        {
            // Test for null condition
            {
                var result = Metrics.GetNextMetricPeriodStartFromCondition(null, null);
                Assert.That(result, Is.Null);
            }

            // Test for condition that is not metric type
            {
                var condition = TestHelpers.CreateTestCondition(ConditionConditionType.Trait);
                var result = Metrics.GetNextMetricPeriodStartFromCondition(condition, null);
                Assert.That(result, Is.Null);
            }

            // Test for metric period is null
            {
                var condition = TestHelpers.CreateTestCondition(ConditionConditionType.Metric);
                condition.MetricPeriod = null;
                var result = Metrics.GetNextMetricPeriodStartFromCondition(condition, null);
                Assert.That(result, Is.Null);
            }

            // Test for metric period is all time
            {
                var condition = TestHelpers.CreateTestCondition(ConditionConditionType.Metric);
                condition.MetricPeriod = ConditionMetricPeriod.AllTime;
                var result = Metrics.GetNextMetricPeriodStartFromCondition(condition, null);
                Assert.That(result, Is.Null);
            }

            // Test for metric period is current month with billing cycle reset
            {
                var company = TestHelpers.CreateTestCompany();
                company.Subscription = new Subscription
                {
                    Id = "test-subscription",
                    PeriodStart = DateTime.UtcNow.AddMonths(-1),
                    PeriodEnd = DateTime.UtcNow.AddMonths(1)
                };

                var condition = TestHelpers.CreateTestCondition(ConditionConditionType.Metric);
                condition.MetricPeriod = ConditionMetricPeriod.CurrentMonth;
                condition.MetricPeriodMonthReset = ConditionMetricPeriodMonthReset.BillingCycle;

                var result = Metrics.GetNextMetricPeriodStartFromCondition(condition, company);
                var expected = Metrics.GetNextMetricPeriodStartForCompanyBillingSubscription(company);

                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.EqualTo(expected));
            }

            // Test for metric period is calendar-based
            {
                var condition = TestHelpers.CreateTestCondition(ConditionConditionType.Metric);
                condition.MetricPeriod = ConditionMetricPeriod.CurrentDay;
                
                var result = Metrics.GetNextMetricPeriodStartFromCondition(condition, null);
                var expected = Metrics.GetNextMetricPeriodStartForCalendarMetricPeriod(ConditionMetricPeriod.CurrentDay);

                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.EqualTo(expected));
            }
        }
    }
}