using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UserUsageByCompanyResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Per-user net credit consumption within the window
    /// </summary>
    [JsonPropertyName("credits")]
    public IEnumerable<UserCreditUsageResponseData> Credits { get; set; } =
        new List<UserCreditUsageResponseData>();

    /// <summary>
    /// End of the usage window (exclusive)
    /// </summary>
    [JsonPropertyName("end_time")]
    public required DateTime EndTime { get; set; }

    /// <summary>
    /// Per-user, per-feature usage within the window; rows for a feature sum to the company total
    /// </summary>
    [JsonPropertyName("rows")]
    public IEnumerable<UserFeatureUsageResponseData> Rows { get; set; } =
        new List<UserFeatureUsageResponseData>();

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
