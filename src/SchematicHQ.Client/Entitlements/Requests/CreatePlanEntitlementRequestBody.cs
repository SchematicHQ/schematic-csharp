using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreatePlanEntitlementRequestBody
{
    [JsonPropertyName("billing_product_id")]
    public string? BillingProductId { get; set; }

    [JsonPropertyName("billing_threshold")]
    public long? BillingThreshold { get; set; }

    [JsonPropertyName("credit_consumption_rate")]
    public double? CreditConsumptionRate { get; set; }

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("currency_prices")]
    public IEnumerable<CurrencyPriceRequestBody>? CurrencyPrices { get; set; }

    [JsonPropertyName("feature_id")]
    public required string FeatureId { get; set; }

    [JsonPropertyName("metric_period")]
    public MetricPeriod? MetricPeriod { get; set; }

    [JsonPropertyName("metric_period_month_reset")]
    public MetricPeriodMonthReset? MetricPeriodMonthReset { get; set; }

    [JsonPropertyName("monthly_metered_price_id")]
    public string? MonthlyMeteredPriceId { get; set; }

    [JsonPropertyName("monthly_price_tiers")]
    public IEnumerable<CreatePriceTierRequestBody>? MonthlyPriceTiers { get; set; }

    [JsonPropertyName("monthly_unit_price")]
    public long? MonthlyUnitPrice { get; set; }

    [JsonPropertyName("monthly_unit_price_decimal")]
    public string? MonthlyUnitPriceDecimal { get; set; }

    /// <summary>
    /// How often overage charges are assessed and invoiced. Defaults to end_of_billing_period, where the billing provider aggregates usage over the subscription's own period and bills it at period end. Set to monthly or quarterly to have overage assessed each month or quarter and billed on its own invoice, which is the point of the setting on annual plans. A quarter charges each month's usage against that month's allowance. Only applies to overage price behavior.
    /// </summary>
    [JsonPropertyName("overage_billing_cadence")]
    public BillingArrearsCadence? OverageBillingCadence { get; set; }

    [JsonPropertyName("overage_billing_product_id")]
    public string? OverageBillingProductId { get; set; }

    /// <summary>
    /// Which boundary closes a monthly or quarterly overage window: the subscription's own recurrence (billing_period_start) or the calendar month (month_end). Quarterly windows run in three-month blocks from the billing period start, held to the subscription's own period, or in calendar quarters at month_end. Only applies when overage_billing_cadence is monthly or quarterly, and must match metric_period_month_reset so each window closes when the allowance resets: billing_period_start for billing_cycle, month_end for first_of_month. Defaults to the anchor that matches the reset.
    /// </summary>
    [JsonPropertyName("overage_invoice_anchor")]
    public BillingArrearsAnchor? OverageInvoiceAnchor { get; set; }

    [JsonPropertyName("plan_id")]
    public required string PlanId { get; set; }

    [JsonPropertyName("plan_version_id")]
    public string? PlanVersionId { get; set; }

    [JsonPropertyName("price_behavior")]
    public EntitlementPriceBehavior? PriceBehavior { get; set; }

    /// <summary>
    /// Use MonthlyPriceTiers or YearlyPriceTiers instead
    /// </summary>
    [JsonPropertyName("price_tiers")]
    public IEnumerable<CreatePriceTierRequestBody>? PriceTiers { get; set; }

    [JsonPropertyName("quarterly_metered_price_id")]
    public string? QuarterlyMeteredPriceId { get; set; }

    [JsonPropertyName("quarterly_price_tiers")]
    public IEnumerable<CreatePriceTierRequestBody>? QuarterlyPriceTiers { get; set; }

    [JsonPropertyName("quarterly_unit_price")]
    public long? QuarterlyUnitPrice { get; set; }

    [JsonPropertyName("quarterly_unit_price_decimal")]
    public string? QuarterlyUnitPriceDecimal { get; set; }

    [JsonPropertyName("soft_limit")]
    public long? SoftLimit { get; set; }

    [JsonPropertyName("tier_mode")]
    public BillingTiersMode? TierMode { get; set; }

    /// <summary>
    /// The committed unit quantity for this entitlement. For custom plans this is the quantity the company is contractually committed to; for standard plans it is the quantity pre-filled when subscribing. Only applies to pay-in-advance entitlements. Note: this is not yet enforced/auto-provisioned as a true default — it is currently stored for downstream billing use.
    /// </summary>
    [JsonPropertyName("usage_quantity")]
    public long? UsageQuantity { get; set; }

    [JsonPropertyName("value_bool")]
    public bool? ValueBool { get; set; }

    [JsonPropertyName("value_credit_id")]
    public string? ValueCreditId { get; set; }

    [JsonPropertyName("value_numeric")]
    public long? ValueNumeric { get; set; }

    [JsonPropertyName("value_trait_id")]
    public string? ValueTraitId { get; set; }

    [JsonPropertyName("value_type")]
    public required EntitlementValueType ValueType { get; set; }

    [JsonPropertyName("warning_tiers")]
    public IEnumerable<WarningTierRequestBody>? WarningTiers { get; set; }

    [JsonPropertyName("yearly_metered_price_id")]
    public string? YearlyMeteredPriceId { get; set; }

    [JsonPropertyName("yearly_price_tiers")]
    public IEnumerable<CreatePriceTierRequestBody>? YearlyPriceTiers { get; set; }

    [JsonPropertyName("yearly_unit_price")]
    public long? YearlyUnitPrice { get; set; }

    [JsonPropertyName("yearly_unit_price_decimal")]
    public string? YearlyUnitPriceDecimal { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
