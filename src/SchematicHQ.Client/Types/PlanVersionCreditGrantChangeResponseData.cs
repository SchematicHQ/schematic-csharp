using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PlanVersionCreditGrantChangeResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("change_type")]
    public required PlanVersionDiffChangeType ChangeType { get; set; }

    /// <summary>
    /// For a changed grant, the fields that differ.
    /// </summary>
    [JsonPropertyName("changed_fields")]
    public IEnumerable<string> ChangedFields { get; set; } = new List<string>();

    [JsonPropertyName("credit_id")]
    public required string CreditId { get; set; }

    [JsonPropertyName("new")]
    public BillingPlanCreditGrantResponseData? New { get; set; }

    [JsonPropertyName("old")]
    public BillingPlanCreditGrantResponseData? Old { get; set; }

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
