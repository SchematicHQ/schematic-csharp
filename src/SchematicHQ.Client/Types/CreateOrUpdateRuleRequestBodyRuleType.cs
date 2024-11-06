using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CreateOrUpdateRuleRequestBodyRuleType>))]
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
    PlanAudience
}
