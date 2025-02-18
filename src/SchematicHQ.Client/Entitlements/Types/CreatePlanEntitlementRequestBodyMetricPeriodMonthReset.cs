using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(EnumSerializer<CreatePlanEntitlementRequestBodyMetricPeriodMonthReset>))]
public enum CreatePlanEntitlementRequestBodyMetricPeriodMonthReset
{
    [EnumMember(Value = "first_of_month")]
    FirstOfMonth,

    [EnumMember(Value = "billing_cycle")]
    BillingCycle,
}
