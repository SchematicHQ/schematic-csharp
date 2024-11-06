using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record BillingCustomerSubscription
{
    [JsonPropertyName("currency")]
    public required string Currency { get; set; }

    [JsonPropertyName("expired_at")]
    public DateTime? ExpiredAt { get; set; }

    [JsonPropertyName("interval")]
    public required string Interval { get; set; }

    [JsonPropertyName("metered_usage")]
    public required bool MeteredUsage { get; set; }

    [JsonPropertyName("per_unit_price")]
    public required int PerUnitPrice { get; set; }

    [JsonPropertyName("total_price")]
    public required int TotalPrice { get; set; }
}
