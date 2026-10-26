using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

/// <summary>
/// Input parameters
/// </summary>
[Serializable]
public record ListBillingInvoicesParams : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    [JsonPropertyName("contract_id")]
    public string? ContractId { get; set; }

    [JsonPropertyName("document_state")]
    public InvoiceDocumentState? DocumentState { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonPropertyName("limit")]
    public long? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonPropertyName("offset")]
    public long? Offset { get; set; }

    /// <summary>
    /// Only open invoices with an amount left to pay whose due date has passed.
    /// </summary>
    [JsonPropertyName("past_due")]
    public bool? PastDue { get; set; }

    /// <summary>
    /// Only invoices from this billing provider. Leave empty for invoices from Schematic and Stripe.
    /// </summary>
    [JsonPropertyName("provider_type")]
    public BillingProviderType? ProviderType { get; set; }

    /// <summary>
    /// Search by invoice number, provider invoice ID or company name.
    /// </summary>
    [JsonPropertyName("q")]
    public string? Q { get; set; }

    [JsonPropertyName("status")]
    public InvoiceStatus? Status { get; set; }

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
