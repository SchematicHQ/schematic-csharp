using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record BillingSubscriptionPeriodLineWebhook : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("amount")]
    public required long Amount { get; set; }

    [JsonPropertyName("currency")]
    public required Currency Currency { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("period_end")]
    public required DateTime PeriodEnd { get; set; }

    [JsonPropertyName("period_start")]
    public required DateTime PeriodStart { get; set; }

    [JsonPropertyName("quantity")]
    public long? Quantity { get; set; }

    /// <summary>
    /// The plan or add-on the line bills for.
    /// </summary>
    [JsonPropertyName("source_id")]
    public required string SourceId { get; set; }

    [JsonPropertyName("unit_amount")]
    public long? UnitAmount { get; set; }

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
