using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record ComponentPreviewResponseData
{
    [JsonPropertyName("active_add_ons")]
    public IEnumerable<CompanyPlanDetailResponseData> ActiveAddOns { get; init; } =
        new List<CompanyPlanDetailResponseData>();

    [JsonPropertyName("active_plans")]
    public IEnumerable<CompanyPlanDetailResponseData> ActivePlans { get; init; } =
        new List<CompanyPlanDetailResponseData>();

    [JsonPropertyName("active_usage_based_entitlements")]
    public IEnumerable<UsageBasedEntitlementResponseData> ActiveUsageBasedEntitlements { get; init; } =
        new List<UsageBasedEntitlementResponseData>();

    [JsonPropertyName("capabilities")]
    public ComponentCapabilities? Capabilities { get; init; }

    [JsonPropertyName("company")]
    public CompanyDetailResponseData? Company { get; init; }

    [JsonPropertyName("component")]
    public ComponentResponseData? Component { get; init; }

    [JsonPropertyName("feature_usage")]
    public FeatureUsageDetailResponseData? FeatureUsage { get; init; }

    [JsonPropertyName("invoices")]
    public IEnumerable<InvoiceResponseData> Invoices { get; init; } =
        new List<InvoiceResponseData>();

    [JsonPropertyName("stripe_embed")]
    public StripeEmbedInfo? StripeEmbed { get; init; }

    [JsonPropertyName("subscription")]
    public CompanySubscriptionResponseData? Subscription { get; init; }

    [JsonPropertyName("trial_payment_method_required")]
    public bool? TrialPaymentMethodRequired { get; init; }

    [JsonPropertyName("upcoming_invoice")]
    public InvoiceResponseData? UpcomingInvoice { get; init; }
}
