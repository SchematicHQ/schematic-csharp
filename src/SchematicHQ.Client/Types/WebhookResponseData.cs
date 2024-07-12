using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record WebhookResponseData
{
    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("request_types")]
    public IEnumerable<string> RequestTypes { get; init; } = new List<string>();

    [JsonPropertyName("status")]
    public required string Status { get; init; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; init; }

    [JsonPropertyName("url")]
    public required string Url { get; init; }
}
