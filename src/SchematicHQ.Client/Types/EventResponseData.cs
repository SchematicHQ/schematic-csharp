using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record EventResponseData
{
    [JsonPropertyName("api_key")]
    public string? ApiKey { get; init; }

    [JsonPropertyName("body")]
    public Dictionary<string, object> Body { get; init; } = new Dictionary<string, object>();

    [JsonPropertyName("body_preview")]
    public required string BodyPreview { get; init; }

    [JsonPropertyName("captured_at")]
    public required DateTime CapturedAt { get; init; }

    [JsonPropertyName("company_id")]
    public string? CompanyId { get; init; }

    [JsonPropertyName("enriched_at")]
    public DateTime? EnrichedAt { get; init; }

    [JsonPropertyName("environment_id")]
    public string? EnvironmentId { get; init; }

    [JsonPropertyName("error_message")]
    public string? ErrorMessage { get; init; }

    [JsonPropertyName("feature_ids")]
    public IEnumerable<string> FeatureIds { get; init; } = new List<string>();

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("loaded_at")]
    public DateTime? LoadedAt { get; init; }

    [JsonPropertyName("processed_at")]
    public DateTime? ProcessedAt { get; init; }

    [JsonPropertyName("sent_at")]
    public DateTime? SentAt { get; init; }

    [JsonPropertyName("status")]
    public required string Status { get; init; }

    [JsonPropertyName("subtype")]
    public string? Subtype { get; init; }

    [JsonPropertyName("type")]
    public required string Type { get; init; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; init; }

    [JsonPropertyName("user_id")]
    public string? UserId { get; init; }
}
