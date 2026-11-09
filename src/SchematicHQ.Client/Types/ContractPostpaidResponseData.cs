using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record ContractPostpaidResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("credits")]
    public IEnumerable<ContractPostpaidCreditResponseData> Credits { get; set; } =
        new List<ContractPostpaidCreditResponseData>();

    /// <summary>
    /// The contract's currency. Null when neither the contract nor its credits name one.
    /// </summary>
    [JsonPropertyName("currency")]
    public Currency? Currency { get; set; }

    /// <summary>
    /// Owed across the credits priced in currency, each at its own rate, in minor units of currency.
    /// </summary>
    [JsonPropertyName("owed")]
    public required long Owed { get; set; }

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
