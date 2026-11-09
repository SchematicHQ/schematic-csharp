using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record ContractPostpaidCreditResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Whether owed counts against the contract's overdraft limit: only a credit priced in the contract's currency does.
    /// </summary>
    [JsonPropertyName("counts_toward_limit")]
    public required bool CountsTowardLimit { get; set; }

    [JsonPropertyName("credit_id")]
    public required string CreditId { get; set; }

    [JsonPropertyName("currency")]
    public Currency? Currency { get; set; }

    /// <summary>
    /// Credits drawn past zero and not yet settled.
    /// </summary>
    [JsonPropertyName("debt")]
    public required double Debt { get; set; }

    /// <summary>
    /// The plan grant's cap on credits drawn past zero. Null for no cap.
    /// </summary>
    [JsonPropertyName("grant_cap")]
    public double? GrantCap { get; set; }

    /// <summary>
    /// debt at rate_per_unit, in minor units of currency. Null when the credit has no rate.
    /// </summary>
    [JsonPropertyName("owed")]
    public long? Owed { get; set; }

    /// <summary>
    /// What one credit past zero costs, in minor units of currency, as a decimal. Null when the credit has no rate.
    /// </summary>
    [JsonPropertyName("rate_per_unit")]
    public string? RatePerUnit { get; set; }

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
