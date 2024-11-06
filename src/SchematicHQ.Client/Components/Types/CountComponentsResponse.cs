using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CountComponentsResponse
{
    [JsonPropertyName("data")]
    public required CountResponse Data { get; set; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required CountComponentsParams Params { get; set; }
}
