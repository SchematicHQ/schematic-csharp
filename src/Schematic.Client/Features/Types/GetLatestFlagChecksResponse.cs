using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class GetLatestFlagChecksResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public List<FlagCheckLogResponseData> Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public GetLatestFlagChecksParams Params { get; init; }
}
