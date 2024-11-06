using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record EnvironmentDetailResponseData
{
    [JsonPropertyName("api_keys")]
    public IEnumerable<ApiKeyResponseData> ApiKeys { get; set; } = new List<ApiKeyResponseData>();

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("environment_type")]
    public required string EnvironmentType { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }
}
