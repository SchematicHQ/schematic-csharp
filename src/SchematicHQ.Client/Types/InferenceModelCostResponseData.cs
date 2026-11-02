using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record InferenceModelCostResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Summed cost of this model's priced events, one entry per currency
    /// </summary>
    [JsonPropertyName("costs")]
    public IEnumerable<InferenceCostAmountResponseData> Costs { get; set; } =
        new List<InferenceCostAmountResponseData>();

    /// <summary>
    /// Number of inference events for this model, priced or not
    /// </summary>
    [JsonPropertyName("event_count")]
    public required long EventCount { get; set; }

    /// <summary>
    /// The model provider the events reported, such as openai or anthropic
    /// </summary>
    [JsonPropertyName("provider")]
    public required string Provider { get; set; }

    /// <summary>
    /// The model the provider reported answering the call
    /// </summary>
    [JsonPropertyName("response_model")]
    public required string ResponseModel { get; set; }

    /// <summary>
    /// Number of this model's events with no known cost
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
