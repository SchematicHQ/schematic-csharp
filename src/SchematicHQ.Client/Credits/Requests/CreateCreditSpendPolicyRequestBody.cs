using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreateCreditSpendPolicyRequestBody
{
    [JsonPropertyName("billing_credit_id")]
    public required string BillingCreditId { get; set; }

    /// <summary>
    /// The company the cap applies to. Set exactly one of company_id and user_id.
    /// </summary>
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    [JsonPropertyName("label")]
    public string? Label { get; set; }

    /// <summary>
    /// The largest number of credits a single draw may spend.
    /// </summary>
    [JsonPropertyName("max_per_draw")]
    public required double MaxPerDraw { get; set; }

    /// <summary>
    /// The user the cap applies to. Set exactly one of company_id and user_id.
    /// </summary>
    [JsonPropertyName("user_id")]
    public string? UserId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
