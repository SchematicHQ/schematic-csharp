using System.Text.Json.Serialization;
using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class UpdateEntityTraitDefinitionRequestBody
{
    [JsonPropertyName("display_name")]
    public string? DisplayName { get; init; }

    [JsonPropertyName("trait_type")]
    public UpdateEntityTraitDefinitionRequestBodyTraitType TraitType { get; init; }
}
