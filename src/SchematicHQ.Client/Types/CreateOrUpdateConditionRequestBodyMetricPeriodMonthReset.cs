using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

[JsonConverter(typeof(EnumSerializer<CreateOrUpdateConditionRequestBodyMetricPeriodMonthReset>))]
public enum CreateOrUpdateConditionRequestBodyMetricPeriodMonthReset
{
    [EnumMember(Value = "first_of_month")]
    FirstOfMonth,

    [EnumMember(Value = "billing_cycle")]
    BillingCycle,
}
