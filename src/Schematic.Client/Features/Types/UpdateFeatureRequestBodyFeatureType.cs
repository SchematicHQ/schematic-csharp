using System.Runtime.Serialization;

namespace Schematic.Client;

public enum UpdateFeatureRequestBodyFeatureType
{
    [EnumMember(Value = "boolean")]
    Boolean,

    [EnumMember(Value = "event")]
    Event,

    [EnumMember(Value = "trait")]
    Trait
}
