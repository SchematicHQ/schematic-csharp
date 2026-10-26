using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PreviewSubscriptionFinanceResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("amount_off")]
    public required long AmountOff { get; set; }

    /// <summary>
    /// ISO 4217 currency every amount in this block is denominated in.
    /// </summary>
    [JsonPropertyName("currency")]
    public required Currency Currency { get; set; }

    /// <summary>
    /// What the company's stored credit covers of this invoice, in minor units.
    /// </summary>
    [JsonPropertyName("customer_balance_applied")]
    public required long CustomerBalanceApplied { get; set; }

    /// <summary>
    /// Credit left on the company's balance after this invoice, in minor units: what it did not consume, plus what a total below zero adds, which the provider applies to later invoices.
    /// </summary>
    [JsonPropertyName("customer_balance_remaining")]
    public required long CustomerBalanceRemaining { get; set; }

    [JsonPropertyName("discount_amount")]
    public required long DiscountAmount { get; set; }

    [JsonPropertyName("discounts")]
    public IEnumerable<PreviewSubscriptionDiscountResponseData> Discounts { get; set; } =
        new List<PreviewSubscriptionDiscountResponseData>();

    [JsonPropertyName("due_now")]
    public required long DueNow { get; set; }

    [JsonPropertyName("new_charges")]
    public required long NewCharges { get; set; }

    [JsonPropertyName("percent_off")]
    public required double PercentOff { get; set; }

    [JsonPropertyName("period_end")]
    public required DateTime PeriodEnd { get; set; }

    [JsonPropertyName("period_start")]
    public required DateTime PeriodStart { get; set; }

    [JsonPropertyName("promo_code_applied")]
    public required bool PromoCodeApplied { get; set; }

    [JsonPropertyName("proration")]
    public required long Proration { get; set; }

    [JsonPropertyName("proration_billed_at")]
    public DateTime? ProrationBilledAt { get; set; }

    /// <summary>
    /// The proration itemized in the cart's terms, by plan and price. Null when the change prorates nothing. upcoming_invoice_line_items stays as the billing provider reported it.
    /// </summary>
    [JsonPropertyName("proration_detail")]
    public PreviewSubscriptionProrationResponseData? ProrationDetail { get; set; }

    [JsonPropertyName("tax_amount")]
    public long? TaxAmount { get; set; }

    [JsonPropertyName("tax_display_name")]
    public string? TaxDisplayName { get; set; }

    [JsonPropertyName("tax_require_billing_details")]
    public required bool TaxRequireBillingDetails { get; set; }

    [JsonPropertyName("total_per_billing_period")]
    public required long TotalPerBillingPeriod { get; set; }

    [JsonPropertyName("trial_end")]
    public DateTime? TrialEnd { get; set; }

    [JsonPropertyName("upcoming_invoice_line_items")]
    public IEnumerable<PreviewSubscriptionUpcomingInvoiceLineItems> UpcomingInvoiceLineItems { get; set; } =
        new List<PreviewSubscriptionUpcomingInvoiceLineItems>();

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
