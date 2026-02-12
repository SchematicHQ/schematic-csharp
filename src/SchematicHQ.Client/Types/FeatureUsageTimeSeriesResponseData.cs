using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record FeatureUsageTimeSeriesResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("feature_id")]
    public required string FeatureId { get; set; }

    [JsonPropertyName("feature_type")]
    public required FeatureType FeatureType { get; set; }

    [JsonPropertyName("limits")]
    public IEnumerable<LimitTimeSeriesPointResponseData> Limits { get; set; } =
        new List<LimitTimeSeriesPointResponseData>();

    [JsonPropertyName("period_type")]
    public string? PeriodType { get; set; }

    [JsonPropertyName("usage_points")]
    public IEnumerable<UsageTimeSeriesPointResponseData> UsagePoints { get; set; } =
        new List<UsageTimeSeriesPointResponseData>();

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
