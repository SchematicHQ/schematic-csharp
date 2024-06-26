using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class WebhookEventResponseData
{
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("payload")]
    public string Payload { get; init; }

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

    [JsonPropertyName("webhook_id")]
    public string WebhookId { get; init; }
}
