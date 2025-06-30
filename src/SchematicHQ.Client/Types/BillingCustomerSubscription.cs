using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record BillingCustomerSubscription : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("currency")]
    public required string Currency { get; set; }

    [JsonPropertyName("expired_at")]
    public DateTime? ExpiredAt { get; set; }

    [JsonPropertyName("interval")]
    public required string Interval { get; set; }

    [JsonPropertyName("metered_usage")]
    public required bool MeteredUsage { get; set; }

    [JsonPropertyName("per_unit_price")]
    public required int PerUnitPrice { get; set; }

    [JsonPropertyName("total_price")]
    public required int TotalPrice { get; set; }

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
