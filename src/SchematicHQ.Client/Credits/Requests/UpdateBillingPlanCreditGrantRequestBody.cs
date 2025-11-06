using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdateBillingPlanCreditGrantRequestBody
{
    [JsonPropertyName("apply_to_existing")]
    public bool? ApplyToExisting { get; set; }

    [JsonPropertyName("credit_amount")]
    public int? CreditAmount { get; set; }

    [JsonPropertyName("expiry_type")]
    public UpdateBillingPlanCreditGrantRequestBodyExpiryType? ExpiryType { get; set; }

    [JsonPropertyName("expiry_unit")]
    public UpdateBillingPlanCreditGrantRequestBodyExpiryUnit? ExpiryUnit { get; set; }

    [JsonPropertyName("expiry_unit_count")]
    public int? ExpiryUnitCount { get; set; }

    [JsonPropertyName("reset_cadence")]
    public required UpdateBillingPlanCreditGrantRequestBodyResetCadence ResetCadence { get; set; }

    [JsonPropertyName("reset_start")]
    public required UpdateBillingPlanCreditGrantRequestBodyResetStart ResetStart { get; set; }

    [JsonPropertyName("reset_type")]
    public UpdateBillingPlanCreditGrantRequestBodyResetType? ResetType { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
