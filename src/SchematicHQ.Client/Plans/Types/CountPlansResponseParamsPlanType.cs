using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(EnumSerializer<CountPlansResponseParamsPlanType>))]
public enum CountPlansResponseParamsPlanType
{
    [EnumMember(Value = "plan")]
    Plan,

    [EnumMember(Value = "add_on")]
    AddOn,
}
