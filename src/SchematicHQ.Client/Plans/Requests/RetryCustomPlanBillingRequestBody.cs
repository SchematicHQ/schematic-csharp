using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record RetryCustomPlanBillingRequestBody
{
    [JsonPropertyName("activation_strategy")]
    public CustomPlanActivationStrategy? ActivationStrategy { get; set; }

    [JsonPropertyName("customer_email")]
    public required string CustomerEmail { get; set; }

    [JsonPropertyName("days_until_due")]
    public long? DaysUntilDue { get; set; }

    [JsonPropertyName("pay_in_advance")]
    public IEnumerable<UpdatePayInAdvanceRequestBody> PayInAdvance { get; set; } =
        new List<UpdatePayInAdvanceRequestBody>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
