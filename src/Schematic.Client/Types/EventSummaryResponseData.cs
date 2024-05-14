using System.Text.Json.Serialization;

namespace Schematic.Client;

public class EventSummaryResponseData
{
    [JsonPropertyName("company_count")]
    public int CompanyCount { get; init; }

    [JsonPropertyName("environment_id")]
    public string EnvironmentId { get; init; }

    [JsonPropertyName("event_count")]
    public int EventCount { get; init; }

    [JsonPropertyName("event_subtype")]
    public string EventSubtype { get; init; }

    [JsonPropertyName("last_seen_at")]
    public DateTime? LastSeenAt { get; init; }

    [JsonPropertyName("user_count")]
    public int UserCount { get; init; }
}
