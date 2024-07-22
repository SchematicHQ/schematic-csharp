using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<UpdateComponentRequestBodyState>))]
public enum UpdateComponentRequestBodyState
{
    [EnumMember(Value = "draft")]
    Draft,

    [EnumMember(Value = "live")]
    Live
}
