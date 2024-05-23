using System.Text.Json.Serialization;
using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class CreateCompanyOverrideRequestBody
{
    [JsonPropertyName("company_id")]
    public string CompanyId { get; init; }

    [JsonPropertyName("feature_id")]
    public string FeatureId { get; init; }

    [JsonPropertyName("metric_period")]
    public CreateCompanyOverrideRequestBodyMetricPeriod? MetricPeriod { get; init; }

    [JsonPropertyName("value_bool")]
    public bool? ValueBool { get; init; }

    [JsonPropertyName("value_numeric")]
    public int? ValueNumeric { get; init; }

    [JsonPropertyName("value_trait_id")]
    public string? ValueTraitId { get; init; }

    [JsonPropertyName("value_type")]
    public CreateCompanyOverrideRequestBodyValueType ValueType { get; init; }
}
