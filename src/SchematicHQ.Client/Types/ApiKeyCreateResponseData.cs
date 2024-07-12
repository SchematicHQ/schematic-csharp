using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ApiKeyCreateResponseData
{
    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("environment_id")]
    public string? EnvironmentId { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("last_used_at")]
    public DateTime? LastUsedAt { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("scopes")]
    public IEnumerable<string> Scopes { get; init; } = new List<string>();

    [JsonPropertyName("secret")]
    public required string Secret { get; init; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; init; }
}
