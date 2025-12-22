using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record EventDetailResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("api_key")]
    public string? ApiKey { get; set; }

    [JsonPropertyName("body")]
    public Dictionary<string, object?> Body { get; set; } = new Dictionary<string, object?>();

    [JsonPropertyName("body_preview")]
    public required string BodyPreview { get; set; }

    [JsonPropertyName("captured_at")]
    public required DateTime CapturedAt { get; set; }

    [JsonPropertyName("company")]
    public PreviewObject? Company { get; set; }

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

    [JsonPropertyName("features")]
    public IEnumerable<PreviewObject> Features { get; set; } = new List<PreviewObject>();

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("loaded_at")]
    public DateTime? LoadedAt { get; set; }

    [JsonPropertyName("processed_at")]
    public DateTime? ProcessedAt { get; set; }

    [JsonPropertyName("quantity")]
    public required int Quantity { get; set; }

    [JsonPropertyName("sent_at")]
    public DateTime? SentAt { get; set; }

    [JsonPropertyName("status")]
    public required EventStatus Status { get; set; }

    [JsonPropertyName("subtype")]
    public string? Subtype { get; set; }

    [JsonPropertyName("type")]
    public required EventType Type { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }

    [JsonPropertyName("user")]
    public PreviewObject? User { get; set; }

    [JsonPropertyName("user_id")]
    public string? UserId { get; set; }

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
