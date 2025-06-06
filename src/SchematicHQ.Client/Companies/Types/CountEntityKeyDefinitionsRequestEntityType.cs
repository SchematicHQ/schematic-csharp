using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(EnumSerializer<CountEntityKeyDefinitionsRequestEntityType>))]
public enum CountEntityKeyDefinitionsRequestEntityType
{
    [EnumMember(Value = "company")]
    Company,

    [EnumMember(Value = "user")]
    User,
}
