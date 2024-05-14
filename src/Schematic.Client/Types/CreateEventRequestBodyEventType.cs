using System.Runtime.Serialization;

namespace Schematic.Client;

public enum CreateEventRequestBodyEventType
{
    [EnumMember(Value = "identify")]
    Identify,

    [EnumMember(Value = "track")]
    Track
}
