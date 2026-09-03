using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PublishPlanVersionRequestBody
{
    [JsonPropertyName("activation_strategy")]
    public CustomPlanActivationStrategy? ActivationStrategy { get; set; }

    [JsonPropertyName("address")]
    public CustomerBillingAddress? Address { get; set; }

    /// <summary>
    /// The date the subscription's billing period renews on. Only honored on a first publish that starts a subscription.
    /// </summary>
    [JsonPropertyName("billing_cycle_anchor")]
    public DateTime? BillingCycleAnchor { get; set; }

    [JsonPropertyName("coupon_external_id")]
    public string? CouponExternalId { get; set; }

    [JsonPropertyName("custom_field_values")]
    public IEnumerable<CheckoutFieldValue>? CustomFieldValues { get; set; }

    [JsonPropertyName("customer_email")]
    public string? CustomerEmail { get; set; }

    [JsonPropertyName("days_until_due")]
    public long? DaysUntilDue { get; set; }

    [JsonPropertyName("excluded_company_ids")]
    public IEnumerable<string> ExcludedCompanyIds { get; set; } = new List<string>();

    [JsonPropertyName("migration_strategy")]
    public required PlanVersionMigrationStrategy MigrationStrategy { get; set; }

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    /// <summary>
    /// When true, the partial period between the subscription starting and its renewal date is billed pro rata straight away. When false that period is free and no invoice is raised until the renewal date. Only applies alongside billing_cycle_anchor. Defaults to true.
    /// </summary>
    [JsonPropertyName("prorate_first_period")]
    public bool? ProrateFirstPeriod { get; set; }

    [JsonPropertyName("proration_behavior")]
    public MigrationProrationBehavior? ProrationBehavior { get; set; }

    /// <summary>
    /// Refuse the publish if any company would be migrated onto the new version
    /// </summary>
    [JsonPropertyName("require_no_migration")]
    public bool? RequireNoMigration { get; set; }

    /// <summary>
    /// Whether Stripe emails the invoice when it is finalized. Defaults to true.
    /// </summary>
    [JsonPropertyName("send_invoice")]
    public bool? SendInvoice { get; set; }

    [JsonPropertyName("tax_id")]
    public TaxIdInput? TaxId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
