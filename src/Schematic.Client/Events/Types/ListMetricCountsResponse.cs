using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class ListMetricCountsResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public List<MetricCountsHourlyResponseData> Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public ListMetricCountsParams Params { get; init; }
}
