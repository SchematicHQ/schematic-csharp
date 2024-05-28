using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class ListEntityTraitDefinitionsParams
{
    [JsonPropertyName("entity_type")]
    public string? EntityType { get; init; }

    [JsonPropertyName("ids")]
    public List<string>? Ids { get; init; }

    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    [JsonPropertyName("offset")]
    public int? Offset { get; init; }

    [JsonPropertyName("q")]
    public string? Q { get; init; }

    [JsonPropertyName("trait_type")]
    public string? TraitType { get; init; }
}
