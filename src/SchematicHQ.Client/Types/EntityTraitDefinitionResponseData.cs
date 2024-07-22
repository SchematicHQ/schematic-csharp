using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record EntityTraitDefinitionResponseData
{
    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; init; }

    [JsonPropertyName("display_name")]
    public required string DisplayName { get; init; }

    [JsonPropertyName("entity_type")]
    public required string EntityType { get; init; }

    [JsonPropertyName("hierarchy")]
    public IEnumerable<string> Hierarchy { get; init; } = new List<string>();

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("trait_type")]
    public required string TraitType { get; init; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; init; }
}
