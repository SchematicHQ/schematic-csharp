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

    [JsonPropertyName("plan_id")]
    public required string PlanId { get; set; }

    [JsonPropertyName("reset_cadence")]
    public required CreateBillingPlanCreditGrantRequestBodyResetCadence ResetCadence { get; set; }

    [JsonPropertyName("reset_start")]
    public required CreateBillingPlanCreditGrantRequestBodyResetStart ResetStart { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
