using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record GroupRolloutChangesResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("credit_grants_added")]
    public IEnumerable<GroupRolloutChangeResponseData> CreditGrantsAdded { get; set; } =
        new List<GroupRolloutChangeResponseData>();

    [JsonPropertyName("credit_grants_changed")]
    public IEnumerable<GroupRolloutChangeResponseData> CreditGrantsChanged { get; set; } =
        new List<GroupRolloutChangeResponseData>();

    [JsonPropertyName("credit_grants_removed")]
    public IEnumerable<GroupRolloutChangeResponseData> CreditGrantsRemoved { get; set; } =
        new List<GroupRolloutChangeResponseData>();

    [JsonPropertyName("entitlements_added")]
    public IEnumerable<GroupRolloutChangeResponseData> EntitlementsAdded { get; set; } =
        new List<GroupRolloutChangeResponseData>();

    [JsonPropertyName("entitlements_changed")]
    public IEnumerable<GroupRolloutChangeResponseData> EntitlementsChanged { get; set; } =
        new List<GroupRolloutChangeResponseData>();

    [JsonPropertyName("entitlements_removed")]
    public IEnumerable<GroupRolloutChangeResponseData> EntitlementsRemoved { get; set; } =
        new List<GroupRolloutChangeResponseData>();

    [JsonPropertyName("price_changed")]
    public required bool PriceChanged { get; set; }

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
