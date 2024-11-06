using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListEventsParams
{
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    [JsonPropertyName("event_subtype")]
    public string? EventSubtype { get; set; }

    [JsonPropertyName("event_types")]
    public IEnumerable<string>? EventTypes { get; set; }

    [JsonPropertyName("flag_id")]
    public string? FlagId { get; set; }

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

    [JsonPropertyName("user_id")]
    public string? UserId { get; set; }
}
