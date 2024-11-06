using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListEventsResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<EventDetailResponseData> Data { get; set; } =
        new List<EventDetailResponseData>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required ListEventsParams Params { get; set; }
}
