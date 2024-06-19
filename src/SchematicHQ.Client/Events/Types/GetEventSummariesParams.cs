using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class GetEventSummariesParams
{
    [JsonPropertyName("event_subtypes")]
    public List<string>? EventSubtypes { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonPropertyName("offset")]
    public int? Offset { get; init; }

    [JsonPropertyName("q")]
    public string? Q { get; init; }
}
