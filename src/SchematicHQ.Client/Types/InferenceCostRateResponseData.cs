using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record InferenceCostRateResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    /// <summary>
    /// Lowercase ISO currency code the rates are priced in.
    /// </summary>
    [JsonPropertyName("currency")]
    public required string Currency { get; set; }

    /// <summary>
    /// The rate's ID. An account rate's ID is the one its update and delete routes take.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// Whether Schematic's daily list-price update leaves this price alone. Always false for an account rate.
    /// </summary>
    [JsonPropertyName("pinned")]
    public required bool Pinned { get; set; }

    /// <summary>
    /// The inference provider, such as anthropic or openai.
    /// </summary>
    [JsonPropertyName("provider")]
    public required string Provider { get; set; }

    /// <summary>
    /// Cost of a single token in the row's currency, keyed by quantity key.
    /// </summary>
    [JsonPropertyName("rates")]
    public Dictionary<string, double> Rates { get; set; } = new Dictionary<string, double>();

    /// <summary>
    /// When the stored events this rate governs were last brought to its price. Null until Schematic reprices them after the rate is created or changed.
    /// </summary>
    [JsonPropertyName("repriced_at")]
    public DateTime? RepricedAt { get; set; }

    /// <summary>
    /// The model name the provider reports on its responses.
    /// </summary>
    [JsonPropertyName("response_model")]
    public required string ResponseModel { get; set; }

    /// <summary>
    /// Which sheet the rate belongs to. An account rate prices an event ahead of the list price.
    /// </summary>
    [JsonPropertyName("scope")]
    public required InferenceCostRateScope Scope { get; set; }

    /// <summary>
    /// When this price takes effect. It applies until the next rate for the same provider, model and currency starts.
    /// </summary>
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
