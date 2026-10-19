using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record BillingStatementLineResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// In minor units of the statement's currency.
    /// </summary>
    [JsonPropertyName("amount")]
    public required long Amount { get; set; }

    [JsonPropertyName("kind")]
    public required InvoiceLineKind Kind { get; set; }

    [JsonPropertyName("period_end")]
    public DateTime? PeriodEnd { get; set; }

    [JsonPropertyName("period_start")]
    public DateTime? PeriodStart { get; set; }

    [JsonPropertyName("quantity")]
    public long? Quantity { get; set; }

    [JsonPropertyName("subscription_line_id")]
    public string? SubscriptionLineId { get; set; }

    [JsonPropertyName("unit_amount")]
    public long? UnitAmount { get; set; }

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
