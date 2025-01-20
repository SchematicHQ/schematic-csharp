using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record EventResponseData
{
    [JsonPropertyName("api_key")]
    public string? ApiKey { get; set; }

    [JsonPropertyName("body")]
    public object Body { get; set; } = new Dictionary<string, object?>();

    [JsonPropertyName("body_preview")]
    public required string BodyPreview { get; set; }

    [JsonPropertyName("captured_at")]
    public required DateTime CapturedAt { get; set; }

    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    [JsonPropertyName("enriched_at")]
    public DateTime? EnrichedAt { get; set; }

    [JsonPropertyName("environment_id")]
    public string? EnvironmentId { get; set; }

    [JsonPropertyName("error_message")]
    public string? ErrorMessage { get; set; }

    [JsonPropertyName("feature_ids")]
    public IEnumerable<string> FeatureIds { get; set; } = new List<string>();

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("loaded_at")]
    public DateTime? LoadedAt { get; set; }

    [JsonPropertyName("processed_at")]
    public DateTime? ProcessedAt { get; set; }

    [JsonPropertyName("sent_at")]
    public DateTime? SentAt { get; set; }

    [JsonPropertyName("status")]
    public required string Status { get; set; }

    [JsonPropertyName("subtype")]
    public string? Subtype { get; set; }

    [JsonPropertyName("type")]
    public required string Type { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }

    [JsonPropertyName("user_id")]
    public string? UserId { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
