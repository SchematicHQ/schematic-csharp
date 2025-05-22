using SchematicHQ.RulesEngine.Models;
using SchematicHQ.RulesEngine.Utils;

namespace SchematicHQ.RulesEngine
{
  public static class Metrics
  {
    public static DateTime? GetNextMetricPeriodStartFromCondition(this Condition condition, Company company)
    {
      if (condition == null || condition.MetricPeriod == null)
      {
        return null;
      }

      var now = DateTime.UtcNow;
      var period = condition.MetricPeriod.Value;

      switch (period)
      {
        case MetricPeriod.CurrentDay:
          return now.Date.AddDays(1);
        case MetricPeriod.CurrentWeek:
          // Find next Monday
          int daysUntilMonday = ((int)DayOfWeek.Monday - (int)now.DayOfWeek + 7) % 7;
          if (daysUntilMonday == 0)
            daysUntilMonday = 7; // If today is Monday, get next Monday
          return now.Date.AddDays(daysUntilMonday);
        case MetricPeriod.CurrentMonth:
          if (condition.MetricPeriodMonthReset.HasValue)
          {
            var day = condition.MetricPeriodMonthReset.Value;
            var nextMonth = now.AddMonths(1);
            if (day > DateTime.DaysInMonth(nextMonth.Year, nextMonth.Month))
            {
              day = DateTime.DaysInMonth(nextMonth.Year, nextMonth.Month);
            }
            return new DateTime(nextMonth.Year, nextMonth.Month, day);
          }
          else
          {
            return new DateTime(now.Year, now.Month, 1).AddMonths(1);
          }
        default:
          return null;
      }
    }
  }
}