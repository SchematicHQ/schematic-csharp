using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(EnumSerializer<UpsertBillingProductRequestBodyChargeType>))]
public enum UpsertBillingProductRequestBodyChargeType
{
    [EnumMember(Value = "one_time")]
    OneTime,

    [EnumMember(Value = "recurring")]
    Recurring,

    [EnumMember(Value = "free")]
    Free,
}
