using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class CountFlagsResponse
{
    [JsonPropertyName("data")]
    public CountResponse Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public CountFlagsParams Params { get; init; }
}
