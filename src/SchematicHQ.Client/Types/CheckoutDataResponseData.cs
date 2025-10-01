using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CheckoutDataResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("active_add_ons")]
    public IEnumerable<PlanDetailResponseData> ActiveAddOns { get; set; } =
        new List<PlanDetailResponseData>();

    [JsonPropertyName("active_plan")]
    public PlanDetailResponseData? ActivePlan { get; set; }

    [JsonPropertyName("active_usage_based_entitlements")]
    public IEnumerable<UsageBasedEntitlementResponseData> ActiveUsageBasedEntitlements { get; set; } =
        new List<UsageBasedEntitlementResponseData>();

    [JsonPropertyName("available_credit_bundles")]
    public IEnumerable<BillingCreditBundleResponseData> AvailableCreditBundles { get; set; } =
        new List<BillingCreditBundleResponseData>();

    [JsonPropertyName("company")]
    public CompanyDetailResponseData? Company { get; set; }

    [JsonPropertyName("feature_usage")]
    public FeatureUsageDetailResponseData? FeatureUsage { get; set; }

    [JsonPropertyName("selected_credit_bundles")]
    public IEnumerable<CreditBundlePurchaseResponseData> SelectedCreditBundles { get; set; } =
        new List<CreditBundlePurchaseResponseData>();

    [JsonPropertyName("selected_plan")]
    public PlanDetailResponseData? SelectedPlan { get; set; }

    [JsonPropertyName("selected_usage_based_entitlements")]
    public IEnumerable<UsageBasedEntitlementResponseData> SelectedUsageBasedEntitlements { get; set; } =
        new List<UsageBasedEntitlementResponseData>();

    [JsonPropertyName("subscription")]
    public CompanySubscriptionResponseData? Subscription { get; set; }

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
