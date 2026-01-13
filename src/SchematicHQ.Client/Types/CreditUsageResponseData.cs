using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreditUsageResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("credit_consumption_rate")]
    public double? CreditConsumptionRate { get; set; }

    [JsonPropertyName("credit_grant_counts")]
    public Dictionary<string, double>? CreditGrantCounts { get; set; }

    [JsonPropertyName("credit_grant_details")]
    public IEnumerable<CreditGrantDetail> CreditGrantDetails { get; set; } =
        new List<CreditGrantDetail>();

    [JsonPropertyName("credit_remaining")]
    public double? CreditRemaining { get; set; }

    /// <summary>
    /// Deprecated: Use credit_remaining instead.
    /// </summary>
    [JsonPropertyName("credit_total")]
    public double? CreditTotal { get; set; }

    [JsonPropertyName("credit_type_icon")]
    public string? CreditTypeIcon { get; set; }

    [JsonPropertyName("credit_type_name")]
    public string? CreditTypeName { get; set; }

    [JsonPropertyName("credit_used")]
    public double? CreditUsed { get; set; }

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
