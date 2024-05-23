using System.Text.Json.Serialization;

namespace SchematicHQ.Client;

public class ListEnvironmentsParams
{
    [JsonPropertyName("ids")]
    public List<string>? Ids { get; init; }

    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    [JsonPropertyName("offset")]
    public int? Offset { get; init; }
}
