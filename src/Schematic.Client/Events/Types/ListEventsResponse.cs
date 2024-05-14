using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class ListEventsResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public List<EventDetailResponseData> Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public ListEventsParams Params { get; init; }
}
