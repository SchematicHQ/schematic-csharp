using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record GroupRolloutPreviewResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("changes")]
    public GroupRolloutChangesResponseData? Changes { get; set; }

    [JsonPropertyName("companies")]
    public IEnumerable<GroupRolloutCompanyResponseData> Companies { get; set; } =
        new List<GroupRolloutCompanyResponseData>();

    [JsonPropertyName("from_plan_version_id")]
    public required string FromPlanVersionId { get; set; }

    [JsonPropertyName("payer_company_id")]
    public required string PayerCompanyId { get; set; }

    [JsonPropertyName("to_plan_version_id")]
    public required string ToPlanVersionId { get; set; }

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
