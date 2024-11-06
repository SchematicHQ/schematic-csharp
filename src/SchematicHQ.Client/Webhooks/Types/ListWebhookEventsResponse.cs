using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListWebhookEventsResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<WebhookEventDetailResponseData> Data { get; set; } =
        new List<WebhookEventDetailResponseData>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required ListWebhookEventsParams Params { get; set; }
}
