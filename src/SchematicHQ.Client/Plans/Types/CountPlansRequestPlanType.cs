using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(EnumSerializer<CountPlansRequestPlanType>))]
public enum CountPlansRequestPlanType
{
    [EnumMember(Value = "plan")]
    Plan,

    [EnumMember(Value = "add_on")]
    AddOn,
}
