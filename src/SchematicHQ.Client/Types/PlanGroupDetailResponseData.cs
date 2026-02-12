using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PlanGroupDetailResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("add_ons")]
    public IEnumerable<PlanGroupPlanDetailResponseData> AddOns { get; set; } =
        new List<PlanGroupPlanDetailResponseData>();

    [JsonPropertyName("checkout_settings")]
    public required CheckoutSettingsResponseData CheckoutSettings { get; set; }

    [JsonPropertyName("component_settings")]
    public required ComponentSettingsResponseData ComponentSettings { get; set; }

    [JsonPropertyName("custom_plan_config")]
    public CustomPlanViewConfigResponseData? CustomPlanConfig { get; set; }

    [JsonPropertyName("custom_plan_id")]
    public string? CustomPlanId { get; set; }

    [JsonPropertyName("default_plan")]
    public PlanGroupPlanDetailResponseData? DefaultPlan { get; set; }

    [JsonPropertyName("default_plan_id")]
    public string? DefaultPlanId { get; set; }

    [JsonPropertyName("fallback_plan")]
    public PlanGroupPlanDetailResponseData? FallbackPlan { get; set; }

    [JsonPropertyName("fallback_plan_id")]
    public string? FallbackPlanId { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("initial_plan")]
    public PlanGroupPlanDetailResponseData? InitialPlan { get; set; }

    [JsonPropertyName("initial_plan_id")]
    public string? InitialPlanId { get; set; }

    [JsonPropertyName("initial_plan_price")]
    public BillingPriceView? InitialPlanPrice { get; set; }

    [JsonPropertyName("initial_plan_price_id")]
    public string? InitialPlanPriceId { get; set; }

    [JsonPropertyName("ordered_add_on_list")]
    public IEnumerable<PlanGroupPlanEntitlementsOrder> OrderedAddOnList { get; set; } =
        new List<PlanGroupPlanEntitlementsOrder>();

    [JsonPropertyName("ordered_bundle_list")]
    public IEnumerable<PlanGroupBundleOrder> OrderedBundleList { get; set; } =
        new List<PlanGroupBundleOrder>();

    [JsonPropertyName("ordered_plan_list")]
    public IEnumerable<PlanGroupPlanEntitlementsOrder> OrderedPlanList { get; set; } =
        new List<PlanGroupPlanEntitlementsOrder>();

    [JsonPropertyName("plans")]
    public IEnumerable<PlanGroupPlanDetailResponseData> Plans { get; set; } =
        new List<PlanGroupPlanDetailResponseData>();

    [JsonPropertyName("prevent_downgrades_when_over_limit")]
    public required bool PreventDowngradesWhenOverLimit { get; set; }

    [JsonPropertyName("prevent_self_service_downgrade")]
    public required bool PreventSelfServiceDowngrade { get; set; }

    [JsonPropertyName("prevent_self_service_downgrade_button_text")]
    public string? PreventSelfServiceDowngradeButtonText { get; set; }

    [JsonPropertyName("prevent_self_service_downgrade_url")]
    public string? PreventSelfServiceDowngradeUrl { get; set; }

    [JsonPropertyName("proration_behavior")]
    public required string ProrationBehavior { get; set; }

    [JsonPropertyName("scheduled_downgrade_behavior")]
    public string? ScheduledDowngradeBehavior { get; set; }

    [JsonPropertyName("scheduled_downgrade_prevent_when_over_limit")]
    public bool? ScheduledDowngradePreventWhenOverLimit { get; set; }

    [JsonPropertyName("show_as_monthly_prices")]
    public required bool ShowAsMonthlyPrices { get; set; }

    [JsonPropertyName("show_credits")]
    public required bool ShowCredits { get; set; }

    [JsonPropertyName("show_period_toggle")]
    public required bool ShowPeriodToggle { get; set; }

    [JsonPropertyName("show_zero_price_as_free")]
    public required bool ShowZeroPriceAsFree { get; set; }

    [JsonPropertyName("sync_customer_billing_details")]
    public required bool SyncCustomerBillingDetails { get; set; }

    [JsonPropertyName("tax_collection_enabled")]
    public required bool TaxCollectionEnabled { get; set; }

    [JsonPropertyName("trial_days")]
    public int? TrialDays { get; set; }

    [JsonPropertyName("trial_expiry_plan")]
    public PlanGroupPlanDetailResponseData? TrialExpiryPlan { get; set; }

    [JsonPropertyName("trial_expiry_plan_id")]
    public string? TrialExpiryPlanId { get; set; }

    [JsonPropertyName("trial_expiry_plan_price")]
    public BillingPriceView? TrialExpiryPlanPrice { get; set; }

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
