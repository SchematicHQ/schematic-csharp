using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record BillingSettingsResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("default_collector")]
    public required Collector DefaultCollector { get; set; }

    [JsonPropertyName("default_provider_type")]
    public BillingProviderType? DefaultProviderType { get; set; }

    /// <summary>
    /// Where a payer's reply to an invoice email goes. Null until set, and Schematic sends no invoice email while it is null.
    /// </summary>
    [JsonPropertyName("invoice_reply_to_email")]
    public string? InvoiceReplyToEmail { get; set; }

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
