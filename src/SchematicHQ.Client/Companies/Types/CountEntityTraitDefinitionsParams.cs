using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public class CountEntityTraitDefinitionsParams
{
    [JsonPropertyName("entity_type")]
    public CountEntityTraitDefinitionsResponseParamsEntityType? EntityType { get; init; }

    [JsonPropertyName("ids")]
    public IEnumerable<string>? Ids { get; init; }

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
    public CountEntityTraitDefinitionsResponseParamsTraitType? TraitType { get; init; }
}
