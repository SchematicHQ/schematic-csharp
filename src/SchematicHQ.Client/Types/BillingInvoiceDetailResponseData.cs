using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record BillingInvoiceDetailResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// What the company is asked to pay: the total less any balance applied to it, never below zero. Zero on a draft.
    /// </summary>
    [JsonPropertyName("amount_due")]
    public required long AmountDue { get; set; }

    [JsonPropertyName("amount_paid")]
    public required long AmountPaid { get; set; }

    [JsonPropertyName("amount_remaining")]
    public required long AmountRemaining { get; set; }

    /// <summary>
    /// The collector's report on payment. Null on a draft and on a Stripe invoice.
    /// </summary>
    [JsonPropertyName("collection_state")]
    public InvoiceCollectionState? CollectionState { get; set; }

    [JsonPropertyName("collector")]
    public Collector? Collector { get; set; }

    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    /// <summary>
    /// Null on a Stripe invoice.
    /// </summary>
    [JsonPropertyName("contract_id")]
    public string? ContractId { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("currency")]
    public required Currency Currency { get; set; }

    /// <summary>
    /// What discounts took off the subtotal, in minor units of the invoice's currency. Zero on an invoice Schematic issued.
    /// </summary>
    [JsonPropertyName("discount")]
    public required long Discount { get; set; }

    /// <summary>
    /// Null on a Stripe invoice.
    /// </summary>
    [JsonPropertyName("document_state")]
    public InvoiceDocumentState? DocumentState { get; set; }

    [JsonPropertyName("due_at")]
    public DateTime? DueAt { get; set; }

    /// <summary>
    /// The company's balance after this invoice, in minor units. Below zero is credit the next invoice takes off what is due.
    /// </summary>
    [JsonPropertyName("ending_balance")]
    public required long EndingBalance { get; set; }

    /// <summary>
    /// The billing provider's invoice ID. Null on an invoice Schematic issued.
    /// </summary>
    [JsonPropertyName("external_id")]
    public string? ExternalId { get; set; }

    [JsonPropertyName("finalized_at")]
    public DateTime? FinalizedAt { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("lines")]
    public IEnumerable<BillingInvoiceLineResponseData> Lines { get; set; } =
        new List<BillingInvoiceLineResponseData>();

    [JsonPropertyName("number")]
    public string? Number { get; set; }

    /// <summary>
    /// The billing provider's PDF of the invoice. Null on an invoice Schematic issued, which downloads its PDF from this API.
    /// </summary>
    [JsonPropertyName("pdf_url")]
    public string? PdfUrl { get; set; }

    /// <summary>
    /// Null on a Stripe invoice.
    /// </summary>
    [JsonPropertyName("period_end")]
    public DateTime? PeriodEnd { get; set; }

    /// <summary>
    /// Null on a Stripe invoice.
    /// </summary>
    [JsonPropertyName("period_start")]
    public DateTime? PeriodStart { get; set; }

    [JsonPropertyName("provider_type")]
    public required BillingProviderType ProviderType { get; set; }

    [JsonPropertyName("settlements")]
    public IEnumerable<BillingInvoiceSettlementResponseData> Settlements { get; set; } =
        new List<BillingInvoiceSettlementResponseData>();

    /// <summary>
    /// The company's balance before this invoice, in minor units. Below zero is credit applied to this invoice.
    /// </summary>
    [JsonPropertyName("starting_balance")]
    public required long StartingBalance { get; set; }

    [JsonPropertyName("status")]
    public required InvoiceStatus Status { get; set; }

    /// <summary>
    /// The billing provider's subscription ID. Null on an invoice Schematic issued.
    /// </summary>
    [JsonPropertyName("subscription_external_id")]
    public string? SubscriptionExternalId { get; set; }

    /// <summary>
    /// The Schematic subscription the invoice's lines bill for. Null on a Stripe invoice, which links its subscription through subscription_external_id.
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

    /// <summary>
    /// The contract's terms, copied when the invoice was finalized. Null on a draft and on a Stripe invoice.
    /// </summary>
    [JsonPropertyName("terms")]
    public BillingInvoiceTermsResponseData? Terms { get; set; }

    [JsonPropertyName("total")]
    public required long Total { get; set; }

    /// <summary>
    /// The billing provider's hosted invoice page. Null on an invoice Schematic issued.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

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
