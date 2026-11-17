using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdateCompanyTrialRequestBody
{
    [JsonPropertyName("end_behavior")]
    public BillingSubscriptionTrialEndSetting? EndBehavior { get; set; }

    /// <summary>
    /// Moves the end of the trial, and the first billing date with it.
    /// </summary>
    [JsonPropertyName("trial_end")]
    public DateTime? TrialEnd { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
