using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record ScheduledDowngradeResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("currency")]
    public required string Currency { get; set; }

    [JsonPropertyName("effective_after")]
    public required DateTime EffectiveAfter { get; set; }

    [JsonPropertyName("from_plan_id")]
    public required string FromPlanId { get; set; }

    [JsonPropertyName("from_plan_name")]
    public required string FromPlanName { get; set; }

    [JsonPropertyName("from_subscription_price")]
    public required int FromSubscriptionPrice { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("interval")]
    public required string Interval { get; set; }

    [JsonPropertyName("scheduled_interval")]
    public string? ScheduledInterval { get; set; }

    [JsonPropertyName("scheduled_price")]
    public int? ScheduledPrice { get; set; }

    [JsonPropertyName("to_plan_id")]
    public required string ToPlanId { get; set; }

    [JsonPropertyName("to_plan_name")]
    public required string ToPlanName { get; set; }

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
