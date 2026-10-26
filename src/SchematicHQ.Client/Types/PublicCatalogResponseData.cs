using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PublicCatalogResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("add_ons")]
    public IEnumerable<CatalogPlanResponseData> AddOns { get; set; } =
        new List<CatalogPlanResponseData>();

    [JsonPropertyName("capabilities")]
    public required CatalogCapabilitiesResponseData Capabilities { get; set; }

    [JsonPropertyName("credit_bundles")]
    public IEnumerable<CatalogCreditBundleResponseData> CreditBundles { get; set; } =
        new List<CatalogCreditBundleResponseData>();

    /// <summary>
    /// Present when the catalog offers a custom plan; absent when custom plans are hidden.
    /// </summary>
    [JsonPropertyName("custom_plan_cta")]
    public CatalogCustomPlanCtaResponseData? CustomPlanCta { get; set; }

    /// <summary>
    /// ISO 4217 currency of every top-level price slot; currency_prices carries the others.
    /// </summary>
    [JsonPropertyName("default_currency")]
    public required Currency DefaultCurrency { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("plans")]
    public IEnumerable<CatalogPlanResponseData> Plans { get; set; } =
        new List<CatalogPlanResponseData>();

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
