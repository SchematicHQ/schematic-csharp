using System.Runtime.Serialization;

namespace SchematicHQ.Client;

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
