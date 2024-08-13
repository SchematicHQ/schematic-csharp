using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CountPlansResponseParamsPlanType>))]
public enum CountPlansResponseParamsPlanType
{
    [EnumMember(Value = "plan")]
    Plan,

    [EnumMember(Value = "add_on")]
    AddOn
}
