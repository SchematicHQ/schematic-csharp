using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(EnumSerializer<ListEventsResponseParamsEventTypesItem>))]
public enum ListEventsResponseParamsEventTypesItem
{
    [EnumMember(Value = "identify")]
    Identify,

    [EnumMember(Value = "track")]
    Track,

    [EnumMember(Value = "flag_check")]
    FlagCheck,
}
