using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListEntityTraitDefinitionsParams
{
    [JsonPropertyName("entity_type")]
    public ListEntityTraitDefinitionsResponseParamsEntityType? EntityType { get; set; }

    [JsonPropertyName("ids")]
    public IEnumerable<string>? Ids { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonPropertyName("offset")]
    public int? Offset { get; set; }

    [JsonPropertyName("q")]
    public string? Q { get; set; }

    [JsonPropertyName("trait_type")]
    public ListEntityTraitDefinitionsResponseParamsTraitType? TraitType { get; set; }
}
