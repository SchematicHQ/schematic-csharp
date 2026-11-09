using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdateBillingSettingsRequestBody
{
    [JsonPropertyName("default_collector")]
    public Collector? DefaultCollector { get; set; }

    [JsonPropertyName("default_provider_type")]
    public BillingProviderType? DefaultProviderType { get; set; }

    /// <summary>
    /// Where a payer's reply to an invoice email goes. Schematic sends no invoice email until it is set. Send null to clear it.
    /// </summary>
    [JsonPropertyName("invoice_reply_to_email")]
    public string? InvoiceReplyToEmail { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
