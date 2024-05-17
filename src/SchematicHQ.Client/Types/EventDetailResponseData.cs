using System.Text.Json.Serialization;
using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class EventDetailResponseData
{
    [JsonPropertyName("api_key")]
    public string? ApiKey { get; init; }

    [JsonPropertyName("body")]
    public Dictionary<string, object> Body { get; init; }

    [JsonPropertyName("body_preview")]
    public string BodyPreview { get; init; }

    [JsonPropertyName("captured_at")]
    public DateTime CapturedAt { get; init; }

    [JsonPropertyName("company")]
    public PreviewObject? Company { get; init; }

    [JsonPropertyName("company_id")]
    public string? CompanyId { get; init; }

    [JsonPropertyName("enriched_at")]
    public DateTime? EnrichedAt { get; init; }

    [JsonPropertyName("environment_id")]
    public string? EnvironmentId { get; init; }

    [JsonPropertyName("error_message")]
    public string? ErrorMessage { get; init; }

    [JsonPropertyName("feature_ids")]
    public List<string> FeatureIds { get; init; }

    [JsonPropertyName("features")]
    public List<PreviewObject> Features { get; init; }

    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("loaded_at")]
    public DateTime? LoadedAt { get; init; }

    [JsonPropertyName("processed_at")]
    public DateTime? ProcessedAt { get; init; }

    [JsonPropertyName("sent_at")]
    public DateTime? SentAt { get; init; }

    [JsonPropertyName("status")]
    public string Status { get; init; }

    [JsonPropertyName("subtype")]
    public string? Subtype { get; init; }

    [JsonPropertyName("type")]
    public string Type { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; init; }

    [JsonPropertyName("user")]
    public PreviewObject? User { get; init; }

    [JsonPropertyName("user_id")]
    public string? UserId { get; init; }
}
