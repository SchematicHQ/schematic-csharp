using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record EntityTraitResponseData
{
    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; init; }

    [JsonPropertyName("definition_id")]
    public required string DefinitionId { get; init; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; init; }

    [JsonPropertyName("value")]
    public required string Value { get; init; }
}
