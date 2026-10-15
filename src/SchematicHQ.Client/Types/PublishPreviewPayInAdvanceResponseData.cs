using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PublishPreviewPayInAdvanceResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("committed_quantity")]
    public required long CommittedQuantity { get; set; }

    /// <summary>
    /// Null when the subscription has no item for the feature.
    /// </summary>
    [JsonPropertyName("current_quantity")]
    public long? CurrentQuantity { get; set; }

    /// <summary>
    /// The quantity after finalizing. Finalizing raises an item to the committed quantity and never lowers it. Zero when the subscription will hold no item for the feature.
    /// </summary>
    [JsonPropertyName("effective_quantity")]
    public required long EffectiveQuantity { get; set; }

    [JsonPropertyName("feature_id")]
    public required string FeatureId { get; set; }

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
