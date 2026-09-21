using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record BillingPlanCreditGrantPriceTierResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Lower bound of the tier, in credits per invoice (inclusive).
    /// </summary>
    [JsonPropertyName("from")]
    public required long From { get; set; }

    /// <summary>
    /// Price per credit in this tier, in the plan currency's smallest unit.
    /// </summary>
    [JsonPropertyName("per_unit_price")]
    public long? PerUnitPrice { get; set; }

    /// <summary>
    /// Price per credit in this tier as a decimal, for rates below one cent.
    /// </summary>
    [JsonPropertyName("per_unit_price_decimal")]
    public string? PerUnitPriceDecimal { get; set; }

    /// <summary>
    /// Upper bound of the tier, in credits per invoice (inclusive). Null marks the final, unbounded tier.
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
