using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record EnvironmentDetailResponseData
{
    [JsonPropertyName("api_keys")]
    public IEnumerable<ApiKeyResponseData> ApiKeys { get; init; } = new List<ApiKeyResponseData>();

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; init; }

    [JsonPropertyName("environment_type")]
    public required string EnvironmentType { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; init; }
}
