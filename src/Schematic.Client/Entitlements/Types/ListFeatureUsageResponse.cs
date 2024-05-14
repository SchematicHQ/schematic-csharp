using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class ListFeatureUsageResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public List<FeatureUsageResponseData> Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public ListFeatureUsageParams Params { get; init; }
}
