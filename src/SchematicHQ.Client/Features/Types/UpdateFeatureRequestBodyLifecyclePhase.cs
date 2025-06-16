using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(EnumSerializer<UpdateFeatureRequestBodyLifecyclePhase>))]
public enum UpdateFeatureRequestBodyLifecyclePhase
{
    [EnumMember(Value = "add_on")]
    AddOn,

    [EnumMember(Value = "alpha")]
    Alpha,

    [EnumMember(Value = "beta")]
    Beta,

    [EnumMember(Value = "deprecated")]
    Deprecated,

    [EnumMember(Value = "ga")]
    Ga,

    [EnumMember(Value = "in_plan")]
    InPlan,

    [EnumMember(Value = "inactive")]
    Inactive,

    [EnumMember(Value = "internal_testing")]
    InternalTesting,

    [EnumMember(Value = "legacy")]
    Legacy,
}
