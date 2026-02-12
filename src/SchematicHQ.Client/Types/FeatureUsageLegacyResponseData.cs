using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record FeatureUsageLegacyResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("Allocation")]
    public int? Allocation { get; set; }

    [JsonPropertyName("CreditUsage")]
    public CreditUsage? CreditUsage { get; set; }

    [JsonPropertyName("Entitlement")]
    public required string Entitlement { get; set; }

    [JsonPropertyName("Feature")]
    public FeatureView? Feature { get; set; }

    [JsonPropertyName("MetricResetAt")]
    public DateTime? MetricResetAt { get; set; }

    [JsonPropertyName("Usage")]
    public int? Usage { get; set; }

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
