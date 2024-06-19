using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class CountEntityTraitDefinitionsParams
{
    [JsonPropertyName("entity_type")]
    public string? EntityType { get; init; }

    [JsonPropertyName("ids")]
    public List<string>? Ids { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonPropertyName("offset")]
    public int? Offset { get; init; }

    [JsonPropertyName("q")]
    public string? Q { get; init; }

    [JsonPropertyName("trait_type")]
    public string? TraitType { get; init; }
}
