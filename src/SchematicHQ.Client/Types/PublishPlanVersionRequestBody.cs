using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PublishPlanVersionRequestBody : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// When the company gets the plan. On a custom-plan first publish, charging the default payment method always activates on payment. On a custom-plan republish, on_payment is only accepted when the amendment is invoiced today (migration_strategy immediate, proration_behavior always_invoice, collection_method send_invoice): the company stays on its current version until that invoice is paid.
    /// </summary>
    [JsonPropertyName("activation_strategy")]
    public CustomPlanActivationStrategy? ActivationStrategy { get; set; }

    [JsonPropertyName("address")]
    public CustomerBillingAddress? Address { get; set; }

    /// <summary>
    /// The date the subscription's billing period renews on. Only honored on a first publish that starts a subscription.
    /// </summary>
    [JsonPropertyName("billing_cycle_anchor")]
    public DateTime? BillingCycleAnchor { get; set; }

    /// <summary>
    /// The date the contract term starts. A past date backdates the subscription so the first invoice covers the term from this date to the renewal date. Requires billing_cycle_anchor. Only honored on a first publish that starts a subscription.
    /// </summary>
    [JsonPropertyName("billing_start_date")]
    public DateTime? BillingStartDate { get; set; }

    /// <summary>
    /// How a custom plan's invoice is collected. send_invoice issues it for the company to pay; charge_automatically charges the default payment method straight away. On a first publish, omitted charges the default payment method when the company has one and the first period is billed, and sends the invoice otherwise. On a republish, omitted means send_invoice, and it is only accepted with migration_strategy immediate and proration_behavior always_invoice.
    /// </summary>
    [JsonPropertyName("collection_method")]
    public BillingCollectionMethod? CollectionMethod { get; set; }

    /// <summary>
    /// A Stripe coupon to apply. On a custom-plan republish it discounts the amendment invoice and is only used when that invoice is raised today; it must exist in Stripe. It is attached to the subscription, so a repeating or forever coupon also discounts the renewals that follow.
    /// </summary>
    [JsonPropertyName("coupon_external_id")]
    public string? CouponExternalId { get; set; }

    [JsonPropertyName("custom_field_values")]
    public IEnumerable<CheckoutFieldValue>? CustomFieldValues { get; set; }

    [JsonPropertyName("customer_email")]
    public string? CustomerEmail { get; set; }

    /// <summary>
    /// Payment terms in days. On a custom-plan republish they apply to the amendment invoice only, leave the subscription's renewal terms unchanged, and are only used when that invoice is raised today and sent rather than charged. Defaults to the subscription's terms.
    /// </summary>
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

    /// <summary>
    /// How Stripe handles the price difference when companies are migrated. With migration_strategy immediate, omitted means create_prorations. With end_of_billing_period only none is accepted and means the same as omitting it: the change lands on the renewal boundary, so there is nothing to prorate. With scheduled any value is accepted and omitted means none. Not accepted with leave.
    /// </summary>
    [JsonPropertyName("proration_behavior")]
    public MigrationProrationBehavior? ProrationBehavior { get; set; }

    /// <summary>
    /// Refuse the publish if any company would be migrated onto the new version
    /// </summary>
    [JsonPropertyName("require_no_migration")]
    public bool? RequireNoMigration { get; set; }

    /// <summary>
    /// When every company moves, for migration_strategy scheduled. Must be in the future; the migration runs within about a minute of this time. Not accepted with other strategies.
    /// </summary>
    [JsonPropertyName("scheduled_at")]
    public DateTime? ScheduledAt { get; set; }

    /// <summary>
    /// Whether Stripe emails the invoice when it is finalized. Defaults to true.
    /// </summary>
    [JsonPropertyName("send_invoice")]
    public bool? SendInvoice { get; set; }

    [JsonPropertyName("tax_id")]
    public TaxIdInput? TaxId { get; set; }

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
