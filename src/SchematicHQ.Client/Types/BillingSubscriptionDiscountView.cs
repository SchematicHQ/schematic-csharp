using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record BillingSubscriptionDiscountView
{
    [JsonPropertyName("amount_off")]
    public int? AmountOff { get; set; }

    [JsonPropertyName("coupon_id")]
    public required string CouponId { get; set; }

    [JsonPropertyName("coupon_name")]
    public required string CouponName { get; set; }

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("customer_facing_code")]
    public string? CustomerFacingCode { get; set; }

    [JsonPropertyName("discount_external_id")]
    public required string DiscountExternalId { get; set; }

    [JsonPropertyName("duration")]
    public required string Duration { get; set; }

    [JsonPropertyName("duration_in_months")]
    public int? DurationInMonths { get; set; }

    [JsonPropertyName("ended_at")]
    public DateTime? EndedAt { get; set; }

    [JsonPropertyName("is_active")]
    public required bool IsActive { get; set; }

    [JsonPropertyName("percent_off")]
    public double? PercentOff { get; set; }

    [JsonPropertyName("promo_code_external_id")]
    public string? PromoCodeExternalId { get; set; }

    [JsonPropertyName("started_at")]
    public required DateTime StartedAt { get; set; }

    [JsonPropertyName("subscription_external_id")]
    public required string SubscriptionExternalId { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
