using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

/// <summary>
/// Rows are consistent within a page of 250 companies. The As Of column marks when the export started; in a large environment, later pages may include usage recorded after that time.
/// </summary>
[Serializable]
public record CreditBalanceExportMetadata : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Restrict the export to balances of this credit type ID (starting with 'bilcr_')
    /// </summary>
    [JsonPropertyName("billing_credit_id")]
    public string? BillingCreditId { get; set; }

    /// <summary>
    /// Restrict the export to balances for this company ID (starting with 'comp_')
    /// </summary>
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    /// <summary>
    /// Account member emails to notify when the export completes; empty means the artifact is only retrievable via the API
    /// </summary>
    [JsonPropertyName("notification_email_recipient_email_addresses")]
    public IEnumerable<string>? NotificationEmailRecipientEmailAddresses { get; set; }

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
