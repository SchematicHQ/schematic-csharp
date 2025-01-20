using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record BillingCouponResponseData
{
    [JsonPropertyName("account_id")]
    public required string AccountId { get; init; }

    [JsonPropertyName("amount_off")]
    public int? AmountOff { get; init; }

    [JsonPropertyName("currency")]
    public string? Currency { get; init; }

    [JsonPropertyName("duration")]
    public string? Duration { get; init; }

    [JsonPropertyName("duration_in_months")]
    public int? DurationInMonths { get; init; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; init; }

    [JsonPropertyName("external_id")]
    public required string ExternalId { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("is_active")]
    public required bool IsActive { get; init; }

    [JsonPropertyName("max_redemptions")]
    public int? MaxRedemptions { get; init; }

    [JsonPropertyName("metadata")]
    public Dictionary<string, object> Metadata { get; init; } = new Dictionary<string, object>();

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("percent_off")]
    public int? PercentOff { get; init; }

    [JsonPropertyName("times_redeemed")]
    public required int TimesRedeemed { get; init; }

    [JsonPropertyName("valid_from")]
    public DateTime? ValidFrom { get; init; }

    [JsonPropertyName("valid_until")]
    public DateTime? ValidUntil { get; init; }
}
