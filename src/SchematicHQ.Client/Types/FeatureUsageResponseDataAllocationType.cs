using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(EnumSerializer<FeatureUsageResponseDataAllocationType>))]
public enum FeatureUsageResponseDataAllocationType
{
    [EnumMember(Value = "boolean")]
    Boolean,

    [EnumMember(Value = "numeric")]
    Numeric,

    [EnumMember(Value = "trait")]
    Trait,

    [EnumMember(Value = "unlimited")]
    Unlimited,
}
