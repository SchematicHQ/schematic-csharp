using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

[JsonConverter(typeof(EnumSerializer<UpdateCompanyOverrideRequestBodyMetricPeriod>))]
public enum UpdateCompanyOverrideRequestBodyMetricPeriod
{
    [EnumMember(Value = "all_time")]
    AllTime,

    [EnumMember(Value = "current_month")]
    CurrentMonth,

    [EnumMember(Value = "current_week")]
    CurrentWeek,

    [EnumMember(Value = "current_day")]
    CurrentDay,
}
