using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record UpdatePlanEntitlementRequestBody
{
    [JsonPropertyName("metric_period")]
    public UpdatePlanEntitlementRequestBodyMetricPeriod? MetricPeriod { get; set; }

    [JsonPropertyName("value_bool")]
    public bool? ValueBool { get; set; }

    [JsonPropertyName("value_numeric")]
    public int? ValueNumeric { get; set; }

    [JsonPropertyName("value_trait_id")]
    public string? ValueTraitId { get; set; }

    [JsonPropertyName("value_type")]
    public required UpdatePlanEntitlementRequestBodyValueType ValueType { get; set; }
}
