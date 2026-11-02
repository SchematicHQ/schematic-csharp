using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CompanyInferenceCostResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Cost and event counts per provider and response model, busiest model first
    /// </summary>
    [JsonPropertyName("by_model")]
    public IEnumerable<InferenceModelCostResponseData> ByModel { get; set; } =
        new List<InferenceModelCostResponseData>();

    /// <summary>
    /// The company the cost belongs to
    /// </summary>
    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    /// <summary>
    /// Summed cost of priced inference events, one entry per currency. Amounts in different currencies are not converted or combined. Empty when no event in the window was priced
    /// </summary>
    [JsonPropertyName("costs")]
    public IEnumerable<InferenceCostAmountResponseData> Costs { get; set; } =
        new List<InferenceCostAmountResponseData>();

    /// <summary>
    /// End of the window (exclusive)
    /// </summary>
    [JsonPropertyName("end_time")]
    public required DateTime EndTime { get; set; }

    /// <summary>
    /// Number of inference events the company sent in the window, priced or not
    /// </summary>
    [JsonPropertyName("event_count")]
    public required long EventCount { get; set; }

    /// <summary>
    /// Start of the window (inclusive)
    /// </summary>
    [JsonPropertyName("start_time")]
    public required DateTime StartTime { get; set; }

    /// <summary>
    /// Number of inference events in the window with no known cost: the event carried none and none could be derived from a price for its model. These add nothing to costs, so a non-zero count means the real cost is higher than reported
    /// </summary>
    [JsonPropertyName("unpriced_event_count")]
    public required long UnpricedEventCount { get; set; }

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
