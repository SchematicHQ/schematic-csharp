using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record BillingUpcomingInvoicesResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    /// <summary>
    /// The invoices the company will be billed next, earliest first. For a subscription Schematic bills, every invoice up to and including the next one that closes a billing period, priced from the subscription, its scheduled changes and its usage as they stand now. For a Stripe subscription, Stripe's upcoming invoice.
    /// </summary>
    [JsonPropertyName("invoices")]
    public IEnumerable<BillingUpcomingInvoiceResponseData> Invoices { get; set; } =
        new List<BillingUpcomingInvoiceResponseData>();

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
