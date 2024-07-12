using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record CreateEntityTraitDefinitionRequestBody
{
    [JsonPropertyName("display_name")]
    public string? DisplayName { get; init; }

    [JsonPropertyName("entity_type")]
    public required CreateEntityTraitDefinitionRequestBodyEntityType EntityType { get; init; }

    [JsonPropertyName("hierarchy")]
    public IEnumerable<string> Hierarchy { get; init; } = new List<string>();

    [JsonPropertyName("trait_type")]
    public required CreateEntityTraitDefinitionRequestBodyTraitType TraitType { get; init; }
}
