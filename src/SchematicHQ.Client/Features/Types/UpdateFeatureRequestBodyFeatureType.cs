using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<UpdateFeatureRequestBodyFeatureType>))]
public enum UpdateFeatureRequestBodyFeatureType
{
    [EnumMember(Value = "boolean")]
    Boolean,

    [EnumMember(Value = "event")]
    Event,

    [EnumMember(Value = "trait")]
    Trait
}
