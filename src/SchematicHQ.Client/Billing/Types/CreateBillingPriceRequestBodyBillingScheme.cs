using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(EnumSerializer<CreateBillingPriceRequestBodyBillingScheme>))]
public enum CreateBillingPriceRequestBodyBillingScheme
{
    [EnumMember(Value = "per_unit")]
    PerUnit,

    [EnumMember(Value = "tiered")]
    Tiered,
}
