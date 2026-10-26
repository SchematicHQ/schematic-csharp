using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CatalogCreditBundleResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("bundle_type")]
    public string BundleType { get; set; } = "fixed";

    /// <summary>
    /// The plans whose companies may purchase this bundle. Null means compatible with every plan.
    /// </summary>
    [JsonPropertyName("compatible_plan_ids")]
    public IEnumerable<string>? CompatiblePlanIds { get; set; }

    [JsonPropertyName("credit_icon")]
    public string? CreditIcon { get; set; }

    [JsonPropertyName("credit_id")]
    public required string CreditId { get; set; }

    [JsonPropertyName("credit_name")]
    public required string CreditName { get; set; }

    [JsonPropertyName("credit_plural_name")]
    public string? CreditPluralName { get; set; }

    [JsonPropertyName("credit_singular_name")]
    public string? CreditSingularName { get; set; }

    [JsonPropertyName("currency_prices")]
    public IEnumerable<CatalogBundleCurrencyPriceResponseData> CurrencyPrices { get; set; } =
        new List<CatalogBundleCurrencyPriceResponseData>();

    [JsonPropertyName("expiry_type")]
    public required BillingCreditExpiryType ExpiryType { get; set; }

    [JsonPropertyName("expiry_unit")]
    public required BillingCreditExpiryUnit ExpiryUnit { get; set; }

    [JsonPropertyName("expiry_unit_count")]
    public long? ExpiryUnitCount { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("price")]
    public CatalogPriceResponseData? Price { get; set; }

    /// <summary>
    /// Credits granted per purchase; null means the buyer chooses a custom quantity priced via unit_price.
    /// </summary>
    [JsonPropertyName("quantity")]
    public long? Quantity { get; set; }

    [JsonPropertyName("unit_price")]
    public CatalogPriceResponseData? UnitPrice { get; set; }

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
