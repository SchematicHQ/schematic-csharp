using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(EnumSerializer<CreateOrUpdateConditionRequestBodyOperator>))]
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
    Lte,

    [EnumMember(Value = "is_empty")]
    IsEmpty,

    [EnumMember(Value = "not_empty")]
    NotEmpty,
}
