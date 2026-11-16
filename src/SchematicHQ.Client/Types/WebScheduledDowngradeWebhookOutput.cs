using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record WebScheduledDowngradeWebhookOutput : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("company")]
    public CompanyResponseData? Company { get; set; }

    [JsonPropertyName("execute_after")]
    public required DateTime ExecuteAfter { get; set; }

    [JsonPropertyName("from_plan")]
    public PlanSnapshotView? FromPlan { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("scheduled_interval")]
    public string? ScheduledInterval { get; set; }

    /// <summary>
    /// With scheduled_interval, the scheduled price's billing period: 3 with month is quarterly. Null for a downgrade scheduled before the price was recorded.
    /// </summary>
    [JsonPropertyName("scheduled_interval_count")]
    public long? ScheduledIntervalCount { get; set; }

    [JsonPropertyName("scheduled_price")]
    public long? ScheduledPrice { get; set; }

    [JsonPropertyName("to_plan")]
    public PlanSnapshotView? ToPlan { get; set; }

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
