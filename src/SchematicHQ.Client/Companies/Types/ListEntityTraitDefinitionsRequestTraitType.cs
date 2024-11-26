using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

[JsonConverter(typeof(EnumSerializer<ListEntityTraitDefinitionsRequestTraitType>))]
public enum ListEntityTraitDefinitionsRequestTraitType
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
