using System.Text.Json.Serialization;

namespace SchematicHQ.Client;

public class ListFlagsParams
{
    [JsonPropertyName("feature_id")]
    public string? FeatureId { get; init; }

    [JsonPropertyName("ids")]
    public List<string>? Ids { get; init; }

    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    [JsonPropertyName("offset")]
    public int? Offset { get; init; }

    [JsonPropertyName("q")]
    public string? Q { get; init; }
}
