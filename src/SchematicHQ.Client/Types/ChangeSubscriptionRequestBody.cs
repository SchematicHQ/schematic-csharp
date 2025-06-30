using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record ChangeSubscriptionRequestBody : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("add_on_ids")]
    public IEnumerable<UpdateAddOnRequestBody> AddOnIds { get; set; } =
        new List<UpdateAddOnRequestBody>();

    [JsonPropertyName("coupon_external_id")]
    public string? CouponExternalId { get; set; }

    [JsonPropertyName("credit_bundles")]
    public IEnumerable<UpdateCreditBundleRequestBody> CreditBundles { get; set; } =
        new List<UpdateCreditBundleRequestBody>();

    [JsonPropertyName("new_plan_id")]
    public required string NewPlanId { get; set; }

    [JsonPropertyName("new_price_id")]
    public required string NewPriceId { get; set; }

    [JsonPropertyName("pay_in_advance")]
    public IEnumerable<UpdatePayInAdvanceRequestBody> PayInAdvance { get; set; } =
        new List<UpdatePayInAdvanceRequestBody>();

    [JsonPropertyName("payment_method_id")]
    public string? PaymentMethodId { get; set; }

    [JsonPropertyName("promo_code")]
    public string? PromoCode { get; set; }

    [JsonPropertyName("skip_trial")]
    public required bool SkipTrial { get; set; }

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
