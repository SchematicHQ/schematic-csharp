using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

[JsonConverter(typeof(EnumSerializer<UpdateEnvironmentRequestBodyEnvironmentType>))]
public enum UpdateEnvironmentRequestBodyEnvironmentType
{
    [EnumMember(Value = "development")]
    Development,

    [EnumMember(Value = "staging")]
    Staging,

    [EnumMember(Value = "production")]
    Production,
}
