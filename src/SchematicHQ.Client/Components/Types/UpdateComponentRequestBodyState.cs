using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(EnumSerializer<UpdateComponentRequestBodyState>))]
public enum UpdateComponentRequestBodyState
{
    [EnumMember(Value = "draft")]
    Draft,

    [EnumMember(Value = "live")]
    Live,
}
