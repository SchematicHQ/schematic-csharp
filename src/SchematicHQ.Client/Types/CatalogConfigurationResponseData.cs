using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CatalogConfigurationResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("add_ons")]
    public IEnumerable<PlanGroupPlanDetailResponseData> AddOns { get; set; } =
        new List<PlanGroupPlanDetailResponseData>();

    [JsonPropertyName("custom_plan_cta_text")]
    public string? CustomPlanCtaText { get; set; }

    [JsonPropertyName("custom_plan_cta_url")]
    public string? CustomPlanCtaUrl { get; set; }

    [JsonPropertyName("custom_plan_price_text")]
    public string? CustomPlanPriceText { get; set; }

    [JsonPropertyName("custom_plans_visible")]
    public required bool CustomPlansVisible { get; set; }

    [JsonPropertyName("ordered_add_ons")]
    public IEnumerable<CatalogConfigOrderedPlanResponseData> OrderedAddOns { get; set; } =
        new List<CatalogConfigOrderedPlanResponseData>();

    [JsonPropertyName("ordered_bundles")]
    public IEnumerable<CatalogConfigOrderedBundleResponseData> OrderedBundles { get; set; } =
        new List<CatalogConfigOrderedBundleResponseData>();

    [JsonPropertyName("ordered_plans")]
    public IEnumerable<CatalogConfigOrderedPlanResponseData> OrderedPlans { get; set; } =
        new List<CatalogConfigOrderedPlanResponseData>();

    [JsonPropertyName("plans")]
    public IEnumerable<PlanGroupPlanDetailResponseData> Plans { get; set; } =
        new List<PlanGroupPlanDetailResponseData>();

    [JsonPropertyName("pricing_model")]
    public string? PricingModel { get; set; }

    [JsonPropertyName("pricing_url")]
    public string? PricingUrl { get; set; }

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
