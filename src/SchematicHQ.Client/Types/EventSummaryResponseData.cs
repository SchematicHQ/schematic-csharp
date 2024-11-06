using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record EventSummaryResponseData
{
    [JsonPropertyName("company_count")]
    public required int CompanyCount { get; set; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; set; }

    [JsonPropertyName("event_count")]
    public required int EventCount { get; set; }

    [JsonPropertyName("event_subtype")]
    public required string EventSubtype { get; set; }

    [JsonPropertyName("last_seen_at")]
    public DateTime? LastSeenAt { get; set; }

    [JsonPropertyName("user_count")]
    public required int UserCount { get; set; }
}
