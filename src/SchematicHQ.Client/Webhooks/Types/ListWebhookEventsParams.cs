using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class ListWebhookEventsParams
{
    [JsonPropertyName("ids")]
    public List<string>? Ids { get; init; }

    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    [JsonPropertyName("offset")]
    public int? Offset { get; init; }

    [JsonPropertyName("q")]
    public string? Q { get; init; }

    [JsonPropertyName("webhook_id")]
    public string? WebhookId { get; init; }
}
