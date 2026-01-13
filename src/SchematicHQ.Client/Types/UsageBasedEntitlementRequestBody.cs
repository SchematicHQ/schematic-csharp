using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UsageBasedEntitlementRequestBody : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("billing_product_id")]
    public string? BillingProductId { get; set; }

    [JsonPropertyName("billing_threshold")]
    public int? BillingThreshold { get; set; }

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("monthly_metered_price_id")]
    public string? MonthlyMeteredPriceId { get; set; }

    [JsonPropertyName("monthly_price_tiers")]
    public IEnumerable<CreatePriceTierRequestBody>? MonthlyPriceTiers { get; set; }

    [JsonPropertyName("monthly_unit_price")]
    public int? MonthlyUnitPrice { get; set; }

    [JsonPropertyName("monthly_unit_price_decimal")]
    public string? MonthlyUnitPriceDecimal { get; set; }

    [JsonPropertyName("overage_billing_product_id")]
    public string? OverageBillingProductId { get; set; }

    [JsonPropertyName("price_behavior")]
    public EntitlementPriceBehavior? PriceBehavior { get; set; }

    /// <summary>
    /// Use MonthlyPriceTiers or YearlyPriceTiers instead
    /// </summary>
    [JsonPropertyName("price_tiers")]
    public IEnumerable<CreatePriceTierRequestBody>? PriceTiers { get; set; }

    [JsonPropertyName("soft_limit")]
    public int? SoftLimit { get; set; }

    [JsonPropertyName("tier_mode")]
    public BillingTiersMode? TierMode { get; set; }

    [JsonPropertyName("yearly_metered_price_id")]
    public string? YearlyMeteredPriceId { get; set; }

    [JsonPropertyName("yearly_price_tiers")]
    public IEnumerable<CreatePriceTierRequestBody>? YearlyPriceTiers { get; set; }

    [JsonPropertyName("yearly_unit_price")]
    public int? YearlyUnitPrice { get; set; }

    [JsonPropertyName("yearly_unit_price_decimal")]
    public string? YearlyUnitPriceDecimal { get; set; }

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
