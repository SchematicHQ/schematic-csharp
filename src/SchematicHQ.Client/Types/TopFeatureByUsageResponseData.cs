using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record TopFeatureByUsageResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("change_pct")]
    public double? ChangePct { get; set; }

    [JsonPropertyName("feature_id")]
    public required string FeatureId { get; set; }

    [JsonPropertyName("feature_name")]
    public required string FeatureName { get; set; }

    [JsonPropertyName("plural_name")]
    public string? PluralName { get; set; }

    [JsonPropertyName("prior_usage")]
    public required long PriorUsage { get; set; }

    [JsonPropertyName("usage")]
    public required long Usage { get; set; }

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
