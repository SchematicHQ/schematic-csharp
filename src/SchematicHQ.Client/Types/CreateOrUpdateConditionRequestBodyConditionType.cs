using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(EnumSerializer<CreateOrUpdateConditionRequestBodyConditionType>))]
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
    BillingProduct,

    [EnumMember(Value = "crm_product")]
    CrmProduct,

    [EnumMember(Value = "base_plan")]
    BasePlan,
}
