using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record BillingSubscriptionDiscount
{
    [JsonPropertyName("coupon_external_id")]
    public required string CouponExternalId { get; set; }

    [JsonPropertyName("customer_facing_code")]
    public string? CustomerFacingCode { get; set; }

    [JsonPropertyName("ended_at")]
    public DateTime? EndedAt { get; set; }

    [JsonPropertyName("external_id")]
    public required string ExternalId { get; set; }

    [JsonPropertyName("is_active")]
    public required bool IsActive { get; set; }

    [JsonPropertyName("promo_code_external_id")]
    public string? PromoCodeExternalId { get; set; }

    [JsonPropertyName("started_at")]
    public required DateTime StartedAt { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
