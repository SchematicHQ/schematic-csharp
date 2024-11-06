using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record IssueTemporaryAccessTokenResponseData
{
    [JsonPropertyName("api_key_id")]
    public required string ApiKeyId { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; set; }

    [JsonPropertyName("expired_at")]
    public required DateTime ExpiredAt { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("resource_type")]
    public required string ResourceType { get; set; }

    [JsonPropertyName("token")]
    public required string Token { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }
}
