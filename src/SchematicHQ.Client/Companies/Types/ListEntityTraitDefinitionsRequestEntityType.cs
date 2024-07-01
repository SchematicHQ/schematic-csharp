using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ListEntityTraitDefinitionsRequestEntityType>))]
public enum ListEntityTraitDefinitionsRequestEntityType
{
    [EnumMember(Value = "company")]
    Company,

    [EnumMember(Value = "user")]
    User
}
