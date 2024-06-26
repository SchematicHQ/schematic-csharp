using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class EntityTraitDefinitionResponseData
{
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("display_name")]
    public string DisplayName { get; init; }

    [JsonPropertyName("entity_type")]
    public string EntityType { get; init; }

    [JsonPropertyName("hierarchy")]
    public IEnumerable<string> Hierarchy { get; init; }

    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("trait_type")]
    public string TraitType { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; init; }
}
