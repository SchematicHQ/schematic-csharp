using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record FeatureUsageHistoryResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Companies are identified by ID only. To report against your own identifiers, resolve them once via the companies API and cache the mapping; keys are not repeated on every row.
    /// </summary>
    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    [JsonPropertyName("event_subtype")]
    public required string EventSubtype { get; set; }

    [JsonPropertyName("feature_id")]
    public required string FeatureId { get; set; }

    /// <summary>
    /// Exclusive end of the period this usage covers
    /// </summary>
    [JsonPropertyName("period_end")]
    public required DateTime PeriodEnd { get; set; }

    /// <summary>
    /// Inclusive start of the period this usage covers
    /// </summary>
    [JsonPropertyName("period_start")]
    public required DateTime PeriodStart { get; set; }

    /// <summary>
    /// Usage recorded within this period; an incremental total, not a running one
    /// </summary>
    [JsonPropertyName("usage")]
    public required long Usage { get; set; }

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
