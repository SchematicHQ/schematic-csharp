using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ListEntityKeyDefinitionsRequestEntityType>))]
public enum ListEntityKeyDefinitionsRequestEntityType
{
    [EnumMember(Value = "company")]
    Company,

    [EnumMember(Value = "user")]
    User,
}
