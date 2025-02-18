using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(EnumSerializer<ListBillingProductsResponseParamsPriceUsageType>))]
public enum ListBillingProductsResponseParamsPriceUsageType
{
    [EnumMember(Value = "licensed")]
    Licensed,

    [EnumMember(Value = "metered")]
    Metered,
}
