using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class ListFeaturesResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public List<FeatureDetailResponseData> Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public ListFeaturesParams Params { get; init; }
}
