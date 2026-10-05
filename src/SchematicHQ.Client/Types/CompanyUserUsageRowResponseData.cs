using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CompanyUserUsageRowResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// When the user last used the feature within the window; null for the credits metric, which aggregates balances rather than timestamped events
    /// </summary>
    [JsonPropertyName("last_seen")]
    public DateTime? LastSeen { get; set; }

    /// <summary>
    /// This row's fraction (0-1) of consumption within the window, including unattributed consumption
    /// </summary>
    [JsonPropertyName("share")]
    public required double Share { get; set; }

    /// <summary>
    /// The user the consumption is attributed to; null for consumption from events sent without a user
    /// </summary>
    [JsonPropertyName("user")]
    public UserResponseData? User { get; set; }

    /// <summary>
    /// The user the consumption is attributed to; null for unattributed consumption
    /// </summary>
    [JsonPropertyName("user_id")]
    public string? UserId { get; set; }

    /// <summary>
    /// The user's consumption of the metric within the window
    /// </summary>
    [JsonPropertyName("value")]
    public required double Value { get; set; }

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
