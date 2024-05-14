using System.Runtime.Serialization;

namespace Schematic.Client;

public enum CreateOrUpdateConditionRequestBodyConditionType
{
    [EnumMember(Value = "company")]
    Company,

    [EnumMember(Value = "metric")]
    Metric,

    [EnumMember(Value = "trait")]
    Trait,

    [EnumMember(Value = "user")]
    User,

    [EnumMember(Value = "plan")]
    Plan,

    [EnumMember(Value = "billing_product")]
    BillingProduct
}
