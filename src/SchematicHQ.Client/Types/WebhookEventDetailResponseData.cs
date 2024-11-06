using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record WebhookEventDetailResponseData
{
    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("payload")]
    public string? Payload { get; set; }

    [JsonPropertyName("request_type")]
    public required string RequestType { get; set; }

    [JsonPropertyName("response_code")]
    public int? ResponseCode { get; set; }

    [JsonPropertyName("sent_at")]
    public DateTime? SentAt { get; set; }

    [JsonPropertyName("status")]
    public required string Status { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }

    [JsonPropertyName("webhook")]
    public WebhookResponseData? Webhook { get; set; }

    [JsonPropertyName("webhook_id")]
    public required string WebhookId { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
