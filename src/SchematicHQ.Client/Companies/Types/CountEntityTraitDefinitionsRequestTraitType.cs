using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(EnumSerializer<CountEntityTraitDefinitionsRequestTraitType>))]
public enum CountEntityTraitDefinitionsRequestTraitType
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
    Url,
}
