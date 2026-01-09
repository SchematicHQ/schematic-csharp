using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreateBillingPriceRequestBody
{
    [JsonPropertyName("billing_scheme")]
    public required BillingPriceScheme BillingScheme { get; set; }

    [JsonPropertyName("currency")]
    public required string Currency { get; set; }

    [JsonPropertyName("external_account_id")]
    public required string ExternalAccountId { get; set; }

    [JsonPropertyName("interval")]
    public required string Interval { get; set; }

    [JsonPropertyName("is_active")]
    public required bool IsActive { get; set; }

    [JsonPropertyName("meter_id")]
    public string? MeterId { get; set; }

    [JsonPropertyName("package_size")]
    public int? PackageSize { get; set; }

    [JsonPropertyName("price")]
    public required int Price { get; set; }

    [JsonPropertyName("price_decimal")]
    public string? PriceDecimal { get; set; }

    [JsonPropertyName("price_external_id")]
    public required string PriceExternalId { get; set; }

    [JsonPropertyName("price_tiers")]
    public IEnumerable<CreateBillingPriceTierRequestBody> PriceTiers { get; set; } =
        new List<CreateBillingPriceTierRequestBody>();

    [JsonPropertyName("product_external_id")]
    public required string ProductExternalId { get; set; }

    [JsonPropertyName("provider_type")]
    public BillingProviderType? ProviderType { get; set; }

    [JsonPropertyName("tiers_mode")]
    public BillingTiersMode? TiersMode { get; set; }

    [JsonPropertyName("usage_type")]
    public required BillingPriceUsageType UsageType { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
