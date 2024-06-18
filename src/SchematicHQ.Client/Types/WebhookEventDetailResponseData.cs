using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public class WebhookEventDetailResponseData
{
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("request_type")]
    public string RequestType { get; init; }

    [JsonPropertyName("response_code")]
    public int? ResponseCode { get; init; }

    [JsonPropertyName("sent_at")]
    public DateTime? SentAt { get; init; }

    [JsonPropertyName("status")]
    public string Status { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; init; }

    [JsonPropertyName("webhook")]
    public WebhookResponseData? Webhook { get; init; }

    [JsonPropertyName("webhook_id")]
    public string WebhookId { get; init; }
}
