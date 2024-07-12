using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListEventsParams
{
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; init; }

    [JsonPropertyName("event_subtype")]
    public string? EventSubtype { get; init; }

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

    [JsonPropertyName("user_id")]
    public string? UserId { get; init; }
}
