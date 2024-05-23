using System.Text.Json.Serialization;

namespace SchematicHQ.Client;

public class CreateBillingSubscriptionsRequestBody
{
    [JsonPropertyName("customer_external_id")]
    public string CustomerExternalId { get; init; }

    [JsonPropertyName("expired_at")]
    public DateTime ExpiredAt { get; init; }

    [JsonPropertyName("interval")]
    public string? Interval { get; init; }

    [JsonPropertyName("product_external_ids")]
    public List<string> ProductExternalIds { get; init; }

    [JsonPropertyName("subscription_external_id")]
    public string SubscriptionExternalId { get; init; }
}
