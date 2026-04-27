using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record InsightsSummaryResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("active_credits")]
    public required double ActiveCredits { get; set; }

    [JsonPropertyName("mrr")]
    public IEnumerable<MrrResponseData> Mrr { get; set; } = new List<MrrResponseData>();

    [JsonPropertyName("paid_companies")]
    public required long PaidCompanies { get; set; }

    [JsonPropertyName("total_companies")]
    public required long TotalCompanies { get; set; }

    [JsonPropertyName("total_features")]
    public required long TotalFeatures { get; set; }

    [JsonPropertyName("total_plans")]
    public required long TotalPlans { get; set; }

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
