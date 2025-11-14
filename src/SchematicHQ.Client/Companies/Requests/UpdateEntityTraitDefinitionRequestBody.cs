using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdateEntityTraitDefinitionRequestBody
{
    /// <summary>
    /// entity_trait_definition_id
    /// </summary>
    [JsonIgnore]
    public required string EntityTraitDefinitionId { get; set; }

    [JsonPropertyName("display_name")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("trait_type")]
    public required UpdateEntityTraitDefinitionRequestBodyTraitType TraitType { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
