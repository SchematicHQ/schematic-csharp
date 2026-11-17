using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreditLedgerExportMetadata : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Restrict the export to ledger entries for this billing credit ID
    /// </summary>
    [JsonPropertyName("billing_credit_id")]
    public string? BillingCreditId { get; set; }

    /// <summary>
    /// Restrict the export to ledger entries for this company ID (starting with 'comp_'); omit to export every company in the environment
    /// </summary>
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    /// <summary>
    /// Restrict the export to ledger entries that occurred before this time
    /// </summary>
    [JsonPropertyName("end_time")]
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Restrict the export to ledger entries of this event type
    /// </summary>
    [JsonPropertyName("event_type")]
    public CreditLedgerExportMetadataEventType? EventType { get; set; }

    /// <summary>
    /// Restrict the export to ledger entries for this feature ID (starting with 'feat_')
    /// </summary>
    [JsonPropertyName("feature_id")]
    public string? FeatureId { get; set; }

    /// <summary>
    /// Account member emails to notify when the export completes; empty means the artifact is only retrievable via the API
    /// </summary>
    [JsonPropertyName("notification_email_recipient_email_addresses")]
    public IEnumerable<string>? NotificationEmailRecipientEmailAddresses { get; set; }

    /// <summary>
    /// Restrict the export to ledger entries drawn by this track or inference event, using the event ID that POST /events returned for it
    /// </summary>
    [JsonPropertyName("source_event_id")]
    public string? SourceEventId { get; set; }

    /// <summary>
    /// Restrict the export to ledger entries that occurred at or after this time
    /// </summary>
    [JsonPropertyName("start_time")]
    public DateTime? StartTime { get; set; }

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
