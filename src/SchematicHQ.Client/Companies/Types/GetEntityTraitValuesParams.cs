using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record GetEntityTraitValuesParams
{
    [JsonPropertyName("definition_id")]
    public string? DefinitionId { get; set; }

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
}
