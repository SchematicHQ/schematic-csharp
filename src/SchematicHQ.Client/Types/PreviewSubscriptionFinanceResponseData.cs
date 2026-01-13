using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PreviewSubscriptionFinanceResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("amount_off")]
    public required int AmountOff { get; set; }

    [JsonPropertyName("due_now")]
    public required int DueNow { get; set; }

    [JsonPropertyName("new_charges")]
    public required int NewCharges { get; set; }

    [JsonPropertyName("percent_off")]
    public required double PercentOff { get; set; }

    [JsonPropertyName("period_start")]
    public required DateTime PeriodStart { get; set; }

    [JsonPropertyName("promo_code_applied")]
    public required bool PromoCodeApplied { get; set; }

    [JsonPropertyName("proration")]
    public required int Proration { get; set; }

    [JsonPropertyName("tax_amount")]
    public int? TaxAmount { get; set; }

    [JsonPropertyName("tax_display_name")]
    public string? TaxDisplayName { get; set; }

    [JsonPropertyName("tax_require_billing_details")]
    public required bool TaxRequireBillingDetails { get; set; }

    [JsonPropertyName("total_per_billing_period")]
    public required int TotalPerBillingPeriod { get; set; }

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
