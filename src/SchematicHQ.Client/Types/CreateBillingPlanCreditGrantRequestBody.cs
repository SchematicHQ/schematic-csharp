using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreateBillingPlanCreditGrantRequestBody : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("apply_to_existing")]
    public bool? ApplyToExisting { get; set; }

    /// <summary>
    /// Which boundary closes a monthly arrears window: the subscription's own recurrence (billing_period_start) or the calendar month (month_end). Only applies when arrears_cadence is monthly; defaults to billing_period_start.
    /// </summary>
    [JsonPropertyName("arrears_anchor")]
    public BillingArrearsAnchor? ArrearsAnchor { get; set; }

    /// <summary>
    /// How often postpaid charges are closed and invoiced: end_of_billing_period (the default) or monthly.
    /// </summary>
    [JsonPropertyName("arrears_cadence")]
    public BillingArrearsCadence? ArrearsCadence { get; set; }

    [JsonPropertyName("auto_topup_amount")]
    public long? AutoTopupAmount { get; set; }

    [JsonPropertyName("auto_topup_amount_type")]
    public string? AutoTopupAmountType { get; set; }

    [JsonPropertyName("auto_topup_availability")]
    public BillingCreditAutoTopupAvailability? AutoTopupAvailability { get; set; }

    [JsonPropertyName("auto_topup_enabled")]
    public bool? AutoTopupEnabled { get; set; }

    [JsonPropertyName("auto_topup_expiry_type")]
    public BillingCreditExpiryType? AutoTopupExpiryType { get; set; }

    [JsonPropertyName("auto_topup_expiry_unit")]
    public BillingCreditExpiryUnit? AutoTopupExpiryUnit { get; set; }

    [JsonPropertyName("auto_topup_expiry_unit_count")]
    public long? AutoTopupExpiryUnitCount { get; set; }

    [JsonPropertyName("auto_topup_self_service")]
    public bool? AutoTopupSelfService { get; set; }

    [JsonPropertyName("auto_topup_threshold_credits")]
    public long? AutoTopupThresholdCredits { get; set; }

    [JsonPropertyName("auto_topup_threshold_percent")]
    public long? AutoTopupThresholdPercent { get; set; }

    /// <summary>
    /// Deprecated: use compatible_plan_ids on credit bundles instead. Still accepted; writes through to the credit's bundle compatibility.
    /// </summary>
    [JsonPropertyName("can_buy_bundles")]
    public bool? CanBuyBundles { get; set; }

    /// <summary>
    /// Credits granted once per company on top of the per-license amount. Only valid when scaling is per_license. Defaults to 0.
    /// </summary>
    [JsonPropertyName("company_credit_amount")]
    public long? CompanyCreditAmount { get; set; }

    [JsonPropertyName("credit_amount")]
    public required long CreditAmount { get; set; }

    [JsonPropertyName("credit_id")]
    public required string CreditId { get; set; }

    [JsonPropertyName("expiry_type")]
    public BillingCreditExpiryType? ExpiryType { get; set; }

    [JsonPropertyName("expiry_unit")]
    public BillingCreditExpiryUnit? ExpiryUnit { get; set; }

    [JsonPropertyName("expiry_unit_count")]
    public long? ExpiryUnitCount { get; set; }

    /// <summary>
    /// The license whose quantity scales this grant. Required when scaling is per_license.
    /// </summary>
    [JsonPropertyName("license_id")]
    public string? LicenseId { get; set; }

    /// <summary>
    /// Optional limit on how far the balance may go below zero, in credits. It is a floor on the balance rather than an allowance per invoice window: the balance may run down to minus this figure, and beyond it the flag check denies the same way an exhausted balance does with postpaid off. Nothing resets when an invoice window rolls, so a company that reaches the limit stays denied until a new grant lands or the negative balance is settled. Omit for no limit.
    /// </summary>
    [JsonPropertyName("overdraft_limit")]
    public double? OverdraftLimit { get; set; }

    [JsonPropertyName("plan_id")]
    public required string PlanId { get; set; }

    [JsonPropertyName("plan_version_id")]
    public string? PlanVersionId { get; set; }

    /// <summary>
    /// Whether consumption may continue past a zero balance. When false (the default) the flag check denies once the balance is exhausted, which is the existing behavior. When true, consumption continues and accrues at postpaid_rate_per_unit, settled on arrears_cadence. Intended for invoice-billed customers on net terms, who have no card for auto top-up to charge.
    /// </summary>
    [JsonPropertyName("postpaid_enabled")]
    public bool? PostpaidEnabled { get; set; }

    /// <summary>
    /// Amount charged per credit consumed past a zero balance, in the currency's minor unit. Optional: defaults to the credit's own cost basis (price_per_unit) when postpaid_enabled is true.
    /// </summary>
    [JsonPropertyName("postpaid_rate_per_unit")]
    public long? PostpaidRatePerUnit { get; set; }

    /// <summary>
    /// Decimal string form of postpaid_rate_per_unit, for rates finer than one minor unit (for example 0.0002). Takes precedence over postpaid_rate_per_unit when both are set, matching how the credit's own price_per_unit_decimal behaves.
    /// </summary>
    [JsonPropertyName("postpaid_rate_per_unit_decimal")]
    public string? PostpaidRatePerUnitDecimal { get; set; }

    [JsonPropertyName("reset_cadence")]
    public required BillingPlanCreditGrantResetCadence ResetCadence { get; set; }

    [JsonPropertyName("reset_start")]
    public required BillingPlanCreditGrantResetStart ResetStart { get; set; }

    [JsonPropertyName("reset_type")]
    public BillingPlanCreditGrantResetType? ResetType { get; set; }

    /// <summary>
    /// Percentage of unused credits that carry over when this grant resets. Only applies when reset_type is plan_period. Rolled-over credits expire at the next reset and are not rolled again. Defaults to 0.
    /// </summary>
    [JsonPropertyName("rollover_percentage")]
    public long? RolloverPercentage { get; set; }

    /// <summary>
    /// Whether the grant is a fixed amount per company, or issued once per license the company holds. Defaults to fixed.
    /// </summary>
    [JsonPropertyName("scaling")]
    public PlanCreditGrantScaling? Scaling { get; set; }

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
