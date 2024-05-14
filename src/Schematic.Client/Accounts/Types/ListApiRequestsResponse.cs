using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class ListApiRequestsResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public List<ApiKeyRequestListResponseData> Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public ListApiRequestsParams Params { get; init; }
}
