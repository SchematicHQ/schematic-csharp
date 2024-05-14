using System.Runtime.Serialization;

namespace Schematic.Client;

public enum CreateEntityTraitDefinitionRequestBodyEntityType
{
    [EnumMember(Value = "company")]
    Company,

    [EnumMember(Value = "user")]
    User
}
