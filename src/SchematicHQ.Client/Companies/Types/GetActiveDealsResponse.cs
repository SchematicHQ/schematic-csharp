using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record GetActiveDealsResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<CompanyCrmDealsResponseData> Data { get; set; } =
        new List<CompanyCrmDealsResponseData>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required GetActiveDealsParams Params { get; set; }
}
