using System.Runtime.Serialization;

namespace SchematicHQ.Client;

public enum CreateEntityTraitDefinitionRequestBodyEntityType
{
    [EnumMember(Value = "company")]
    Company,

    [EnumMember(Value = "user")]
    User
}
