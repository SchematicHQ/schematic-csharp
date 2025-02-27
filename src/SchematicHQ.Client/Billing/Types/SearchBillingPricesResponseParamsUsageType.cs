using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(EnumSerializer<SearchBillingPricesResponseParamsUsageType>))]
public enum SearchBillingPricesResponseParamsUsageType
{
    [EnumMember(Value = "licensed")]
    Licensed,

    [EnumMember(Value = "metered")]
    Metered,
}
