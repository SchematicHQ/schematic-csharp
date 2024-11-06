using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record UpdateEntityTraitDefinitionRequestBody
{
    [JsonPropertyName("display_name")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("trait_type")]
    public required UpdateEntityTraitDefinitionRequestBodyTraitType TraitType { get; set; }
}
