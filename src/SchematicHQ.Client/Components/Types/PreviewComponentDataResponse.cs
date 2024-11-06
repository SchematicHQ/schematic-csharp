using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record PreviewComponentDataResponse
{
    [JsonPropertyName("data")]
    public required ComponentPreviewResponseData Data { get; set; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required PreviewComponentDataParams Params { get; set; }
}
