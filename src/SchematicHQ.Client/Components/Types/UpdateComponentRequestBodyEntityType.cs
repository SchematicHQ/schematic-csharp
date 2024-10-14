using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<UpdateComponentRequestBodyEntityType>))]
public enum UpdateComponentRequestBodyEntityType
{
    [EnumMember(Value = "entitlement")]
    Entitlement,

    [EnumMember(Value = "billing")]
    Billing,
}
