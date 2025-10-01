using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreateCompanyCreditGrant
{
    [JsonPropertyName("billing_periods_count")]
    public int? BillingPeriodsCount { get; set; }

    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    [JsonPropertyName("credit_id")]
    public required string CreditId { get; set; }

    [JsonPropertyName("expires_at")]
    public DateTime? ExpiresAt { get; set; }

    [JsonPropertyName("expiry_type")]
    public CreateCompanyCreditGrantExpiryType? ExpiryType { get; set; }

    [JsonPropertyName("expiry_unit")]
    public CreateCompanyCreditGrantExpiryUnit? ExpiryUnit { get; set; }

    [JsonPropertyName("expiry_unit_count")]
    public int? ExpiryUnitCount { get; set; }

    [JsonPropertyName("quantity")]
    public required int Quantity { get; set; }

    [JsonPropertyName("reason")]
    public required string Reason { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
