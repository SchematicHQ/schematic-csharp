using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record LimitTimeSeriesPointResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("effective_at")]
    public required DateTime EffectiveAt { get; set; }

    [JsonPropertyName("is_soft_limit")]
    public required bool IsSoftLimit { get; set; }

    [JsonPropertyName("limit_source")]
    public required EntitlementType LimitSource { get; set; }

    [JsonPropertyName("limit_value")]
    public int? LimitValue { get; set; }

    [JsonPropertyName("plan_id")]
    public string? PlanId { get; set; }

    [JsonPropertyName("price_behavior")]
    public EntitlementPriceBehavior? PriceBehavior { get; set; }

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
