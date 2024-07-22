using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record CheckFlagsResponseData
{
    [JsonPropertyName("flags")]
    public IEnumerable<CheckFlagOutputWithFlagKey> Flags { get; init; } =
        new List<CheckFlagOutputWithFlagKey>();
}
