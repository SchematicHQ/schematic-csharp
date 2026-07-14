using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using OneOf;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreateEventRequestBody : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Requires a secret API key, and trusted_client_clock. Import historical data without affecting billing.
    /// </summary>
    [JsonPropertyName("backfill")]
    public bool? Backfill { get; set; }

    [JsonPropertyName("body")]
    public OneOf<
        EventBodyTrack,
        EventBodyFlagCheck,
        EventBodyIdentify,
        EventBodyInference
    >? Body { get; set; }

    /// <summary>
    /// Either 'identify' or 'track'
    /// </summary>
    [JsonPropertyName("event_type")]
    public required EventType EventType { get; set; }

    /// <summary>
    /// Optional client-supplied key. Duplicate events with the same key (scoped to the environment) are dropped for 24h.
    /// </summary>
    [JsonPropertyName("idempotency_key")]
    public string? IdempotencyKey { get; set; }

    /// <summary>
    /// Optionally provide a timestamp at which the event was sent to Schematic
    /// </summary>
    [JsonPropertyName("sent_at")]
    public DateTime? SentAt { get; set; }

    /// <summary>
    /// Requires a secret API key and sent_at. Use sent_at as the effective timestamp.
    /// </summary>
    [JsonPropertyName("trusted_client_clock")]
    public bool? TrustedClientClock { get; set; }

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
