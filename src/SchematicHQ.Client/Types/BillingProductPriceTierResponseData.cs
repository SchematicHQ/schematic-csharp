using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record BillingProductPriceTierResponseData
{
    [JsonPropertyName("flat_amount")]
    public int? FlatAmount { get; set; }

    [JsonPropertyName("per_unit_price")]
    public int? PerUnitPrice { get; set; }

    [JsonPropertyName("per_unit_price_decimal")]
    public string? PerUnitPriceDecimal { get; set; }

    [JsonPropertyName("up_to")]
    public int? UpTo { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
