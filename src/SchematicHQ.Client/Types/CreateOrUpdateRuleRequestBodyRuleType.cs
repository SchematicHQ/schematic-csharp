using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(EnumSerializer<CreateOrUpdateRuleRequestBodyRuleType>))]
public enum CreateOrUpdateRuleRequestBodyRuleType
{
    [EnumMember(Value = "global_override")]
    GlobalOverride,

    [EnumMember(Value = "company_override")]
    CompanyOverride,

    [EnumMember(Value = "plan_entitlement")]
    PlanEntitlement,

    [EnumMember(Value = "standard")]
    Standard,

    [EnumMember(Value = "default")]
    Default,

    [EnumMember(Value = "plan_audience")]
    PlanAudience,
}
