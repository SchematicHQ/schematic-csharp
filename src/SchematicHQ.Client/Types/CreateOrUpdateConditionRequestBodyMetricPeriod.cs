using System.Runtime.Serialization;

namespace SchematicHQ.Client;

public enum CreateOrUpdateConditionRequestBodyMetricPeriod
{
    [EnumMember(Value = "current_month")]
    CurrentMonth,

    [EnumMember(Value = "current_week")]
    CurrentWeek,

    [EnumMember(Value = "current_day")]
    CurrentDay
}
