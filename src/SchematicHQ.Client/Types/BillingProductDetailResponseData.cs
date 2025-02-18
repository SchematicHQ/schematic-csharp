using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record BillingProductDetailResponseData
{
    [JsonPropertyName("account_id")]
    public required string AccountId { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("currency")]
    public required string Currency { get; set; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; set; }

    [JsonPropertyName("external_id")]
    public required string ExternalId { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("price")]
    public required double Price { get; set; }

    [JsonPropertyName("prices")]
    public IEnumerable<BillingPriceResponseData> Prices { get; set; } =
        new List<BillingPriceResponseData>();

    [JsonPropertyName("product_id")]
    public required string ProductId { get; set; }

    [JsonPropertyName("quantity")]
    public required double Quantity { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
