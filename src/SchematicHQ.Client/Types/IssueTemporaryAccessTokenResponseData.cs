using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record IssueTemporaryAccessTokenResponseData
{
    [JsonPropertyName("api_key_id")]
    public required string ApiKeyId { get; init; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; init; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; init; }

    [JsonPropertyName("expired_at")]
    public required DateTime ExpiredAt { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("resource_type")]
    public required string ResourceType { get; init; }

    [JsonPropertyName("token")]
    public required string Token { get; init; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; init; }
}
