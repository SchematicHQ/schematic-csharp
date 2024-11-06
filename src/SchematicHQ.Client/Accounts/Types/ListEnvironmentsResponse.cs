using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListEnvironmentsResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<EnvironmentResponseData> Data { get; set; } =
        new List<EnvironmentResponseData>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required ListEnvironmentsParams Params { get; set; }
}
