using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class CheckFlagsResponseData
{
    [JsonPropertyName("flags")]
    public List<CheckFlagOutputWithFlagKey> Flags { get; init; }
}
