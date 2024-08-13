using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record BillingCustomerSubscription
{
    [JsonPropertyName("expired_at")]
    public DateTime? ExpiredAt { get; init; }

    [JsonPropertyName("total_price")]
    public required int TotalPrice { get; init; }
}
