using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreateBillingPriceTierRequestBody : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("flat_amount")]
    public int? FlatAmount { get; set; }

    [JsonPropertyName("per_unit_decimal")]
    public string? PerUnitDecimal { get; set; }

    [JsonPropertyName("per_unit_price")]
    public int? PerUnitPrice { get; set; }

    [JsonPropertyName("price_external_id")]
    public required string PriceExternalId { get; set; }

    [JsonPropertyName("provider_type")]
    public BillingProviderType? ProviderType { get; set; }

    [JsonPropertyName("up_to")]
    public int? UpTo { get; set; }

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
