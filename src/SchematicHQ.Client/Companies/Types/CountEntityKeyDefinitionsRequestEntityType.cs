using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CountEntityKeyDefinitionsRequestEntityType>))]
public enum CountEntityKeyDefinitionsRequestEntityType
{
    [EnumMember(Value = "company")]
    Company,

    [EnumMember(Value = "user")]
    User
}
