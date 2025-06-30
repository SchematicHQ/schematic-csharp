using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

/// <summary>
/// Input parameters
/// </summary>
[Serializable]
public record ListFeatureUsageParams : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    [JsonPropertyName("company_keys")]
    public Dictionary<string, string>? CompanyKeys { get; set; }

    [JsonPropertyName("feature_ids")]
    public IEnumerable<string>? FeatureIds { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonPropertyName("offset")]
    public int? Offset { get; set; }

    [JsonPropertyName("q")]
    public string? Q { get; set; }

    [JsonPropertyName("without_negative_entitlements")]
    public bool? WithoutNegativeEntitlements { get; set; }

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
