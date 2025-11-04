using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PlanGroupResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("add_on_compatibilities")]
    public IEnumerable<CompatiblePlansResponseData> AddOnCompatibilities { get; set; } =
        new List<CompatiblePlansResponseData>();

    [JsonPropertyName("add_on_ids")]
    public IEnumerable<string> AddOnIds { get; set; } = new List<string>();

    [JsonPropertyName("checkout_settings")]
    public required CheckoutSettingsResponseData CheckoutSettings { get; set; }

    [JsonPropertyName("default_plan_id")]
    public string? DefaultPlanId { get; set; }

    [JsonPropertyName("fallback_plan_id")]
    public string? FallbackPlanId { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("initial_plan_id")]
    public string? InitialPlanId { get; set; }

    [JsonPropertyName("initial_plan_price_id")]
    public string? InitialPlanPriceId { get; set; }

    [JsonPropertyName("ordered_add_on_ids")]
    public IEnumerable<OrderedPlansInGroup> OrderedAddOnIds { get; set; } =
        new List<OrderedPlansInGroup>();

    [JsonPropertyName("plan_ids")]
    public IEnumerable<OrderedPlansInGroup> PlanIds { get; set; } = new List<OrderedPlansInGroup>();

    [JsonPropertyName("prevent_downgrades_when_over_limit")]
    public required bool PreventDowngradesWhenOverLimit { get; set; }

    [JsonPropertyName("show_credits")]
    public required bool ShowCredits { get; set; }

    [JsonPropertyName("show_period_toggle")]
    public required bool ShowPeriodToggle { get; set; }

    [JsonPropertyName("show_zero_price_as_free")]
    public required bool ShowZeroPriceAsFree { get; set; }

    [JsonPropertyName("sync_customer_billing_details_for_tax")]
    public required bool SyncCustomerBillingDetailsForTax { get; set; }

    [JsonPropertyName("tax_collection_enabled")]
    public required bool TaxCollectionEnabled { get; set; }

    [JsonPropertyName("trial_days")]
    public int? TrialDays { get; set; }

    [JsonPropertyName("trial_expiry_plan_id")]
    public string? TrialExpiryPlanId { get; set; }

    [JsonPropertyName("trial_expiry_plan_price_id")]
    public string? TrialExpiryPlanPriceId { get; set; }

    [JsonPropertyName("trial_payment_method_required")]
    public bool? TrialPaymentMethodRequired { get; set; }

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
