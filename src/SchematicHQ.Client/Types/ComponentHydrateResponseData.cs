using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record ComponentHydrateResponseData
{
    [JsonPropertyName("active_add_ons")]
    public IEnumerable<CompanyPlanDetailResponseData> ActiveAddOns { get; set; } =
        new List<CompanyPlanDetailResponseData>();

    [JsonPropertyName("active_plans")]
    public IEnumerable<CompanyPlanDetailResponseData> ActivePlans { get; set; } =
        new List<CompanyPlanDetailResponseData>();

    [JsonPropertyName("active_usage_based_entitlements")]
    public IEnumerable<UsageBasedEntitlementResponseData> ActiveUsageBasedEntitlements { get; set; } =
        new List<UsageBasedEntitlementResponseData>();

    [JsonPropertyName("capabilities")]
    public ComponentCapabilities? Capabilities { get; set; }

    [JsonPropertyName("company")]
    public CompanyDetailResponseData? Company { get; set; }

    [JsonPropertyName("component")]
    public ComponentResponseData? Component { get; set; }

    [JsonPropertyName("default_plan")]
    public PlanDetailResponseData? DefaultPlan { get; set; }

    [JsonPropertyName("feature_usage")]
    public FeatureUsageDetailResponseData? FeatureUsage { get; set; }

    [JsonPropertyName("stripe_embed")]
    public StripeEmbedInfo? StripeEmbed { get; set; }

    [JsonPropertyName("subscription")]
    public CompanySubscriptionResponseData? Subscription { get; set; }

    [JsonPropertyName("trial_payment_method_required")]
    public bool? TrialPaymentMethodRequired { get; set; }

    [JsonPropertyName("upcoming_invoice")]
    public InvoiceResponseData? UpcomingInvoice { get; set; }

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
