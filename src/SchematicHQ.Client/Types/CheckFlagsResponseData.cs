using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public class CheckFlagsResponseData
{
    [JsonPropertyName("flags")]
    public IEnumerable<CheckFlagOutputWithFlagKey> Flags { get; init; }
}
