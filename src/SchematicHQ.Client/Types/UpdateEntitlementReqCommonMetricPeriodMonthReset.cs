using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<UpdateEntitlementReqCommonMetricPeriodMonthReset>))]
public enum UpdateEntitlementReqCommonMetricPeriodMonthReset
{
    [EnumMember(Value = "first_of_month")]
    FirstOfMonth,

    [EnumMember(Value = "billing_cycle")]
    BillingCycle
}
