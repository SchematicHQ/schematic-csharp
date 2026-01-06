using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreateCreditBundleRequestBody
{
    [JsonPropertyName("bundle_name")]
    public required string BundleName { get; set; }

    [JsonPropertyName("bundle_type")]
    public string? BundleType { get; set; }

    [JsonPropertyName("credit_id")]
    public required string CreditId { get; set; }

    [JsonPropertyName("currency")]
    public required string Currency { get; set; }

    [JsonPropertyName("expiry_type")]
    public BillingCreditExpiryType? ExpiryType { get; set; }

    [JsonPropertyName("expiry_unit")]
    public BillingCreditExpiryUnit? ExpiryUnit { get; set; }

    [JsonPropertyName("expiry_unit_count")]
    public int? ExpiryUnitCount { get; set; }

    [JsonPropertyName("price_per_unit")]
    public required int PricePerUnit { get; set; }

    [JsonPropertyName("price_per_unit_decimal")]
    public string? PricePerUnitDecimal { get; set; }

    [JsonPropertyName("quantity")]
    public int? Quantity { get; set; }

    [JsonPropertyName("status")]
    public BillingCreditBundleStatus? Status { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
