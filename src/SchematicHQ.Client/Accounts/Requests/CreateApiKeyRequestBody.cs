using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CreateApiKeyRequestBody
{
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("environment_id")]
    public string? EnvironmentId { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }
}
