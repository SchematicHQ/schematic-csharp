using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdateCatalogConfigurationRequestBody
{
    [JsonPropertyName("custom_plan_cta_text")]
    public string? CustomPlanCtaText { get; set; }

    [JsonPropertyName("custom_plan_cta_url")]
    public string? CustomPlanCtaUrl { get; set; }

    [JsonPropertyName("custom_plan_price_text")]
    public string? CustomPlanPriceText { get; set; }

    [JsonPropertyName("custom_plans_visible")]
    public bool? CustomPlansVisible { get; set; }

    [JsonPropertyName("ordered_add_ons")]
    public IEnumerable<CatalogConfigOrderedPlan>? OrderedAddOns { get; set; }

    [JsonPropertyName("ordered_bundles")]
    public IEnumerable<CatalogConfigOrderedBundle>? OrderedBundles { get; set; }

    [JsonPropertyName("ordered_plans")]
    public IEnumerable<CatalogConfigOrderedPlan>? OrderedPlans { get; set; }

    [JsonPropertyName("pricing_model")]
    public string? PricingModel { get; set; }

    [JsonPropertyName("pricing_url")]
    public string? PricingUrl { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
