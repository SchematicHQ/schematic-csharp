using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record AccountInferenceCostRateResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("currency")]
    public required string Currency { get; set; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("provider")]
    public required string Provider { get; set; }

    /// <summary>
    /// Cost of a single token in the row's currency, keyed by quantity key.
    /// </summary>
    [JsonPropertyName("rates")]
    public Dictionary<string, double> Rates { get; set; } = new Dictionary<string, double>();

    /// <summary>
    /// When the stored events this rate governs were last brought to its price. Null until the hourly reprice reaches the row after a create or update.
    /// </summary>
    [JsonPropertyName("repriced_at")]
    public DateTime? RepricedAt { get; set; }

    [JsonPropertyName("response_model")]
    public required string ResponseModel { get; set; }

    [JsonPropertyName("starting_at")]
    public required DateTime StartingAt { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }

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
