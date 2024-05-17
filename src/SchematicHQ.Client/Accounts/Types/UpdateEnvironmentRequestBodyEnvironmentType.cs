using System.Runtime.Serialization;

namespace SchematicHQ.Client;

public enum UpdateEnvironmentRequestBodyEnvironmentType
{
    [EnumMember(Value = "development")]
    Development,

    [EnumMember(Value = "staging")]
    Staging,

    [EnumMember(Value = "production")]
    Production
}
