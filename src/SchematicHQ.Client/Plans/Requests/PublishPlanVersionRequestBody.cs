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
