using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record CheckoutDataResponseData
{
    [JsonPropertyName("active_add_ons")]
    public IEnumerable<PlanDetailResponseData> ActiveAddOns { get; set; } =
        new List<PlanDetailResponseData>();

    [JsonPropertyName("active_plan")]
    public PlanDetailResponseData? ActivePlan { get; set; }

    [JsonPropertyName("active_usage_based_entitlements")]
    public IEnumerable<UsageBasedEntitlementResponseData> ActiveUsageBasedEntitlements { get; set; } =
        new List<UsageBasedEntitlementResponseData>();

    [JsonPropertyName("company")]
    public CompanyDetailResponseData? Company { get; set; }

    [JsonPropertyName("feature_usage")]
    public FeatureUsageDetailResponseData? FeatureUsage { get; set; }

    [JsonPropertyName("selected_plan")]
    public PlanDetailResponseData? SelectedPlan { get; set; }

    [JsonPropertyName("selected_usage_based_entitlements")]
    public IEnumerable<UsageBasedEntitlementResponseData> SelectedUsageBasedEntitlements { get; set; } =
        new List<UsageBasedEntitlementResponseData>();

    [JsonPropertyName("subscription")]
    public CompanySubscriptionResponseData? Subscription { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
