using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record RetryCustomPlanBillingRequestBody
{
    [JsonPropertyName("activation_strategy")]
    public CustomPlanActivationStrategy? ActivationStrategy { get; set; }

    /// <summary>
    /// The date the subscription's billing period renews on. Only honored when the retry creates a subscription.
    /// </summary>
    [JsonPropertyName("billing_cycle_anchor")]
    public DateTime? BillingCycleAnchor { get; set; }

    [JsonPropertyName("customer_email")]
    public required string CustomerEmail { get; set; }

    [JsonPropertyName("days_until_due")]
    public long? DaysUntilDue { get; set; }

    /// <summary>
    /// When true, the partial period between the subscription starting and its renewal date is billed pro rata straight away. When false that period is free and no invoice is raised until the renewal date. Only applies alongside billing_cycle_anchor. Defaults to true.
    /// </summary>
    [JsonPropertyName("prorate_first_period")]
    public bool? ProrateFirstPeriod { get; set; }

    /// <summary>
    /// Whether Stripe emails the invoice when it is finalized. Defaults to true.
    /// </summary>
    [JsonPropertyName("send_invoice")]
    public bool? SendInvoice { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
