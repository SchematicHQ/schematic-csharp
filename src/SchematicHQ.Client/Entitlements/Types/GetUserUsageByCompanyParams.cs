using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

/// <summary>
/// Input parameters
/// </summary>
[Serializable]
public record GetUserUsageByCompanyParams : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Company to break usage down for
    /// </summary>
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    /// <summary>
    /// End of the usage window (exclusive); defaults to now
    /// </summary>
    [JsonPropertyName("end_time")]
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Restrict to a single event-based feature
    /// </summary>
    [JsonPropertyName("feature_id")]
    public string? FeatureId { get; set; }

    /// <summary>
    /// Start of the usage window; defaults to 30 days before the end
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
