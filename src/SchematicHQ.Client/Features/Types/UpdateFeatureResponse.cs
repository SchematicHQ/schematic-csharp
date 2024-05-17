using System.Text.Json.Serialization;
using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class UpdateFeatureResponse
{
    [JsonPropertyName("data")]
    public FeatureDetailResponseData Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public Dictionary<string, object> Params { get; init; }
}