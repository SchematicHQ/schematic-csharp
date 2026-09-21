using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreditGrantPriceTierRequestBody : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Price per credit in this tier, in the plan currency's smallest unit.
    /// </summary>
    [JsonPropertyName("per_unit_price")]
    public long? PerUnitPrice { get; set; }

    /// <summary>
    /// Price per credit in this tier as a decimal in the plan currency's smallest unit, for prices below one cent.
    /// </summary>
    [JsonPropertyName("per_unit_price_decimal")]
    public string? PerUnitPriceDecimal { get; set; }

    /// <summary>
    /// Inclusive upper bound of this tier, counted in credits per invoice. Null marks the final, unbounded tier.
    /// </summary>
    [JsonPropertyName("up_to")]
    public long? UpTo { get; set; }

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
