using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public class CheckFlagsResponseData
{
    [JsonPropertyName("flags")]
    public List<CheckFlagOutputWithFlagKey> Flags { get; init; }
}
