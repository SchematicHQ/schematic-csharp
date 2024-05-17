using System.Runtime.Serialization;

namespace SchematicHQ.Client;

public enum CreateEventRequestBodyEventType
{
    [EnumMember(Value = "identify")]
    Identify,

    [EnumMember(Value = "track")]
    Track
}
