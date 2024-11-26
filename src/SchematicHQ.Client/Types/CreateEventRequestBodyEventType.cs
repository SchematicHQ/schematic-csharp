using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

[JsonConverter(typeof(EnumSerializer<CreateEventRequestBodyEventType>))]
public enum CreateEventRequestBodyEventType
{
    [EnumMember(Value = "identify")]
    Identify,

    [EnumMember(Value = "track")]
    Track,

    [EnumMember(Value = "flag_check")]
    FlagCheck,
}
