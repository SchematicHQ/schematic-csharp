using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListFeatureUsageResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<FeatureUsageResponseData> Data { get; set; } =
        new List<FeatureUsageResponseData>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required ListFeatureUsageParams Params { get; set; }
}
