using System.Runtime.Serialization;

namespace SchematicHQ.Client;

public enum UpdateCompanyOverrideRequestBodyValueType
{
    [EnumMember(Value = "Boolean")]
    Boolean,

    [EnumMember(Value = "Numeric")]
    Numeric,

    [EnumMember(Value = "Trait")]
    Trait,

    [EnumMember(Value = "Unlimited")]
    Unlimited
}