using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListWebhooksResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<WebhookResponseData> Data { get; set; } = new List<WebhookResponseData>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required ListWebhooksParams Params { get; set; }
}
