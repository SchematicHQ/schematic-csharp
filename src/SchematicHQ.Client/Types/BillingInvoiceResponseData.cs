using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record BillingInvoiceResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

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
    /// Null on a Stripe invoice.
    /// </summary>
    [JsonPropertyName("document_state")]
    public InvoiceDocumentState? DocumentState { get; set; }

    [JsonPropertyName("due_at")]
    public DateTime? DueAt { get; set; }

    /// <summary>
    /// The billing provider's invoice ID. Null on an invoice Schematic issued.
    /// </summary>
    [JsonPropertyName("external_id")]
    public string? ExternalId { get; set; }

    [JsonPropertyName("finalized_at")]
    public DateTime? FinalizedAt { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("number")]
    public string? Number { get; set; }

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

    [JsonPropertyName("status")]
    public required InvoiceStatus Status { get; set; }

    /// <summary>
    /// The billing provider's subscription ID. Null on an invoice Schematic issued.
    /// </summary>
    [JsonPropertyName("subscription_external_id")]
    public string? SubscriptionExternalId { get; set; }

    [JsonPropertyName("subtotal")]
    public required long Subtotal { get; set; }

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
