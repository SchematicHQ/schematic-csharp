using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PreflightEventQuantitiesRequestBody : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The event subtype the event would be recorded under
    /// </summary>
    [JsonPropertyName("event_subtype")]
    public required string EventSubtype { get; set; }

    /// <summary>
    /// Named quantities the event would carry, keyed by quantity key, each priced at its rate in the entitlement's quantity rates. For an inference call these are the token counts as the provider reports them: input_tokens includes the cached_input_tokens and cache_creation_input_tokens subsets. Keys the entitlement does not rate cost nothing
    /// </summary>
    [JsonPropertyName("quantities")]
    public Dictionary<string, double>? Quantities { get; set; }

    /// <summary>
    /// Base units of the event, priced at the entitlement's consumption rate; the request count for an inference call. Absent or zero means one
    /// </summary>
    [JsonPropertyName("quantity")]
    public double? Quantity { get; set; }

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
