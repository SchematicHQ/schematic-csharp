using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CreateFeatureResponse
{
    [JsonPropertyName("data")]
    public required FeatureDetailResponseData Data { get; set; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public Dictionary<string, object?> Params { get; set; } = new Dictionary<string, object?>();
}
