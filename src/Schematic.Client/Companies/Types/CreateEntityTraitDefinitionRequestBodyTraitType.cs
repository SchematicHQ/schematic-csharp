using System.Runtime.Serialization;

namespace Schematic.Client;

public enum CreateEntityTraitDefinitionRequestBodyTraitType
{
    [EnumMember(Value = "boolean")]
    Boolean,

    [EnumMember(Value = "currency")]
    Currency,

    [EnumMember(Value = "date")]
    Date,

    [EnumMember(Value = "number")]
    Number,

    [EnumMember(Value = "string")]
    String,

    [EnumMember(Value = "url")]
    Url
}
