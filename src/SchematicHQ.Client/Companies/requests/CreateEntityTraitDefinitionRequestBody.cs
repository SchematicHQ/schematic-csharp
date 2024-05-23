using System.Text.Json.Serialization;
using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class CreateEntityTraitDefinitionRequestBody
{
    [JsonPropertyName("display_name")]
    public string? DisplayName { get; init; }

    [JsonPropertyName("entity_type")]
    public CreateEntityTraitDefinitionRequestBodyEntityType EntityType { get; init; }

    [JsonPropertyName("hierarchy")]
    public List<string> Hierarchy { get; init; }

    [JsonPropertyName("trait_type")]
    public CreateEntityTraitDefinitionRequestBodyTraitType TraitType { get; init; }
}
