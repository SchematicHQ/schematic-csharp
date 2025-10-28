using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record FeatureUsageResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Whether further usage is permitted.
    /// </summary>
    [JsonPropertyName("access")]
    public required bool Access { get; set; }

    /// <summary>
    /// The maximum amount of usage that is permitted; a null value indicates that unlimited usage is permitted.
    /// </summary>
    [JsonPropertyName("allocation")]
    public int? Allocation { get; set; }

    /// <summary>
    /// The type of allocation that is being used.
    /// </summary>
    [JsonPropertyName("allocation_type")]
    public required FeatureUsageResponseDataAllocationType AllocationType { get; set; }

    [JsonPropertyName("company_override")]
    public CompanyOverrideResponseData? CompanyOverride { get; set; }

    /// <summary>
    /// The rate at which credits are consumed per unit of usage
    /// </summary>
    [JsonPropertyName("credit_consumption_rate")]
    public double? CreditConsumptionRate { get; set; }

    [JsonPropertyName("credit_grant_counts")]
    public Dictionary<string, double>? CreditGrantCounts { get; set; }

    [JsonPropertyName("credit_grant_details")]
    public IEnumerable<CreditGrantDetail>? CreditGrantDetails { get; set; }

    /// <summary>
    /// Reason for the credit grant
    /// </summary>
    [JsonPropertyName("credit_grant_reason")]
    public FeatureUsageResponseDataCreditGrantReason? CreditGrantReason { get; set; }

    [JsonPropertyName("credit_remaining")]
    public double? CreditRemaining { get; set; }

    [JsonPropertyName("credit_total")]
    public double? CreditTotal { get; set; }

    /// <summary>
    /// Icon identifier for the credit type
    /// </summary>
    [JsonPropertyName("credit_type_icon")]
    public string? CreditTypeIcon { get; set; }

    [JsonPropertyName("credit_used")]
    public double? CreditUsed { get; set; }

    /// <summary>
    /// Effective limit for usage calculations. For overage pricing, this is the soft limit where overage charges begin. For tiered pricing, this is the first tier boundary. For other pricing models, this is the base allocation. Used to calculate usage percentages and determine access thresholds.
    /// </summary>
    [JsonPropertyName("effective_limit")]
    public int? EffectiveLimit { get; set; }

    /// <summary>
    /// Per-unit price for current usage scenario
    /// </summary>
    [JsonPropertyName("effective_price")]
    public double? EffectivePrice { get; set; }

    [JsonPropertyName("entitlement_expiration_date")]
    public DateTime? EntitlementExpirationDate { get; set; }

    [JsonPropertyName("entitlement_id")]
    public required string EntitlementId { get; set; }

    /// <summary>
    /// Source of the entitlement (plan or company_override)
    /// </summary>
    [JsonPropertyName("entitlement_source")]
    public string? EntitlementSource { get; set; }

    [JsonPropertyName("entitlement_type")]
    public required string EntitlementType { get; set; }

    [JsonPropertyName("feature")]
    public FeatureDetailResponseData? Feature { get; set; }

    /// <summary>
    /// Whether a valid allocation exists
    /// </summary>
    [JsonPropertyName("has_valid_allocation")]
    public bool? HasValidAllocation { get; set; }

    /// <summary>
    /// Whether this is an unlimited allocation
    /// </summary>
    [JsonPropertyName("is_unlimited")]
    public bool? IsUnlimited { get; set; }

    /// <summary>
    /// The time at which the metric will reset.
    /// </summary>
    [JsonPropertyName("metric_reset_at")]
    public DateTime? MetricResetAt { get; set; }

    /// <summary>
    /// If the period is current_month, when the month resets.
    /// </summary>
    [JsonPropertyName("month_reset")]
    public string? MonthReset { get; set; }

    [JsonPropertyName("monthly_usage_based_price")]
    public BillingPriceView? MonthlyUsageBasedPrice { get; set; }

    /// <summary>
    /// Amount of usage exceeding soft limit (overage pricing only)
    /// </summary>
    [JsonPropertyName("overuse")]
    public int? Overuse { get; set; }

    /// <summary>
    /// Percentage of allocation consumed (0-100+)
    /// </summary>
    [JsonPropertyName("percent_used")]
    public double? PercentUsed { get; set; }

    /// <summary>
    /// The period over which usage is measured.
    /// </summary>
    [JsonPropertyName("period")]
    public string? Period { get; set; }

    [JsonPropertyName("plan")]
    public PlanResponseData? Plan { get; set; }

    [JsonPropertyName("plan_entitlement")]
    public PlanEntitlementResponseData? PlanEntitlement { get; set; }

    [JsonPropertyName("price_behavior")]
    public string? PriceBehavior { get; set; }

    /// <summary>
    /// The soft limit for the feature usage. Available only for overage price behavior
    /// </summary>
    [JsonPropertyName("soft_limit")]
    public int? SoftLimit { get; set; }

    /// <summary>
    /// The amount of usage that has been consumed; a null value indicates that usage is not being measured.
    /// </summary>
    [JsonPropertyName("usage")]
    public int? Usage { get; set; }

    [JsonPropertyName("yearly_usage_based_price")]
    public BillingPriceView? YearlyUsageBasedPrice { get; set; }

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
