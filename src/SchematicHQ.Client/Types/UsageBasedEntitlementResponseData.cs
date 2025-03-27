using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record UsageBasedEntitlementResponseData
{
    [JsonPropertyName("feature_id")]
    public required string FeatureId { get; set; }

    [JsonPropertyName("metered_price")]
    public BillingPriceView? MeteredPrice { get; set; }

    [JsonPropertyName("metric_period")]
    public string? MetricPeriod { get; set; }

    [JsonPropertyName("metric_period_month_reset")]
    public string? MetricPeriodMonthReset { get; set; }

    [JsonPropertyName("monthly_usage_based_price")]
    public BillingPriceView? MonthlyUsageBasedPrice { get; set; }

    [JsonPropertyName("price_behavior")]
    public string? PriceBehavior { get; set; }

    [JsonPropertyName("value_bool")]
    public bool? ValueBool { get; set; }

    [JsonPropertyName("value_numeric")]
    public int? ValueNumeric { get; set; }

    [JsonPropertyName("value_type")]
    public required string ValueType { get; set; }

    [JsonPropertyName("yearly_usage_based_price")]
    public BillingPriceView? YearlyUsageBasedPrice { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
