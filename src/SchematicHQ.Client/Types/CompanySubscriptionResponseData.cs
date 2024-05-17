using System.Text.Json.Serialization;
using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class CompanySubscriptionResponseData
{
    [JsonPropertyName("customer_external_id")]
    public string CustomerExternalId { get; init; }

    [JsonPropertyName("expired_at")]
    public DateTime? ExpiredAt { get; init; }

    [JsonPropertyName("products")]
    public List<BillingProductResponseData> Products { get; init; }

    [JsonPropertyName("subscription_external_id")]
    public string SubscriptionExternalId { get; init; }
}