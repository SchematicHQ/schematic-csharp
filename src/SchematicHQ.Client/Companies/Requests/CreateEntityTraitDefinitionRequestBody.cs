using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record CreateEntityTraitDefinitionRequestBody
{
    [JsonPropertyName("display_name")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("entity_type")]
    public required CreateEntityTraitDefinitionRequestBodyEntityType EntityType { get; set; }

    [JsonPropertyName("hierarchy")]
    public IEnumerable<string> Hierarchy { get; set; } = new List<string>();

    [JsonPropertyName("trait_type")]
    public required CreateEntityTraitDefinitionRequestBodyTraitType TraitType { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
