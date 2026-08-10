using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record EventExportMetadata : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Restrict the export to events for this company ID (starting with 'comp_')
    /// </summary>
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    /// <summary>
    /// Restrict the export to events captured at or before this time
    /// </summary>
    [JsonPropertyName("end_time")]
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Restrict the export to track events with this subtype
    /// </summary>
    [JsonPropertyName("event_subtype")]
    public string? EventSubtype { get; set; }

    /// <summary>
    /// Restrict the export to these event types (e.g. "track", "identify"); defaults to track, identify and inference events. Flag check events are only exported when requested here explicitly, and require a flag_id.
    /// </summary>
    [JsonPropertyName("event_types")]
    public IEnumerable<EventExportMetadataEventTypesItem>? EventTypes { get; set; }

    /// <summary>
    /// Restrict the export to flag-check events for this flag ID (starting with 'flag_')
    /// </summary>
    [JsonPropertyName("flag_id")]
    public string? FlagId { get; set; }

    /// <summary>
    /// Account member emails to notify when the export completes; empty means the artifact is only retrievable via the API
    /// </summary>
    [JsonPropertyName("notification_email_recipient_email_addresses")]
    public IEnumerable<string>? NotificationEmailRecipientEmailAddresses { get; set; }

    /// <summary>
    /// Restrict the export to events captured at or after this time
    /// </summary>
    [JsonPropertyName("start_time")]
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// Restrict the export to events for this user ID (starting with 'user_')
    /// </summary>
    [JsonPropertyName("user_id")]
    public string? UserId { get; set; }

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
