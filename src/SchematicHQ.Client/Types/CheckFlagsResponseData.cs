using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CheckFlagsResponseData
{
    [JsonPropertyName("flags")]
    public IEnumerable<CheckFlagOutputWithFlagKey> Flags { get; set; } =
        new List<CheckFlagOutputWithFlagKey>();
}
