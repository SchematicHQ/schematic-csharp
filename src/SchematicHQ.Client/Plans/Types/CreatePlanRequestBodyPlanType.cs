using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CreatePlanRequestBodyPlanType>))]
public enum CreatePlanRequestBodyPlanType
{
    [EnumMember(Value = "product")]
    Product,

    [EnumMember(Value = "pricing_plan")]
    PricingPlan,

    [EnumMember(Value = "add_on")]
    AddOn,

    [EnumMember(Value = "overage")]
    Overage,

    [EnumMember(Value = "billable_metric")]
    BillableMetric,

    [EnumMember(Value = "other")]
    Other
}
