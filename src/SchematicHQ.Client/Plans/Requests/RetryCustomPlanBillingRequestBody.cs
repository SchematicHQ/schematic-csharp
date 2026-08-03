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
