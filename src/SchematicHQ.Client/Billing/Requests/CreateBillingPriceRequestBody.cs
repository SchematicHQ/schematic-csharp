using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record CreateBillingPriceRequestBody
{
    [JsonPropertyName("currency")]
    public required string Currency { get; set; }

    [JsonPropertyName("interval")]
    public required string Interval { get; set; }

    [JsonPropertyName("price")]
    public required int Price { get; set; }

    [JsonPropertyName("price_external_id")]
    public required string PriceExternalId { get; set; }

    [JsonPropertyName("product_external_id")]
    public required string ProductExternalId { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
