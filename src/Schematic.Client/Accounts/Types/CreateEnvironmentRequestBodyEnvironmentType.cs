using System.Runtime.Serialization;

namespace Schematic.Client;

public enum CreateEnvironmentRequestBodyEnvironmentType
{
    [EnumMember(Value = "development")]
    Development,

    [EnumMember(Value = "staging")]
    Staging,

    [EnumMember(Value = "production")]
    Production
}
