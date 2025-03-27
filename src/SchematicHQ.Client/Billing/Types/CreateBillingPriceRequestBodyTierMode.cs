using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(EnumSerializer<CreateBillingPriceRequestBodyTierMode>))]
public enum CreateBillingPriceRequestBodyTierMode
{
    [EnumMember(Value = "volume")]
    Volume,

    [EnumMember(Value = "graduated")]
    Graduated,
}
