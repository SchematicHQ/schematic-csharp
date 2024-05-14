using System.Text.Json.Serialization;

namespace Schematic.Client;

public class ListEventsParams
{
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; init; }

    [JsonPropertyName("event_subtype")]
    public string? EventSubtype { get; init; }

    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    [JsonPropertyName("offset")]
    public int? Offset { get; init; }

    [JsonPropertyName("user_id")]
    public string? UserId { get; init; }
}
