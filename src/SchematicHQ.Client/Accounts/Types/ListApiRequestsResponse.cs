using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListApiRequestsResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<ApiKeyRequestListResponseData> Data { get; set; } =
        new List<ApiKeyRequestListResponseData>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required ListApiRequestsParams Params { get; set; }
}
