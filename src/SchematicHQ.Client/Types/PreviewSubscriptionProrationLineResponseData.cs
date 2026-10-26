using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PreviewSubscriptionProrationLineResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// In minor units of the block's currency; negative on a credit.
    /// </summary>
    [JsonPropertyName("amount")]
    public required long Amount { get; set; }

    [JsonPropertyName("kind")]
    public required ProrationLineKind Kind { get; set; }

    [JsonPropertyName("period_end")]
    public DateTime? PeriodEnd { get; set; }

    [JsonPropertyName("period_start")]
    public DateTime? PeriodStart { get; set; }

    /// <summary>
    /// The plan or add-on in the cart the price belongs to. Null for a price the subscription carries outside the cart.
    /// </summary>
    [JsonPropertyName("plan_id")]
    public string? PlanId { get; set; }

    /// <summary>
    /// The Schematic price. Null when the provider priced something Schematic does not carry.
    /// </summary>
    [JsonPropertyName("price_id")]
    public string? PriceId { get; set; }

    [JsonPropertyName("quantity")]
    public long? Quantity { get; set; }

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
