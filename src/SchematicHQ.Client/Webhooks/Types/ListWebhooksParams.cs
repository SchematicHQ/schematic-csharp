using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class ListWebhooksParams
{
    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    [JsonPropertyName("offset")]
    public int? Offset { get; init; }

    [JsonPropertyName("q")]
    public string? Q { get; init; }
}
