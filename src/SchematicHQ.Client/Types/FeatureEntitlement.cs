using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record FeatureEntitlement : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// If the company has a numeric entitlement for this feature, the allocated amount
    /// </summary>
    [JsonPropertyName("allocation")]
    public int? Allocation { get; set; }

    /// <summary>
    /// If the company has a credit-based entitlement for this feature, the ID of the credit
    /// </summary>
    [JsonPropertyName("credit_id")]
    public string? CreditId { get; set; }

    /// <summary>
    /// If the company has a credit-based entitlement for this feature, the remaining credit amount
    /// </summary>
    [JsonPropertyName("credit_remaining")]
    public double? CreditRemaining { get; set; }

    /// <summary>
    /// If the company has a credit-based entitlement for this feature, the total credit amount
    /// </summary>
    [JsonPropertyName("credit_total")]
    public double? CreditTotal { get; set; }

    /// <summary>
    /// If the company has a credit-based entitlement for this feature, the amount of credit used
    /// </summary>
    [JsonPropertyName("credit_used")]
    public double? CreditUsed { get; set; }

    /// <summary>
    /// If the feature is event-based, the name of the event tracked for usage
    /// </summary>
    [JsonPropertyName("event_name")]
    public string? EventName { get; set; }

    /// <summary>
    /// The ID of the feature
    /// </summary>
    [JsonPropertyName("feature_id")]
    public required string FeatureId { get; set; }

    /// <summary>
    /// The key of the flag associated with the feature
    /// </summary>
    [JsonPropertyName("feature_key")]
    public required string FeatureKey { get; set; }

    /// <summary>
    /// For event-based feature entitlements, the period over which usage is tracked
    /// </summary>
    [JsonPropertyName("metric_period")]
    public FeatureEntitlementMetricPeriod? MetricPeriod { get; set; }

    /// <summary>
    /// For event-based feature entitlements, when the usage period will reset
    /// </summary>
    [JsonPropertyName("metric_reset_at")]
    public DateTime? MetricResetAt { get; set; }

    /// <summary>
    /// For event-based feature entitlements that have a monthly period, whether that monthly reset is based on the calendar month or a billing cycle
    /// </summary>
    [JsonPropertyName("month_reset")]
    public FeatureEntitlementMonthReset? MonthReset { get; set; }

    /// <summary>
    /// For usage-based pricing, the soft limit for overage charges or the next tier boundary
    /// </summary>
    [JsonPropertyName("soft_limit")]
    public int? SoftLimit { get; set; }

    /// <summary>
    /// If the company has a numeric entitlement for this feature, the current usage amount
    /// </summary>
    [JsonPropertyName("usage")]
    public int? Usage { get; set; }

    [JsonPropertyName("value_type")]
    public required EntitlementValueType ValueType { get; set; }

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
