using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListComponentsResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<ComponentResponseData> Data { get; set; } =
        new List<ComponentResponseData>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required ListComponentsParams Params { get; set; }
}
