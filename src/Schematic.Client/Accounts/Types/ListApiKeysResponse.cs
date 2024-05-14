using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class ListApiKeysResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public List<ApiKeyResponseData> Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public ListApiKeysParams Params { get; init; }
}
