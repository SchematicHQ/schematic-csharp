using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreditBalancePointResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("balance")]
    public required double Balance { get; set; }

    /// <summary>
    /// Credits drawn during this day, as a positive number
    /// </summary>
    [JsonPropertyName("consumed")]
    public required double Consumed { get; set; }

    [JsonPropertyName("consumed_by_feature")]
    public IEnumerable<CreditBalanceFeatureConsumptionResponseData> ConsumedByFeature { get; set; } =
        new List<CreditBalanceFeatureConsumptionResponseData>();

    /// <summary>
    /// Start of the UTC day this point covers
    /// </summary>
    [JsonPropertyName("timestamp")]
    public required DateTime Timestamp { get; set; }

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
