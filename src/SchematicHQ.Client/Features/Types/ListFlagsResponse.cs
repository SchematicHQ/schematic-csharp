using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListFlagsResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<FlagDetailResponseData> Data { get; set; } =
        new List<FlagDetailResponseData>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required ListFlagsParams Params { get; set; }
}
