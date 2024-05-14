using System.Runtime.Serialization;

namespace Schematic.Client;

public enum CreateOrUpdateConditionRequestBodyOperator
{
    [EnumMember(Value = "eq")]
    Eq,

    [EnumMember(Value = "ne")]
    Ne,

    [EnumMember(Value = "gt")]
    Gt,

    [EnumMember(Value = "gte")]
    Gte,

    [EnumMember(Value = "lt")]
    Lt,

    [EnumMember(Value = "lte")]
    Lte
}
