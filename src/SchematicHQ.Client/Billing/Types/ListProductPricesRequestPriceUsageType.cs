using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(EnumSerializer<ListProductPricesRequestPriceUsageType>))]
public enum ListProductPricesRequestPriceUsageType
{
    [EnumMember(Value = "licensed")]
    Licensed,

    [EnumMember(Value = "metered")]
    Metered,
}
