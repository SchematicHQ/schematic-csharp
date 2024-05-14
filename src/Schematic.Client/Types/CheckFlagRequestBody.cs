using System.Text.Json.Serialization;

namespace Schematic.Client;

public class CheckFlagRequestBody
{
    [JsonPropertyName("company")]
    public Dictionary<string, object>? Company { get; init; }

    [JsonPropertyName("user")]
    public Dictionary<string, object>? User { get; init; }
}
