using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record ListMetricCountsResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<MetricCountsHourlyResponseData> Data { get; set; } =
        new List<MetricCountsHourlyResponseData>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required ListMetricCountsParams Params { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
