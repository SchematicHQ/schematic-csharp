using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class ListProductsParams
{
    [JsonPropertyName("ids")]
    public List<string>? Ids { get; init; }

    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("offset")]
    public int? Offset { get; init; }
}
