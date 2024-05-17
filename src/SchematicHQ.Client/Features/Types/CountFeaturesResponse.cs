using System.Text.Json.Serialization;
using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class CountFeaturesResponse
{
    [JsonPropertyName("data")]
    public CountResponse Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public CountFeaturesParams Params { get; init; }
}