using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class ListEntityKeyDefinitionsParams
{
    [JsonPropertyName("entity_type")]
    public string? EntityType { get; init; }

    [JsonPropertyName("ids")]
    public List<string>? Ids { get; init; }

    [JsonPropertyName("key")]
    public string? Key { get; init; }

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
}
