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
    /// The collector's report on payment. Null on a draft.
    /// </summary>
    [JsonPropertyName("collection_state")]
    public InvoiceCollectionState? CollectionState { get; set; }

    [JsonPropertyName("collector")]
    public Collector? Collector { get; set; }

    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    [JsonPropertyName("contract_id")]
    public required string ContractId { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("currency")]
    public required Currency Currency { get; set; }

    [JsonPropertyName("document_state")]
    public required InvoiceDocumentState DocumentState { get; set; }

    [JsonPropertyName("due_at")]
    public DateTime? DueAt { get; set; }

    [JsonPropertyName("finalized_at")]
    public DateTime? FinalizedAt { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("number")]
    public string? Number { get; set; }

    [JsonPropertyName("period_end")]
    public required DateTime PeriodEnd { get; set; }

    [JsonPropertyName("period_start")]
    public required DateTime PeriodStart { get; set; }

    [JsonPropertyName("status")]
    public required InvoiceStatus Status { get; set; }

    [JsonPropertyName("subtotal")]
    public required long Subtotal { get; set; }

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
