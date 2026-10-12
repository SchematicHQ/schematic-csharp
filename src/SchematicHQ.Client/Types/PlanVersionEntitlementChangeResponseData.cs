using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PlanVersionEntitlementChangeResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("change_type")]
    public required PlanVersionDiffChangeType ChangeType { get; set; }

    /// <summary>
    /// For a changed entitlement, the fields that differ. usage_quantity is the committed quantity of a pay-in-advance entitlement.
    /// </summary>
    [JsonPropertyName("changed_fields")]
    public IEnumerable<string> ChangedFields { get; set; } = new List<string>();

    [JsonPropertyName("feature_id")]
    public required string FeatureId { get; set; }

    [JsonPropertyName("new")]
    public PlanEntitlementResponseData? New { get; set; }

    [JsonPropertyName("old")]
    public PlanEntitlementResponseData? Old { get; set; }

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
