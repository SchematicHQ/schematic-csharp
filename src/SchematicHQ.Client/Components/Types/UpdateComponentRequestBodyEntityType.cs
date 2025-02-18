using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(EnumSerializer<UpdateComponentRequestBodyEntityType>))]
public enum UpdateComponentRequestBodyEntityType
{
    [EnumMember(Value = "entitlement")]
    Entitlement,

    [EnumMember(Value = "billing")]
    Billing,
}
