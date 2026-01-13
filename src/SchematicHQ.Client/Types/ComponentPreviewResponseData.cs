using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record ComponentPreviewResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("active_add_ons")]
    public IEnumerable<CompanyPlanDetailResponseData> ActiveAddOns { get; set; } =
        new List<CompanyPlanDetailResponseData>();

    [JsonPropertyName("active_plans")]
    public IEnumerable<CompanyPlanDetailResponseData> ActivePlans { get; set; } =
        new List<CompanyPlanDetailResponseData>();

    [JsonPropertyName("active_usage_based_entitlements")]
    public IEnumerable<UsageBasedEntitlementResponseData> ActiveUsageBasedEntitlements { get; set; } =
        new List<UsageBasedEntitlementResponseData>();

    [JsonPropertyName("add_on_compatibilities")]
    public IEnumerable<CompatiblePlans> AddOnCompatibilities { get; set; } =
        new List<CompatiblePlans>();

    [JsonPropertyName("capabilities")]
    public ComponentCapabilities? Capabilities { get; set; }

    [JsonPropertyName("checkout_settings")]
    public required ComponentCheckoutSettings CheckoutSettings { get; set; }

    [JsonPropertyName("company")]
    public CompanyDetailResponseData? Company { get; set; }

    [JsonPropertyName("component")]
    public ComponentResponseData? Component { get; set; }

    [JsonPropertyName("credit_bundles")]
    public IEnumerable<BillingCreditBundleView> CreditBundles { get; set; } =
        new List<BillingCreditBundleView>();

    [JsonPropertyName("credit_grants")]
    public IEnumerable<CreditCompanyGrantView> CreditGrants { get; set; } =
        new List<CreditCompanyGrantView>();

    [JsonPropertyName("default_plan")]
    public PlanDetailResponseData? DefaultPlan { get; set; }

    [JsonPropertyName("display_settings")]
    public required ComponentDisplaySettings DisplaySettings { get; set; }

    [JsonPropertyName("feature_usage")]
    public FeatureUsageDetailResponseData? FeatureUsage { get; set; }

    [JsonPropertyName("invoices")]
    public IEnumerable<InvoiceResponseData> Invoices { get; set; } =
        new List<InvoiceResponseData>();

    [JsonPropertyName("post_trial_plan")]
    public PlanDetailResponseData? PostTrialPlan { get; set; }

    [JsonPropertyName("prevent_self_service_downgrade")]
    public required bool PreventSelfServiceDowngrade { get; set; }

    [JsonPropertyName("prevent_self_service_downgrade_button_text")]
    public string? PreventSelfServiceDowngradeButtonText { get; set; }

    [JsonPropertyName("prevent_self_service_downgrade_url")]
    public string? PreventSelfServiceDowngradeUrl { get; set; }

    [JsonPropertyName("show_as_monthly_prices")]
    public required bool ShowAsMonthlyPrices { get; set; }

    [JsonPropertyName("show_credits")]
    public required bool ShowCredits { get; set; }

    [JsonPropertyName("show_period_toggle")]
    public required bool ShowPeriodToggle { get; set; }

    [JsonPropertyName("show_zero_price_as_free")]
    public required bool ShowZeroPriceAsFree { get; set; }

    [JsonPropertyName("stripe_embed")]
    public StripeEmbedInfo? StripeEmbed { get; set; }

    [JsonPropertyName("subscription")]
    public CompanySubscriptionResponseData? Subscription { get; set; }

    [JsonPropertyName("trial_payment_method_required")]
    public bool? TrialPaymentMethodRequired { get; set; }

    [JsonPropertyName("upcoming_invoice")]
    public InvoiceResponseData? UpcomingInvoice { get; set; }

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
