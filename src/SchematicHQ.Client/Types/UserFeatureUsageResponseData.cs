using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UserFeatureUsageResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("feature")]
    public FeatureResponseData? Feature { get; set; }

    /// <summary>
    /// When the user last used the feature within the window
    /// </summary>
    [JsonPropertyName("last_seen")]
    public required DateTime LastSeen { get; set; }

    /// <summary>
    /// This row's fraction (0-1) of the company's total usage of the feature within the window, including unattributed usage
    /// </summary>
    [JsonPropertyName("share")]
    public required double Share { get; set; }

    /// <summary>
    /// The user the usage is attributed to; null for usage from events sent without a user
    /// </summary>
    [JsonPropertyName("user")]
    public UserResponseData? User { get; set; }

    /// <summary>
    /// The user the usage is attributed to; null for unattributed usage
    /// </summary>
    [JsonPropertyName("user_id")]
    public string? UserId { get; set; }

    /// <summary>
    /// The user's usage of the feature within the window
    /// </summary>
    [JsonPropertyName("value")]
    public required long Value { get; set; }

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
