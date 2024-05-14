using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class ListFlagsResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public List<FlagDetailResponseData> Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public ListFlagsParams Params { get; init; }
}
