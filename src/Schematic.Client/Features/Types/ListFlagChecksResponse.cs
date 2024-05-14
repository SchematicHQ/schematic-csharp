using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class ListFlagChecksResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public List<FlagCheckLogDetailResponseData> Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public ListFlagChecksParams Params { get; init; }
}
