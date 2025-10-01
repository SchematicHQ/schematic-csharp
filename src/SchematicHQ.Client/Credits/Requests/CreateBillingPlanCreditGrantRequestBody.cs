using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreateBillingPlanCreditGrantRequestBody
{
    [JsonPropertyName("credit_amount")]
    public required int CreditAmount { get; set; }

    [JsonPropertyName("credit_id")]
    public required string CreditId { get; set; }

    [JsonPropertyName("expiry_type")]
    public CreateBillingPlanCreditGrantRequestBodyExpiryType? ExpiryType { get; set; }

    [JsonPropertyName("expiry_unit")]
    public CreateBillingPlanCreditGrantRequestBodyExpiryUnit? ExpiryUnit { get; set; }

    [JsonPropertyName("expiry_unit_count")]
    public int? ExpiryUnitCount { get; set; }

    [JsonPropertyName("plan_id")]
    public required string PlanId { get; set; }

    [JsonPropertyName("reset_cadence")]
    public required CreateBillingPlanCreditGrantRequestBodyResetCadence ResetCadence { get; set; }

    [JsonPropertyName("reset_start")]
    public required CreateBillingPlanCreditGrantRequestBodyResetStart ResetStart { get; set; }

    [JsonPropertyName("reset_type")]
    public CreateBillingPlanCreditGrantRequestBodyResetType? ResetType { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
