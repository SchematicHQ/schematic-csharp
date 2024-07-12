using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record EventSummaryResponseData
{
    [JsonPropertyName("company_count")]
    public required int CompanyCount { get; init; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; init; }

    [JsonPropertyName("event_count")]
    public required int EventCount { get; init; }

    [JsonPropertyName("event_subtype")]
    public required string EventSubtype { get; init; }

    [JsonPropertyName("last_seen_at")]
    public DateTime? LastSeenAt { get; init; }

    [JsonPropertyName("user_count")]
    public required int UserCount { get; init; }
}
