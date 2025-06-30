using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record BillingSubscriptionDiscount : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

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

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
