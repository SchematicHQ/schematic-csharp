using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record WebhookEventDetailResponseData
{
    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("payload")]
    public string? Payload { get; init; }

    [JsonPropertyName("request_type")]
    public required string RequestType { get; init; }

    [JsonPropertyName("response_code")]
    public int? ResponseCode { get; init; }

    [JsonPropertyName("sent_at")]
    public DateTime? SentAt { get; init; }

    [JsonPropertyName("status")]
    public required string Status { get; init; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; init; }

    [JsonPropertyName("webhook")]
    public WebhookResponseData? Webhook { get; init; }

    [JsonPropertyName("webhook_id")]
    public required string WebhookId { get; init; }
}
