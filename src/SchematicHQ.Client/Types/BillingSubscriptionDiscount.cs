using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record BillingSubscriptionDiscount
{
    [JsonPropertyName("coupon_external_id")]
    public required string CouponExternalId { get; init; }

    [JsonPropertyName("customer_facing_code")]
    public string? CustomerFacingCode { get; init; }

    [JsonPropertyName("ended_at")]
    public DateTime? EndedAt { get; init; }

    [JsonPropertyName("external_id")]
    public required string ExternalId { get; init; }

    [JsonPropertyName("is_active")]
    public required bool IsActive { get; init; }

    [JsonPropertyName("promo_code_external_id")]
    public string? PromoCodeExternalId { get; init; }

    [JsonPropertyName("started_at")]
    public required DateTime StartedAt { get; init; }
}
