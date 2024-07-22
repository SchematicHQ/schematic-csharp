using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CreateBillingSubscriptionsRequestBody
{
    [JsonPropertyName("customer_external_id")]
    public required string CustomerExternalId { get; init; }

    [JsonPropertyName("expired_at")]
    public required DateTime ExpiredAt { get; init; }

    [JsonPropertyName("interval")]
    public string? Interval { get; init; }

    [JsonPropertyName("product_external_ids")]
    public IEnumerable<string> ProductExternalIds { get; init; } = new List<string>();

    [JsonPropertyName("subscription_external_id")]
    public required string SubscriptionExternalId { get; init; }
}
