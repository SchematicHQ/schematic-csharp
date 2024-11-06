using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListApiKeysResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<ApiKeyResponseData> Data { get; set; } = new List<ApiKeyResponseData>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required ListApiKeysParams Params { get; set; }
}
