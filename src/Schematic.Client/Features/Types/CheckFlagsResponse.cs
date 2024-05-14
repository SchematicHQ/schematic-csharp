using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class CheckFlagsResponse
{
    [JsonPropertyName("data")]
    public CheckFlagsResponseData Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public Dictionary<string, object> Params { get; init; }
}
