using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(EnumSerializer<FeatureCompanyUserResponseDataAllocationType>))]
public enum FeatureCompanyUserResponseDataAllocationType
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
