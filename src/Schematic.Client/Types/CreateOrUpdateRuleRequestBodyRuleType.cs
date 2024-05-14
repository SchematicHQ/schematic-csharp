using System.Runtime.Serialization;

namespace Schematic.Client;

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
