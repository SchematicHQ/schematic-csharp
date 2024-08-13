using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record CompanySubscriptionResponseData
{
    [JsonPropertyName("customer_external_id")]
    public required string CustomerExternalId { get; init; }

    [JsonPropertyName("expired_at")]
    public DateTime? ExpiredAt { get; init; }

    [JsonPropertyName("interval")]
    public required string Interval { get; init; }

    [JsonPropertyName("products")]
    public IEnumerable<BillingProductForSubscriptionResponseData> Products { get; init; } =
        new List<BillingProductForSubscriptionResponseData>();

    [JsonPropertyName("subscription_external_id")]
    public required string SubscriptionExternalId { get; init; }
}
