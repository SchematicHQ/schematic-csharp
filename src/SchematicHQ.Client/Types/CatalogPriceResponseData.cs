using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CatalogPriceResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("currency")]
    public required Currency Currency { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("interval")]
    public required BillingProductPriceInterval Interval { get; set; }

    [JsonPropertyName("interval_count")]
    public required long IntervalCount { get; set; }

    /// <summary>
    /// Per-unit price of the final tier, precomputed for overage display.
    /// </summary>
    [JsonPropertyName("overage_unit_price_decimal")]
    public string? OverageUnitPriceDecimal { get; set; }

    /// <summary>
    /// Units covered per priced package; a price of $10 with package_size 1000 charges $10 per 1000 units.
    /// </summary>
    [JsonPropertyName("package_size")]
    public required long PackageSize { get; set; }

    [JsonPropertyName("price")]
    public required long Price { get; set; }

    [JsonPropertyName("price_decimal")]
    public string? PriceDecimal { get; set; }

    [JsonPropertyName("price_tiers")]
    public IEnumerable<CatalogPriceTierResponseData> PriceTiers { get; set; } =
        new List<CatalogPriceTierResponseData>();

    [JsonPropertyName("scheme")]
    public required BillingPriceScheme Scheme { get; set; }

    [JsonPropertyName("tiers_mode")]
    public BillingTiersMode? TiersMode { get; set; }

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
