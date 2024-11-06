using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CreateCompanyOverrideRequestBody
{
    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    [JsonPropertyName("feature_id")]
    public required string FeatureId { get; set; }

    [JsonPropertyName("metric_period")]
    public CreateCompanyOverrideRequestBodyMetricPeriod? MetricPeriod { get; set; }

    [JsonPropertyName("value_bool")]
    public bool? ValueBool { get; set; }

    [JsonPropertyName("value_numeric")]
    public int? ValueNumeric { get; set; }

    [JsonPropertyName("value_trait_id")]
    public string? ValueTraitId { get; set; }

    [JsonPropertyName("value_type")]
    public required CreateCompanyOverrideRequestBodyValueType ValueType { get; set; }
}
