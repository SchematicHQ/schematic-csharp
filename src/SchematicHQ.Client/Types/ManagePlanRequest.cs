using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record ManagePlanRequest : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("add_on_selections")]
    public IEnumerable<PlanSelection> AddOnSelections { get; set; } = new List<PlanSelection>();

    [JsonPropertyName("base_plan_id")]
    public string? BasePlanId { get; set; }

    [JsonPropertyName("base_plan_price_id")]
    public string? BasePlanPriceId { get; set; }

    /// <summary>
    /// If false, subscription cancels at period end. Only applies when removing all plans. Defaults to true.
    /// </summary>
    [JsonPropertyName("cancel_immediately")]
    public bool? CancelImmediately { get; set; }

    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    [JsonPropertyName("coupon_external_id")]
    public string? CouponExternalId { get; set; }

    [JsonPropertyName("credit_bundles")]
    public IEnumerable<UpdateCreditBundleRequestBody> CreditBundles { get; set; } =
        new List<UpdateCreditBundleRequestBody>();

    [JsonPropertyName("pay_in_advance_entitlements")]
    public IEnumerable<UpdatePayInAdvanceRequestBody> PayInAdvanceEntitlements { get; set; } =
        new List<UpdatePayInAdvanceRequestBody>();

    [JsonPropertyName("payment_method_external_id")]
    public string? PaymentMethodExternalId { get; set; }

    [JsonPropertyName("promo_code")]
    public string? PromoCode { get; set; }

    /// <summary>
    /// If true and cancel_immediately is true, issue prorated credit. Only applies when removing all plans. Defaults to true.
    /// </summary>
    [JsonPropertyName("prorate")]
    public bool? Prorate { get; set; }

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
