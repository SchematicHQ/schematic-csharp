using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record EntityKeyResponseData
{
    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; init; }

    [JsonPropertyName("definition_id")]
    public required string DefinitionId { get; init; }

    [JsonPropertyName("entity_id")]
    public required string EntityId { get; init; }

    [JsonPropertyName("entity_type")]
    public required string EntityType { get; init; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("key")]
    public required string Key { get; init; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; init; }

    [JsonPropertyName("value")]
    public required string Value { get; init; }
}
