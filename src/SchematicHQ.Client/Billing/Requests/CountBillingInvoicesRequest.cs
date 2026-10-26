using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CountBillingInvoicesRequest
{
    [JsonIgnore]
    public string? CompanyId { get; set; }

    [JsonIgnore]
    public string? ContractId { get; set; }

    [JsonIgnore]
    public InvoiceDocumentState? DocumentState { get; set; }

    /// <summary>
    /// Only open invoices with an amount left to pay whose due date has passed.
    /// </summary>
    [JsonIgnore]
    public bool? PastDue { get; set; }

    /// <summary>
    /// Only invoices from this billing provider. Leave empty for invoices from Schematic and Stripe.
    /// </summary>
    [JsonIgnore]
    public BillingProviderType? ProviderType { get; set; }

    /// <summary>
    /// Search by invoice number, provider invoice ID or company name.
    /// </summary>
    [JsonIgnore]
    public string? Q { get; set; }

    [JsonIgnore]
    public InvoiceStatus? Status { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonIgnore]
    public long? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonIgnore]
    public long? Offset { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
