using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class CompanyOverrideResponseData
{
    [JsonPropertyName("company")]
    public CompanyDetailResponseData? Company { get; init; }

    [JsonPropertyName("company_id")]
    public string CompanyId { get; init; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("environment_id")]
    public string EnvironmentId { get; init; }

    [JsonPropertyName("feature")]
    public FeatureResponseData? Feature { get; init; }

    [JsonPropertyName("feature_id")]
    public string FeatureId { get; init; }

    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("metric_period")]
    public string? MetricPeriod { get; init; }

    [JsonPropertyName("rule_id")]
    public string RuleId { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; init; }

    [JsonPropertyName("value_bool")]
    public bool? ValueBool { get; init; }

    [JsonPropertyName("value_numeric")]
    public int? ValueNumeric { get; init; }

    [JsonPropertyName("value_trait")]
    public EntityTraitDefinitionResponseData? ValueTrait { get; init; }

    [JsonPropertyName("value_trait_id")]
    public string? ValueTraitId { get; init; }

    [JsonPropertyName("value_type")]
    public string ValueType { get; init; }
}
