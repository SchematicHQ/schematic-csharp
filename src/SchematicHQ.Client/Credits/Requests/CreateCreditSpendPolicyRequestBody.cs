using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreateCreditSpendPolicyRequestBody
{
    [JsonPropertyName("billing_credit_id")]
    public required string BillingCreditId { get; set; }

    /// <summary>
    /// The company the cap applies in. Alone, it caps the whole company. With user_id, it caps that user's spend in this company. Required with user_id when the user belongs to more than one company.
    /// </summary>
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    [JsonPropertyName("label")]
    public string? Label { get; set; }

    /// <summary>
    /// The largest number of credits a single draw may spend. Set either this or window_amount.
    /// </summary>
    [JsonPropertyName("max_per_draw")]
    public double? MaxPerDraw { get; set; }

    /// <summary>
    /// The user the cap applies to. The user must belong to company_id, and with no company_id, to exactly one company.
    /// </summary>
    [JsonPropertyName("user_id")]
    public string? UserId { get; set; }

    /// <summary>
    /// The number of credits the company or user may spend in one window. Set either this or max_per_draw.
    /// </summary>
    [JsonPropertyName("window_amount")]
    public double? WindowAmount { get; set; }

    /// <summary>
    /// The window that window_amount applies to: one UTC hour or one UTC day. Required with window_amount.
    /// </summary>
    [JsonPropertyName("window_unit")]
    public CreditSpendWindowUnit? WindowUnit { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
