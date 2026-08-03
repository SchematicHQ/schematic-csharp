using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UserUsageDetailResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The user's net credit consumption within the window
    /// </summary>
    [JsonPropertyName("credits")]
    public IEnumerable<UserCreditUsageResponseData> Credits { get; set; } =
        new List<UserCreditUsageResponseData>();

    /// <summary>
    /// The user's net credit consumption by UTC day
    /// </summary>
    [JsonPropertyName("daily_credit_points")]
    public IEnumerable<UserDailyCreditPointResponseData> DailyCreditPoints { get; set; } =
        new List<UserDailyCreditPointResponseData>();

    /// <summary>
    /// The user's per-feature usage by UTC day
    /// </summary>
    [JsonPropertyName("daily_points")]
    public IEnumerable<UserDailyUsagePointResponseData> DailyPoints { get; set; } =
        new List<UserDailyUsagePointResponseData>();

    /// <summary>
    /// End of the usage window (exclusive)
    /// </summary>
    [JsonPropertyName("end_time")]
    public required DateTime EndTime { get; set; }

    /// <summary>
    /// The user's per-feature usage totals within the window
    /// </summary>
    [JsonPropertyName("feature_totals")]
    public IEnumerable<UserFeatureUsageResponseData> FeatureTotals { get; set; } =
        new List<UserFeatureUsageResponseData>();

    /// <summary>
    /// Start of the usage window
    /// </summary>
    [JsonPropertyName("start_time")]
    public required DateTime StartTime { get; set; }

    [JsonPropertyName("user")]
    public UserResponseData? User { get; set; }

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
