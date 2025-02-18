using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(EnumSerializer<UpdateFeatureRequestBodyFeatureType>))]
public enum UpdateFeatureRequestBodyFeatureType
{
    [EnumMember(Value = "boolean")]
    Boolean,

    [EnumMember(Value = "event")]
    Event,

    [EnumMember(Value = "trait")]
    Trait,
}
