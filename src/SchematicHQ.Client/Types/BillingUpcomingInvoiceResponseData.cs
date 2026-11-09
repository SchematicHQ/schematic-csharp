using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record BillingUpcomingInvoiceResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// What the company will be asked to pay: the total less the balance applied to it, never below zero.
    /// </summary>
    [JsonPropertyName("amount_due")]
    public required long AmountDue { get; set; }

    /// <summary>
    /// When the invoice is raised. On a Schematic invoice, before now when a period has turned and its invoice is not raised yet.
    /// </summary>
    [JsonPropertyName("at")]
    public required DateTime At { get; set; }

    /// <summary>
    /// The subscription change that raises the invoice. Null on an invoice that closes a billing period, and on a Stripe invoice.
    /// </summary>
    [JsonPropertyName("change_id")]
    public string? ChangeId { get; set; }

    [JsonPropertyName("currency")]
    public required Currency Currency { get; set; }

    /// <summary>
    /// What discounts take off the subtotal, in minor units of the invoice's currency. Zero on a Schematic invoice.
    /// </summary>
    [JsonPropertyName("discount")]
    public required long Discount { get; set; }

    [JsonPropertyName("due_at")]
    public DateTime? DueAt { get; set; }

    /// <summary>
    /// The company's balance after this invoice, in minor units. Below zero is credit the next invoice takes off what is due.
    /// </summary>
    [JsonPropertyName("ending_balance")]
    public required long EndingBalance { get; set; }

    [JsonPropertyName("lines")]
    public IEnumerable<BillingStatementLineResponseData> Lines { get; set; } =
        new List<BillingStatementLineResponseData>();

    [JsonPropertyName("period_end")]
    public required DateTime PeriodEnd { get; set; }

    [JsonPropertyName("period_start")]
    public required DateTime PeriodStart { get; set; }

    /// <summary>
    /// Who raises the invoice: schematic for a subscription Schematic bills, stripe for a Stripe subscription.
    /// </summary>
    [JsonPropertyName("provider_type")]
    public required BillingProviderType ProviderType { get; set; }

    /// <summary>
    /// The company's balance before this invoice, in minor units. Below zero is credit applied to this invoice.
    /// </summary>
    [JsonPropertyName("starting_balance")]
    public required long StartingBalance { get; set; }

    /// <summary>
    /// The Stripe subscription the invoice bills. Null on a Schematic invoice.
    /// </summary>
    [JsonPropertyName("subscription_external_id")]
    public string? SubscriptionExternalId { get; set; }

    /// <summary>
    /// The Schematic subscription the invoice's lines bill for. Null on a Stripe invoice.
    /// </summary>
    [JsonPropertyName("subscription_id")]
    public string? SubscriptionId { get; set; }

    [JsonPropertyName("subtotal")]
    public required long Subtotal { get; set; }

    /// <summary>
    /// In minor units of the invoice's currency.
    /// </summary>
    [JsonPropertyName("tax")]
    public required long Tax { get; set; }

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
