using System.Runtime.Serialization;

namespace SchematicHQ.Client;

public enum UpdateFeatureRequestBodyFeatureType
{
    [EnumMember(Value = "boolean")]
    Boolean,

    [EnumMember(Value = "event")]
    Event,

    [EnumMember(Value = "trait")]
    Trait
}
