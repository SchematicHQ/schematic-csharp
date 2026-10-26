using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record BillingInvoiceLineResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// In minor units of the invoice's currency.
    /// </summary>
    [JsonPropertyName("amount")]
    public required long Amount { get; set; }

    /// <summary>
    /// The charges this line sums.
    /// </summary>
    [JsonPropertyName("charge_ids")]
    public IEnumerable<string> ChargeIds { get; set; } = new List<string>();

    /// <summary>
    /// What the line bills for: the product, plan, add-on or credit. Null when the line has no subscription line behind it.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// The billing provider's line ID. Null on an invoice Schematic issued.
    /// </summary>
    [JsonPropertyName("external_id")]
    public string? ExternalId { get; set; }

    /// <summary>
    /// Null on a Stripe invoice line.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

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

    [JsonPropertyName("taxable")]
    public required bool Taxable { get; set; }

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
