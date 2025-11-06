using SchematicHQ.Client.RulesEngine.Models;

namespace SchematicHQ.Client.RulesEngine
{
  public static class Metrics
  {

    public static DateTime? GetCurrentMetricPeriodStartForCalendarMetricPeriod(ConditionMetricPeriod? metricPeriod)
    {
      if (metricPeriod == null) return null;

      var now = DateTime.UtcNow;

      if (metricPeriod == ConditionMetricPeriod.CurrentDay)
        // UTC midnight for the current day
        return now.Date;

      if (metricPeriod == ConditionMetricPeriod.CurrentWeek)
      {
        // UTC midnight for the current week's Monday
        int daysSinceMonday = ((int)now.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
        return now.Date.AddDays(-daysSinceMonday);
      }

      if (metricPeriod == ConditionMetricPeriod.CurrentMonth)
        // UTC midnight for the first day of current month
        return new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);

      return null;
    }

    /// <summary>
    /// Given a company, determine the current metric period start based on the company's billing subscription.
    /// </summary>
    public static DateTime? GetCurrentMetricPeriodStartForCompanyBillingSubscription(Models.Company? company)
    {
      // If no subscription exists, we use calendar month reset
      if (company == null || company.Subscription == null)
      {
        return GetCurrentMetricPeriodStartForCalendarMetricPeriod(ConditionMetricPeriod.CurrentMonth);
      }

      var now = DateTime.UtcNow;
      var periodStart = company.Subscription.PeriodStart;
      var periodEnd = company.Subscription.PeriodEnd;

      // If the start period is in the future, the metric period is from the start of the current calendar month until either
      // the end of the current calendar month or the start of the billing period, whichever comes first
      if (periodStart > now)
      {
        DateTime? startOfNextMonth = GetCurrentMetricPeriodStartForCalendarMetricPeriod(ConditionMetricPeriod.CurrentMonth);
        if (periodStart > startOfNextMonth)
        {
          return startOfNextMonth;
        }
        return periodStart;
      }

      // Month metric period will reset on the same day/hour/minute/second as the subscription started every month; get that timestamp for the current month
      var currentPeriodStart = new DateTime(
          now.Year,
          now.Month,
          Math.Min(periodStart.Day, DateTime.DaysInMonth(now.Year, now.Month)),
          periodStart.Hour,
          periodStart.Minute,
          periodStart.Second,
          periodStart.Millisecond,
          DateTimeKind.Utc);

      // If we've already passed this month's reset date, move to next month
      if (currentPeriodStart < now)
      {
        currentPeriodStart = currentPeriodStart.AddMonths(1);

        // Handle day adjustment when going back from 30/31 to a month with fewer days
        var daysInMonth = DateTime.DaysInMonth(currentPeriodStart.Year, currentPeriodStart.Month);
        if (periodStart.Day > daysInMonth)
        {
          currentPeriodStart = new DateTime(
              currentPeriodStart.Year,
              currentPeriodStart.Month,
              daysInMonth,
              periodStart.Hour,
              periodStart.Minute,
              periodStart.Second,
              periodStart.Millisecond,
              DateTimeKind.Utc);
        }
      }

      // If the next reset is after the end of the billing period, use the end of the billing period instead
      if (currentPeriodStart > periodEnd)
      {
        return periodEnd;
      }

      return currentPeriodStart;
    }

    public static DateTime? GetNextMetricPeriodStartForCalendarMetricPeriod(ConditionMetricPeriod? metricPeriod)
    {
      if (metricPeriod == null) return null;

      if (metricPeriod == ConditionMetricPeriod.CurrentDay)
        // UTC midnight for upcoming day
        return DateTime.UtcNow.Date.AddDays(1);

      if (metricPeriod == ConditionMetricPeriod.CurrentWeek)
      {
        // UTC midnight for upcoming Monday (C# uses Monday as first day, Go example used Sunday)
        var now = DateTime.UtcNow;
        int daysUntilMonday = ((int)DayOfWeek.Monday - (int)now.DayOfWeek + 7) % 7;
        if (daysUntilMonday == 0)
          daysUntilMonday = 7; // If today is Monday, get next Monday
        return now.Date.AddDays(daysUntilMonday);
      }

      if (metricPeriod == ConditionMetricPeriod.CurrentMonth)
      {
        // UTC midnight for the first day of next month
        var currentDate = DateTime.UtcNow;
        return new DateTime(currentDate.Year, currentDate.Month, 1, 0, 0, 0, DateTimeKind.Utc).AddMonths(1);
      }

      return null;
    }
    

    /// <summary>
    /// Given a company, determine the next metric period start based on the company's billing subscription.
    /// </summary>
    public static DateTime? GetNextMetricPeriodStartForCompanyBillingSubscription(Models.Company? company)
    {
      // If no subscription exists, we use calendar month reset
      if (company == null || company.Subscription == null)
      {
        return GetNextMetricPeriodStartForCalendarMetricPeriod(ConditionMetricPeriod.CurrentMonth);
      }

      var now = DateTime.UtcNow;
      var periodEnd = company.Subscription.PeriodEnd;
      var periodStart = company.Subscription.PeriodStart;

      // If the start period is in the future, the metric period is from the start of the current calendar month until either
      // the end of the current calendar month or the start of the billing period, whichever comes first
      if (periodStart > now)
      {
        var startOfNextMonth = GetNextMetricPeriodStartForCalendarMetricPeriod(ConditionMetricPeriod.CurrentMonth);
        if (periodStart > startOfNextMonth)
        {
          return startOfNextMonth;
        }

        return periodStart;
      }

      // Month metric period will reset on the same day/hour/minute/second as the subscription started every month;
      // Get that timestamp for the current month
      var nextReset = new DateTime(
          now.Year,
          now.Month,
          periodStart.Day,
          periodStart.Hour,
          periodStart.Minute,
          periodStart.Second,
          periodStart.Millisecond,
          DateTimeKind.Utc);

      // If we've already passed this month's reset date, move to next month
      if (nextReset <= now)
      {
        nextReset = nextReset.AddMonths(1);
      }

      // If the next reset is after the end of the billing period, use the end of the billing period instead
      if (nextReset > periodEnd)
      {
        return periodEnd;
      }

      return nextReset;
    }

    /// <summary>
    /// Given a rule condition and a company, determine the next metric period start.
    /// Will return null if the condition is not a metric condition.
    /// </summary>
    public static DateTime? GetNextMetricPeriodStartFromCondition(Condition? condition, Models.Company? company)
    {
      // Only metric conditions have a metric period that can reset
      if (condition == null || condition.ConditionType != ConditionConditionType.Metric || condition.MetricPeriod == null)
      {
        return null;
      }

      var metricPeriod = condition.MetricPeriod;

      // If the metric period is all-time, no reset
      if (metricPeriod == ConditionMetricPeriod.AllTime)
      {
        return null;
      }

      // Metric period current month with billing cycle reset
      if (metricPeriod == ConditionMetricPeriod.CurrentMonth &&
          condition.MetricPeriodMonthReset.HasValue &&
          condition.MetricPeriodMonthReset == ConditionMetricPeriodMonthReset.BillingCycle)
      {
        return GetNextMetricPeriodStartForCompanyBillingSubscription(company);
      }

      // Calendar-based metric periods
      return GetNextMetricPeriodStartForCalendarMetricPeriod(metricPeriod);
    }
  }
}