using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record FeatureUsageDataResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("entitlement_source")]
    public required string EntitlementSource { get; set; }

    [JsonPropertyName("entitlement_value_type")]
    public required string EntitlementValueType { get; set; }

    [JsonPropertyName("feature_id")]
    public required string FeatureId { get; set; }

    [JsonPropertyName("feature_name")]
    public required string FeatureName { get; set; }

    [JsonPropertyName("feature_type")]
    public required string FeatureType { get; set; }

    [JsonPropertyName("hard_limit")]
    public required string HardLimit { get; set; }

    [JsonPropertyName("has_access")]
    public required bool HasAccess { get; set; }

    [JsonPropertyName("soft_limit")]
    public required string SoftLimit { get; set; }

    [JsonPropertyName("usage")]
    public required string Usage { get; set; }

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
