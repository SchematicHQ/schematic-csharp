using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class GetEventSummariesResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public List<EventSummaryResponseData> Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public GetEventSummariesParams Params { get; init; }
}
