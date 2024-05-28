using System.Runtime.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public enum CreateEventRequestBodyEventType
{
    [EnumMember(Value = "identify")]
    Identify,

    [EnumMember(Value = "track")]
    Track
}
