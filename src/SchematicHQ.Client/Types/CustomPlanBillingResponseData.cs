using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CustomPlanBillingResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("activation_strategy")]
    public required CustomPlanActivationStrategy ActivationStrategy { get; set; }

    /// <summary>
    /// The billing period renewal date pinned when the subscription started, when one was set. When no invoice exists yet, the first invoice is raised on this date.
    /// </summary>
    [JsonPropertyName("billing_cycle_anchor")]
    public DateTime? BillingCycleAnchor { get; set; }

    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("days_until_due")]
    public required long DaysUntilDue { get; set; }

    [JsonPropertyName("external_invoice_id")]
    public string? ExternalInvoiceId { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("paid_at")]
    public DateTime? PaidAt { get; set; }

    /// <summary>
    /// The flow that created this billing record: a custom plan, or a standard plan assigned by invoice through Manage Plan.
    /// </summary>
    [JsonPropertyName("plan_billing_source")]
    public required PlanBillingSource PlanBillingSource { get; set; }

    [JsonPropertyName("plan_id")]
    public required string PlanId { get; set; }

    [JsonPropertyName("published_at")]
    public DateTime? PublishedAt { get; set; }

    [JsonPropertyName("send_invoice")]
    public required bool SendInvoice { get; set; }

    [JsonPropertyName("status")]
    public required CustomPlanBillingStatus Status { get; set; }

    [JsonPropertyName("stripe_invoice_url")]
    public string? StripeInvoiceUrl { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
