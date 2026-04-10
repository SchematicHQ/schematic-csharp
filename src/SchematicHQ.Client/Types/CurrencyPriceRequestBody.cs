using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CurrencyPriceRequestBody : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("currency")]
    public required string Currency { get; set; }

    [JsonPropertyName("monthly_price_tiers")]
    public IEnumerable<CreatePriceTierRequestBody>? MonthlyPriceTiers { get; set; }

    [JsonPropertyName("monthly_unit_price")]
    public long? MonthlyUnitPrice { get; set; }

    [JsonPropertyName("monthly_unit_price_decimal")]
    public string? MonthlyUnitPriceDecimal { get; set; }

    [JsonPropertyName("yearly_price_tiers")]
    public IEnumerable<CreatePriceTierRequestBody>? YearlyPriceTiers { get; set; }

    [JsonPropertyName("yearly_unit_price")]
    public long? YearlyUnitPrice { get; set; }

    [JsonPropertyName("yearly_unit_price_decimal")]
    public string? YearlyUnitPriceDecimal { get; set; }

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
