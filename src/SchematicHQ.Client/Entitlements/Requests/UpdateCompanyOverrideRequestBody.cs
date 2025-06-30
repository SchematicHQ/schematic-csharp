using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdateCompanyOverrideRequestBody
{
    [JsonPropertyName("expiration_date")]
    public DateTime? ExpirationDate { get; set; }

    [JsonPropertyName("metric_period")]
    public UpdateCompanyOverrideRequestBodyMetricPeriod? MetricPeriod { get; set; }

    [JsonPropertyName("metric_period_month_reset")]
    public UpdateCompanyOverrideRequestBodyMetricPeriodMonthReset? MetricPeriodMonthReset { get; set; }

    [JsonPropertyName("value_bool")]
    public bool? ValueBool { get; set; }

    [JsonPropertyName("value_credit_id")]
    public string? ValueCreditId { get; set; }

    [JsonPropertyName("value_numeric")]
    public int? ValueNumeric { get; set; }

    [JsonPropertyName("value_trait_id")]
    public string? ValueTraitId { get; set; }

    [JsonPropertyName("value_type")]
    public required UpdateCompanyOverrideRequestBodyValueType ValueType { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
