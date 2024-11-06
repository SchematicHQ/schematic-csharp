using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record CountEntityTraitDefinitionsParams
{
    [JsonPropertyName("entity_type")]
    public CountEntityTraitDefinitionsResponseParamsEntityType? EntityType { get; set; }

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
    public CountEntityTraitDefinitionsResponseParamsTraitType? TraitType { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
