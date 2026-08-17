using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record ManagePlanRequest : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// If true, the company gets the plan only once the first invoice is paid. Only applies to an invoiced subscription. Defaults to false.
    /// </summary>
    [JsonPropertyName("activate_on_payment")]
    public bool? ActivateOnPayment { get; set; }

    [JsonPropertyName("add_on_selections")]
    public IEnumerable<PlanSelection> AddOnSelections { get; set; } = new List<PlanSelection>();

    [JsonPropertyName("base_plan_id")]
    public string? BasePlanId { get; set; }

    [JsonPropertyName("base_plan_price_id")]
    public string? BasePlanPriceId { get; set; }

    [JsonPropertyName("base_plan_version_id")]
    public string? BasePlanVersionId { get; set; }

    /// <summary>
    /// The date the subscription's billing period renews on. Only honored when starting a new subscription; changing the anchor on an existing subscription is not supported.
    /// </summary>
    [JsonPropertyName("billing_cycle_anchor")]
    public DateTime? BillingCycleAnchor { get; set; }

    /// <summary>
    /// Address the invoice is sent to. Required when collection_method is send_invoice.
    /// </summary>
    [JsonPropertyName("billing_email")]
    public string? BillingEmail { get; set; }

    /// <summary>
    /// The company that pays for this subscription. Must already have a Stripe customer. Only honored when starting a new subscription.
    /// </summary>
    [JsonPropertyName("billing_entity_id")]
    public string? BillingEntityId { get; set; }

    /// <summary>
    /// If false, subscription cancels at period end. Only applies when removing all plans. Defaults to true.
    /// </summary>
    [JsonPropertyName("cancel_immediately")]
    public bool? CancelImmediately { get; set; }

    /// <summary>
    /// How the subscription is paid: charged to a payment method on file, or invoiced with payment terms. Invoicing is only available when starting a new subscription. Defaults to charge_automatically.
    /// </summary>
    [JsonPropertyName("collection_method")]
    public BillingCollectionMethod? CollectionMethod { get; set; }

    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    [JsonPropertyName("coupon_external_id")]
    public string? CouponExternalId { get; set; }

    [JsonPropertyName("credit_bundles")]
    public IEnumerable<UpdateCreditBundleRequestBody> CreditBundles { get; set; } =
        new List<UpdateCreditBundleRequestBody>();

    [JsonPropertyName("custom_field_values")]
    public IEnumerable<CheckoutFieldValue> CustomFieldValues { get; set; } =
        new List<CheckoutFieldValue>();

    /// <summary>
    /// Payment terms in days for an invoiced subscription. Defaults to 30.
    /// </summary>
    [JsonPropertyName("days_until_due")]
    public long? DaysUntilDue { get; set; }

    [JsonPropertyName("pay_in_advance_entitlements")]
    public IEnumerable<UpdatePayInAdvanceRequestBody> PayInAdvanceEntitlements { get; set; } =
        new List<UpdatePayInAdvanceRequestBody>();

    [JsonPropertyName("payment_method_external_id")]
    public string? PaymentMethodExternalId { get; set; }

    [JsonPropertyName("promo_code")]
    public string? PromoCode { get; set; }

    /// <summary>
    /// If true and cancel_immediately is true, issue prorated credit. Only applies when removing all plans. Defaults to true.
    /// </summary>
    [JsonPropertyName("prorate")]
    public bool? Prorate { get; set; }

    /// <summary>
    /// When true, the partial period between the subscription starting and its renewal date is billed pro rata straight away. When false that period is free and no invoice is raised until the renewal date. Only applies alongside billing_cycle_anchor. Defaults to true.
    /// </summary>
    [JsonPropertyName("prorate_first_period")]
    public bool? ProrateFirstPeriod { get; set; }

    /// <summary>
    /// Whether Stripe emails the invoice when it is finalized. Only applies to an invoiced subscription. Defaults to true.
    /// </summary>
    [JsonPropertyName("send_invoice")]
    public bool? SendInvoice { get; set; }

    [JsonPropertyName("trial_end")]
    public DateTime? TrialEnd { get; set; }

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
