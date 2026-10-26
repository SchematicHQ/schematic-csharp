using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PublishPreviewResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("billing_email")]
    public string? BillingEmail { get; set; }

    [JsonPropertyName("currency")]
    public Currency? Currency { get; set; }

    [JsonPropertyName("current_period_end")]
    public DateTime? CurrentPeriodEnd { get; set; }

    [JsonPropertyName("current_period_start")]
    public DateTime? CurrentPeriodStart { get; set; }

    /// <summary>
    /// How the amendment invoice is collected when the publish request leaves collection_method out.
    /// </summary>
    [JsonPropertyName("default_collection_method")]
    public required BillingCollectionMethod DefaultCollectionMethod { get; set; }

    /// <summary>
    /// Payment terms the amendment invoice gets when the publish request leaves days_until_due out.
    /// </summary>
    [JsonPropertyName("default_days_until_due")]
    public required long DefaultDaysUntilDue { get; set; }

    /// <summary>
    /// Whether the amendment invoice is emailed when the publish request leaves send_invoice out.
    /// </summary>
    [JsonPropertyName("default_send_invoice")]
    public required bool DefaultSendInvoice { get; set; }

    /// <summary>
    /// When the company moves to the new version. Null when no date can be named yet: the company's only subscription is in dunning or scheduled to cancel.
    /// </summary>
    [JsonPropertyName("effective_at")]
    public DateTime? EffectiveAt { get; set; }

    /// <summary>
    /// When the subscription ends instead of renewing once the amendment is finalized: the end date moved back to a renewal, or the end it already has. Null when it renews.
    /// </summary>
    [JsonPropertyName("ends_at")]
    public DateTime? EndsAt { get; set; }

    [JsonPropertyName("has_billing_changes")]
    public required bool HasBillingChanges { get; set; }

    /// <summary>
    /// What the amendment's own invoice bills, in cents, when lands_on is immediate_invoice, after discounts and tax. Includes pending invoice items Stripe adds to the same invoice. Negative for a downgrade: Stripe raises a credit invoice with nothing due, and the credit goes to the customer's balance against later invoices.
    /// </summary>
    [JsonPropertyName("immediate_invoice_amount")]
    public long? ImmediateInvoiceAmount { get; set; }

    [JsonPropertyName("lands_on")]
    public required AmendmentProrationLandsOn LandsOn { get; set; }

    /// <summary>
    /// What the subscription's items bill per period once the new version is in place, in cents: unit price times quantity over its licensed recurring items, before discounts and tax, the way the subscription's total_price counts its items today. Null when the change is not priced, including a change with no billing impact.
    /// </summary>
    [JsonPropertyName("new_recurring_amount")]
    public long? NewRecurringAmount { get; set; }

    /// <summary>
    /// The next renewal invoice with the change applied, in cents, before any customer balance is applied. A downgrade invoiced now leaves a credit on that balance, so the amount due can be lower. Null when the change is not priced: it lands at the end of the period, or on a date without proration.
    /// </summary>
    [JsonPropertyName("next_invoice_amount")]
    public long? NextInvoiceAmount { get; set; }

    [JsonPropertyName("next_invoice_at")]
    public DateTime? NextInvoiceAt { get; set; }

    [JsonPropertyName("note")]
    public string? Note { get; set; }

    [JsonPropertyName("pay_in_advance")]
    public IEnumerable<PublishPreviewPayInAdvanceResponseData> PayInAdvance { get; set; } =
        new List<PublishPreviewPayInAdvanceResponseData>();

    [JsonPropertyName("payment_method")]
    public required PublishPreviewPaymentMethodResponseData PaymentMethod { get; set; }

    /// <summary>
    /// Sum of the positive proration lines, in cents.
    /// </summary>
    [JsonPropertyName("proration_charge")]
    public required long ProrationCharge { get; set; }

    /// <summary>
    /// Sum of the negative proration lines, in cents.
    /// </summary>
    [JsonPropertyName("proration_credit")]
    public required long ProrationCredit { get; set; }

    [JsonPropertyName("proration_lines")]
    public IEnumerable<PublishPreviewProrationLineResponseData> ProrationLines { get; set; } =
        new List<PublishPreviewProrationLineResponseData>();

    /// <summary>
    /// proration_charge plus proration_credit, in cents, before tax.
    /// </summary>
    [JsonPropertyName("proration_total")]
    public required long ProrationTotal { get; set; }

    [JsonPropertyName("will_update_subscription")]
    public required bool WillUpdateSubscription { get; set; }

    [JsonPropertyName("would_fail")]
    public required bool WouldFail { get; set; }

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
