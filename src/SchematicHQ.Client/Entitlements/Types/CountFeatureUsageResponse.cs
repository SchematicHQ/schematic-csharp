using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CountFeatureUsageResponse
{
    [JsonPropertyName("data")]
    public required CountResponse Data { get; set; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required CountFeatureUsageParams Params { get; set; }
}
