using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record GetEventSummariesResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<EventSummaryResponseData> Data { get; set; } =
        new List<EventSummaryResponseData>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required GetEventSummariesParams Params { get; set; }
}
