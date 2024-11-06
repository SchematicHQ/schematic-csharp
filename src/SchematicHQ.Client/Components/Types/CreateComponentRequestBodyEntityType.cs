using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CreateComponentRequestBodyEntityType>))]
public enum CreateComponentRequestBodyEntityType
{
    [EnumMember(Value = "entitlement")]
    Entitlement,

    [EnumMember(Value = "billing")]
    Billing
}
