using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreateEntitlementReqCommon : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("credit_consumption_rate")]
    public double? CreditConsumptionRate { get; set; }

    [JsonPropertyName("feature_id")]
    public required string FeatureId { get; set; }

    [JsonPropertyName("metric_period")]
    public MetricPeriod? MetricPeriod { get; set; }

    [JsonPropertyName("metric_period_month_reset")]
    public MetricPeriodMonthReset? MetricPeriodMonthReset { get; set; }

    /// <summary>
    /// Per-model rate overrides for inference features, keyed by provider and model. Each entry is a complete rate card: its consumption_rate and quantity_rates replace the entitlement's credit_consumption_rate and quantity_rates for events from that provider and model. An event matches an entry by exact model name first, then by its model name without a trailing snapshot date (claude-sonnet-4-5-20250929 matches claude-sonnet-4-5). Models with no entry burn at the entitlement's own rates. Omit to leave the overrides as they are; send null to clear them.
    /// </summary>
    [JsonPropertyName("model_rates")]
    public IEnumerable<CreditModelRate>? ModelRates { get; set; }

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
