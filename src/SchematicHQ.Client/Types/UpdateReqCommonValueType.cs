using System.Runtime.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public enum UpdateReqCommonValueType
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
