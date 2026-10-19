using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record BillingStatementResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("currency")]
    public required Currency Currency { get; set; }

    [JsonPropertyName("lines")]
    public IEnumerable<BillingStatementLineResponseData> Lines { get; set; } =
        new List<BillingStatementLineResponseData>();

    /// <summary>
    /// When the statement was read; it keeps growing until the period closes.
    /// </summary>
    [JsonPropertyName("period_end")]
    public required DateTime PeriodEnd { get; set; }

    /// <summary>
    /// The earliest open charge, or period_end when there is none.
    /// </summary>
    [JsonPropertyName("period_start")]
    public required DateTime PeriodStart { get; set; }

    [JsonPropertyName("subtotal")]
    public required long Subtotal { get; set; }

    [JsonPropertyName("total")]
    public required long Total { get; set; }

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
