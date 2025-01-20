using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

[JsonConverter(typeof(EnumSerializer<CreatePlanRequestBodyPlanType>))]
public enum CreatePlanRequestBodyPlanType
{
    [EnumMember(Value = "plan")]
    Plan,

    [EnumMember(Value = "add_on")]
    AddOn,
}
