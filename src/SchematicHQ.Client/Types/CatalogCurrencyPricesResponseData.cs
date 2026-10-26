using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CatalogCurrencyPricesResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("currency")]
    public required Currency Currency { get; set; }

    [JsonPropertyName("monthly_price")]
    public CatalogPriceResponseData? MonthlyPrice { get; set; }

    [JsonPropertyName("one_time_price")]
    public CatalogPriceResponseData? OneTimePrice { get; set; }

    [JsonPropertyName("quarterly_price")]
    public CatalogPriceResponseData? QuarterlyPrice { get; set; }

    [JsonPropertyName("yearly_price")]
    public CatalogPriceResponseData? YearlyPrice { get; set; }

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
