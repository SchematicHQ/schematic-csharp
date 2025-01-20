using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record UpdateCompanyOverrideRequestBody
{
    [JsonPropertyName("expiration_date")]
    public DateTime? ExpirationDate { get; init; }

    [JsonPropertyName("metric_period")]
    public UpdateCompanyOverrideRequestBodyMetricPeriod? MetricPeriod { get; init; }

    [JsonPropertyName("metric_period_month_reset")]
    public UpdateCompanyOverrideRequestBodyMetricPeriodMonthReset? MetricPeriodMonthReset { get; init; }

    [JsonPropertyName("value_bool")]
    public bool? ValueBool { get; init; }

    [JsonPropertyName("value_numeric")]
    public int? ValueNumeric { get; init; }

    [JsonPropertyName("value_trait_id")]
    public string? ValueTraitId { get; init; }

    [JsonPropertyName("value_type")]
    public required UpdateCompanyOverrideRequestBodyValueType ValueType { get; init; }
}
