using System.Runtime.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public enum CreatePlanEntitlementRequestBodyMetricPeriod
{
    [EnumMember(Value = "current_month")]
    CurrentMonth,

    [EnumMember(Value = "current_week")]
    CurrentWeek,

    [EnumMember(Value = "current_day")]
    CurrentDay
}
