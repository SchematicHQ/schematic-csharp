using System.Runtime.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public enum CreateFeatureRequestBodyFeatureType
{
    [EnumMember(Value = "boolean")]
    Boolean,

    [EnumMember(Value = "event")]
    Event,

    [EnumMember(Value = "trait")]
    Trait
}
