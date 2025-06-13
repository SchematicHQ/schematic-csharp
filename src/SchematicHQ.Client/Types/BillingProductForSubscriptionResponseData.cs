using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record BillingProductForSubscriptionResponseData
{
    [JsonPropertyName("billing_scheme")]
    public required string BillingScheme { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("currency")]
    public required string Currency { get; set; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; set; }

    [JsonPropertyName("external_id")]
    public required string ExternalId { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("interval")]
    public required string Interval { get; set; }

    [JsonPropertyName("meter_id")]
    public string? MeterId { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("package_size")]
    public required int PackageSize { get; set; }

    [JsonPropertyName("price")]
    public required int Price { get; set; }

    [JsonPropertyName("price_decimal")]
    public string? PriceDecimal { get; set; }

    [JsonPropertyName("price_external_id")]
    public required string PriceExternalId { get; set; }

    [JsonPropertyName("price_id")]
    public required string PriceId { get; set; }

    [JsonPropertyName("price_tier")]
    public IEnumerable<BillingProductPriceTierResponseData> PriceTier { get; set; } =
        new List<BillingProductPriceTierResponseData>();

    [JsonPropertyName("quantity")]
    public required double Quantity { get; set; }

    [JsonPropertyName("subscription_id")]
    public required string SubscriptionId { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }

    [JsonPropertyName("usage_type")]
    public required string UsageType { get; set; }

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
