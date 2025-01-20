using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record PreviewComponentDataResponse
{
    [JsonPropertyName("data")]
    public required ComponentPreviewResponseData Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required PreviewComponentDataParams Params { get; init; }
}
