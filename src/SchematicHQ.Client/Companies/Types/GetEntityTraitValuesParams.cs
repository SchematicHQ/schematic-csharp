using System.Text.Json.Serialization;

namespace SchematicHQ.Client;

public class GetEntityTraitValuesParams
{
    [JsonPropertyName("definition_id")]
    public string? DefinitionId { get; init; }

    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    [JsonPropertyName("offset")]
    public int? Offset { get; init; }

    [JsonPropertyName("q")]
    public string? Q { get; init; }
}
