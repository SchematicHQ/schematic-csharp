using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CompanyUserUsageMetricsResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// End of the usage window (exclusive)
    /// </summary>
    [JsonPropertyName("end_time")]
    public required DateTime EndTime { get; set; }

    /// <summary>
    /// Event-based features the company has user-attributed usage for in the window; a feature with usage but no entitlement is still listed
    /// </summary>
    [JsonPropertyName("features")]
    public IEnumerable<FeatureResponseData> Features { get; set; } =
        new List<FeatureResponseData>();

    /// <summary>
    /// Whether the company consumed any credits in the window
    /// </summary>
    [JsonPropertyName("has_credits")]
    public required bool HasCredits { get; set; }

    /// <summary>
    /// Start of the usage window
    /// </summary>
    [JsonPropertyName("start_time")]
    public required DateTime StartTime { get; set; }

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
