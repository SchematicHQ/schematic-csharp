using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class CheckFlagResponse
{
    [JsonPropertyName("data")]
    public CheckFlagResponseData Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public Dictionary<string, object> Params { get; init; }
}
