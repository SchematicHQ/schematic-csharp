using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record BillingSubscriptionDiscountView
{
    [JsonPropertyName("amount_off")]
    public int? AmountOff { get; init; }

    [JsonPropertyName("coupon_id")]
    public required string CouponId { get; init; }

    [JsonPropertyName("coupon_name")]
    public required string CouponName { get; init; }

    [JsonPropertyName("currency")]
    public string? Currency { get; init; }

    [JsonPropertyName("customer_facing_code")]
    public string? CustomerFacingCode { get; init; }

    [JsonPropertyName("discount_external_id")]
    public required string DiscountExternalId { get; init; }

    [JsonPropertyName("duration")]
    public required string Duration { get; init; }

    [JsonPropertyName("duration_in_months")]
    public int? DurationInMonths { get; init; }

    [JsonPropertyName("ended_at")]
    public DateTime? EndedAt { get; init; }

    [JsonPropertyName("is_active")]
    public required bool IsActive { get; init; }

    [JsonPropertyName("percent_off")]
    public double? PercentOff { get; init; }

    [JsonPropertyName("promo_code_external_id")]
    public string? PromoCodeExternalId { get; init; }

    [JsonPropertyName("started_at")]
    public required DateTime StartedAt { get; init; }

    [JsonPropertyName("subscription_external_id")]
    public required string SubscriptionExternalId { get; init; }
}
