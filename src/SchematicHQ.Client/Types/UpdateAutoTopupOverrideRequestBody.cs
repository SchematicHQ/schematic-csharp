using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdateAutoTopupOverrideRequestBody : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("auto_topup_amount")]
    public long? AutoTopupAmount { get; set; }

    [JsonPropertyName("auto_topup_enabled")]
    public bool? AutoTopupEnabled { get; set; }

    [JsonPropertyName("auto_topup_threshold_credits")]
    public long? AutoTopupThresholdCredits { get; set; }

    [JsonPropertyName("plan_credit_grant_id")]
    public required string PlanCreditGrantId { get; set; }

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
