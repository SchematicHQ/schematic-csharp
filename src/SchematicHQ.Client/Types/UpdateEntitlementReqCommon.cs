using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record UpdateEntitlementReqCommon
{
    [JsonPropertyName("metric_period")]
    public UpdateEntitlementReqCommonMetricPeriod? MetricPeriod { get; set; }

    [JsonPropertyName("metric_period_month_reset")]
    public UpdateEntitlementReqCommonMetricPeriodMonthReset? MetricPeriodMonthReset { get; set; }

    [JsonPropertyName("value_bool")]
    public bool? ValueBool { get; set; }

    [JsonPropertyName("value_numeric")]
    public int? ValueNumeric { get; set; }

    [JsonPropertyName("value_trait_id")]
    public string? ValueTraitId { get; set; }

    [JsonPropertyName("value_type")]
    public required UpdateEntitlementReqCommonValueType ValueType { get; set; }

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
