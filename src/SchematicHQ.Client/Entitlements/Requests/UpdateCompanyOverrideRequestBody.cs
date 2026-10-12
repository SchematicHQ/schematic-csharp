using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdateCompanyOverrideRequestBody
{
    [JsonPropertyName("credit_consumption_rate")]
    public double? CreditConsumptionRate { get; set; }

    [JsonPropertyName("expiration_date")]
    public DateTime? ExpirationDate { get; set; }

    [JsonPropertyName("metric_period")]
    public MetricPeriod? MetricPeriod { get; set; }

    [JsonPropertyName("metric_period_month_reset")]
    public MetricPeriodMonthReset? MetricPeriodMonthReset { get; set; }

    [JsonPropertyName("note")]
    public string? Note { get; set; }

    /// <summary>
    /// Credits per unit of the quantity (per token for inference features), keyed by quantity key. Inference features may price input_tokens, output_tokens, cached_input_tokens, cache_creation_input_tokens and reasoning_tokens; reasoning_tokens is priced on top of output_tokens, so leave its rate at zero for providers that count reasoning inside output. Omit to leave the schedule as it is; send null to clear it.
    /// </summary>
    [JsonPropertyName("quantity_rates")]
    public Dictionary<string, double>? QuantityRates { get; set; }

    [JsonPropertyName("value_bool")]
    public bool? ValueBool { get; set; }

    [JsonPropertyName("value_credit_id")]
    public string? ValueCreditId { get; set; }

    [JsonPropertyName("value_numeric")]
    public long? ValueNumeric { get; set; }

    [JsonPropertyName("value_trait_id")]
    public string? ValueTraitId { get; set; }

    [JsonPropertyName("value_type")]
    public required EntitlementValueType ValueType { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
