using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ListEntityTraitDefinitionsResponseParamsTraitType>))]
public enum ListEntityTraitDefinitionsResponseParamsTraitType
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
