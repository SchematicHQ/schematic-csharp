using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CatalogPriceTierResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("flat_price")]
    public long? FlatPrice { get; set; }

    /// <summary>
    /// Lower bound of the tier (inclusive).
    /// </summary>
    [JsonPropertyName("from")]
    public required long From { get; set; }

    [JsonPropertyName("per_unit_price")]
    public long? PerUnitPrice { get; set; }

    [JsonPropertyName("per_unit_price_decimal")]
    public string? PerUnitPriceDecimal { get; set; }

    /// <summary>
    /// Upper bound of the tier (inclusive); null means unbounded.
    /// </summary>
    [JsonPropertyName("to")]
    public long? To { get; set; }

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
