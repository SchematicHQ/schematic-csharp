using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record BillingCustomerSubscription
{
    [JsonPropertyName("currency")]
    public required string Currency { get; init; }

    [JsonPropertyName("expired_at")]
    public DateTime? ExpiredAt { get; init; }

    [JsonPropertyName("interval")]
    public required string Interval { get; init; }

    [JsonPropertyName("metered_usage")]
    public required bool MeteredUsage { get; init; }

    [JsonPropertyName("per_unit_price")]
    public required int PerUnitPrice { get; init; }

    [JsonPropertyName("total_price")]
    public required int TotalPrice { get; init; }
}
