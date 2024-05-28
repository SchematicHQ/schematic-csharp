using System.Runtime.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public enum CreateEntityTraitDefinitionRequestBodyEntityType
{
    [EnumMember(Value = "company")]
    Company,

    [EnumMember(Value = "user")]
    User
}
