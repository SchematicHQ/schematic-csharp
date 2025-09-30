using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdateCreditBundleDetailsRequestBody
{
    [JsonPropertyName("bundle_name")]
    public required string BundleName { get; set; }

    [JsonPropertyName("expiry_type")]
    public UpdateCreditBundleDetailsRequestBodyExpiryType? ExpiryType { get; set; }

    [JsonPropertyName("expiry_unit_count")]
    public int? ExpiryUnitCount { get; set; }

    [JsonPropertyName("price_per_unit")]
    public required int PricePerUnit { get; set; }

    [JsonPropertyName("price_per_unit_decimal")]
    public string? PricePerUnitDecimal { get; set; }

    [JsonPropertyName("quantity")]
    public int? Quantity { get; set; }

    [JsonPropertyName("status")]
    public UpdateCreditBundleDetailsRequestBodyStatus? Status { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
