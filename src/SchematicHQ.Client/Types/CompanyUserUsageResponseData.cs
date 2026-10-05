using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CompanyUserUsageResponseData : IJsonOnDeserialized
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
    /// This page of per-user consumption within the window, heaviest first
    /// </summary>
    [JsonPropertyName("rows")]
    public IEnumerable<CompanyUserUsageRowResponseData> Rows { get; set; } =
        new List<CompanyUserUsageRowResponseData>();

    /// <summary>
    /// Start of the usage window
    /// </summary>
    [JsonPropertyName("start_time")]
    public required DateTime StartTime { get; set; }

    /// <summary>
    /// The heaviest user's fraction (0-1) of consumption, measured across every user in the window and not just this page
    /// </summary>
    [JsonPropertyName("top_user_share")]
    public required double TopUserShare { get; set; }

    /// <summary>
    /// Consumption across every user in the window including unattributed, not just this page
    /// </summary>
    [JsonPropertyName("total")]
    public required double Total { get; set; }

    /// <summary>
    /// Consumption from events sent without a user; not a user, so it is excluded from rows and from the count, and returned on every page. Null when the window has none
    /// </summary>
    [JsonPropertyName("unattributed")]
    public CompanyUserUsageRowResponseData? Unattributed { get; set; }

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
