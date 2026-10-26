using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PreviewSubscriptionProrationResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// When the proration is charged: the change for now, the period's end for at_renewal. Null when nothing is charged.
    /// </summary>
    [JsonPropertyName("billed_at")]
    public DateTime? BilledAt { get; set; }

    /// <summary>
    /// Sum of the charge lines, in minor units, before tax.
    /// </summary>
    [JsonPropertyName("charge")]
    public required long Charge { get; set; }

    /// <summary>
    /// Sum of the credit lines, in minor units, before tax: zero or below.
    /// </summary>
    [JsonPropertyName("credit")]
    public required long Credit { get; set; }

    [JsonPropertyName("currency")]
    public required Currency Currency { get; set; }

    [JsonPropertyName("lines")]
    public IEnumerable<PreviewSubscriptionProrationLineResponseData> Lines { get; set; } =
        new List<PreviewSubscriptionProrationLineResponseData>();

    /// <summary>
    /// When the proration is charged: now, at_renewal, or none when it nets to nothing.
    /// </summary>
    [JsonPropertyName("timing")]
    public required CheckoutChargeTiming Timing { get; set; }

    /// <summary>
    /// credit plus charge, before tax. Equals proration.
    /// </summary>
    [JsonPropertyName("total")]
    public required long Total { get; set; }

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
