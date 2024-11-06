using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record GetEventSummariesParams
{
    [JsonPropertyName("event_subtypes")]
    public IEnumerable<string>? EventSubtypes { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonPropertyName("offset")]
    public int? Offset { get; set; }

    [JsonPropertyName("q")]
    public string? Q { get; set; }
}
