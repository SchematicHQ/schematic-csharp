using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CheckFlagsResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Lease-aware credit balances keyed by credit ID, covering every credit type the company holds a balance in
    /// </summary>
    [JsonPropertyName("credit_balances")]
    public Dictionary<string, CompanyCreditBalance>? CreditBalances { get; set; }

    /// <summary>
    /// Credit spend policies binding the evaluated company and user; empty when none bind. Each response carries the whole set, so replace any previously received set with it. Advisory: the flag values do not reflect them, since a check names no draw amount
    /// </summary>
    [JsonPropertyName("credit_spend_policies")]
    public IEnumerable<CreditSpendPolicy> CreditSpendPolicies { get; set; } =
        new List<CreditSpendPolicy>();

    [JsonPropertyName("flags")]
    public IEnumerable<CheckFlagResponseData> Flags { get; set; } =
        new List<CheckFlagResponseData>();

    [JsonPropertyName("plan")]
    public DatastreamCompanyPlan? Plan { get; set; }

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
