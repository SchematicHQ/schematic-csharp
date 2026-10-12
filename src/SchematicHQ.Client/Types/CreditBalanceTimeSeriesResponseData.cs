using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreditBalanceTimeSeriesResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("balance_points")]
    public IEnumerable<CreditBalancePointResponseData> BalancePoints { get; set; } =
        new List<CreditBalancePointResponseData>();

    [JsonPropertyName("billing_credit_id")]
    public required string BillingCreditId { get; set; }

    [JsonPropertyName("events")]
    public IEnumerable<CreditBalanceEventResponseData> Events { get; set; } =
        new List<CreditBalanceEventResponseData>();

    /// <summary>
    /// True when an external provider owns this credit's ledger, in which case Schematic has no history to report and the series is empty
    /// </summary>
    [JsonPropertyName("external")]
    public required bool External { get; set; }

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
