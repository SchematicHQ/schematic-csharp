using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class EntityKeyDetailResponseData
{
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("definition")]
    public EntityKeyDefinitionResponseData? Definition { get; init; }

    [JsonPropertyName("definition_id")]
    public string DefinitionId { get; init; }

    [JsonPropertyName("entity_id")]
    public string EntityId { get; init; }

    [JsonPropertyName("entity_type")]
    public string EntityType { get; init; }

    [JsonPropertyName("environment_id")]
    public string EnvironmentId { get; init; }

    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("key")]
    public string Key { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; init; }

    [JsonPropertyName("value")]
    public string Value { get; init; }
}
