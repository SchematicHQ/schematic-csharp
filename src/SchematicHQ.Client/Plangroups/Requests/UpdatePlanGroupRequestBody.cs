using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdatePlanGroupRequestBody
{
    /// <summary>
    /// plan_group_id
    /// </summary>
    [JsonIgnore]
    public required string PlanGroupId { get; set; }

    [JsonPropertyName("add_on_compatibilities")]
    public IEnumerable<CompatiblePlans>? AddOnCompatibilities { get; set; }

    /// <summary>
    /// Use OrderedAddOns instead
    /// </summary>
    [JsonPropertyName("add_on_ids")]
    public IEnumerable<string> AddOnIds { get; set; } = new List<string>();

    [JsonPropertyName("checkout_collect_address")]
    public required bool CheckoutCollectAddress { get; set; }

    [JsonPropertyName("checkout_collect_email")]
    public required bool CheckoutCollectEmail { get; set; }

    [JsonPropertyName("checkout_collect_phone")]
    public required bool CheckoutCollectPhone { get; set; }

    [JsonPropertyName("custom_plan_config")]
    public CustomPlanConfig? CustomPlanConfig { get; set; }

    [JsonPropertyName("custom_plan_id")]
    public string? CustomPlanId { get; set; }

    [JsonPropertyName("enable_tax_collection")]
    public required bool EnableTaxCollection { get; set; }

    [JsonPropertyName("fallback_plan_id")]
    public string? FallbackPlanId { get; set; }

    [JsonPropertyName("initial_plan_id")]
    public string? InitialPlanId { get; set; }

    [JsonPropertyName("initial_plan_price_id")]
    public string? InitialPlanPriceId { get; set; }

    [JsonPropertyName("ordered_add_ons")]
    public IEnumerable<OrderedPlansInGroup> OrderedAddOns { get; set; } =
        new List<OrderedPlansInGroup>();

    [JsonPropertyName("ordered_bundle_list")]
    public IEnumerable<PlanGroupBundleOrder> OrderedBundleList { get; set; } =
        new List<PlanGroupBundleOrder>();

    [JsonPropertyName("ordered_plans")]
    public IEnumerable<OrderedPlansInGroup> OrderedPlans { get; set; } =
        new List<OrderedPlansInGroup>();

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

    [JsonPropertyName("trial_days")]
    public int? TrialDays { get; set; }

    [JsonPropertyName("trial_expiry_plan_id")]
    public string? TrialExpiryPlanId { get; set; }

    [JsonPropertyName("trial_expiry_plan_price_id")]
    public string? TrialExpiryPlanPriceId { get; set; }

    [JsonPropertyName("trial_payment_method_required")]
    public bool? TrialPaymentMethodRequired { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
