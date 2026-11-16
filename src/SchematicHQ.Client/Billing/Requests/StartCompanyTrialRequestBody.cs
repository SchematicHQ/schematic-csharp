using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record StartCompanyTrialRequestBody
{
    /// <summary>
    /// The unit of how often the plan bills once the trial converts: month or year.
    /// </summary>
    [JsonPropertyName("cadence")]
    public required BillingProductPriceInterval Cadence { get; set; }

    /// <summary>
    /// How many cadence units one billing period spans. Defaults to 1; use 3 with month for quarterly.
    /// </summary>
    [JsonPropertyName("cadence_interval_count")]
    public long? CadenceIntervalCount { get; set; }

    /// <summary>
    /// What happens when the trial reaches its end date: subscribe starts billing, cancel moves the company off the plan. Defaults to the plan group's trial setting.
    /// </summary>
    [JsonPropertyName("end_behavior")]
    public BillingSubscriptionTrialEndSetting? EndBehavior { get; set; }

    [JsonPropertyName("plan_id")]
    public required string PlanId { get; set; }

    /// <summary>
    /// When the trial ends. Defaults to the plan's trial length, then the plan group's, then 14 days. Setting it grants the trial even when the company has used its trial before.
    /// </summary>
    [JsonPropertyName("trial_end")]
    public DateTime? TrialEnd { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
