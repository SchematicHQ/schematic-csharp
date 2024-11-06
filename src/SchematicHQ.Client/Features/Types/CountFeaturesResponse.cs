using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CountFeaturesResponse
{
    [JsonPropertyName("data")]
    public required CountResponse Data { get; set; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required CountFeaturesParams Params { get; set; }
}
