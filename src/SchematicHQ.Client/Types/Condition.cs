using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record Condition : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("account_id")]
    public required string AccountId { get; set; }

    [JsonPropertyName("comparison_trait_definition")]
    public TraitDefinition? ComparisonTraitDefinition { get; set; }

    [JsonPropertyName("condition_type")]
    public required ConditionConditionType ConditionType { get; set; }

    [JsonPropertyName("consumption_rate")]
    public double? ConsumptionRate { get; set; }

    [JsonPropertyName("credit_id")]
    public string? CreditId { get; set; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; set; }

    [JsonPropertyName("event_subtype")]
    public string? EventSubtype { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("metric_period")]
    public ConditionMetricPeriod? MetricPeriod { get; set; }

    [JsonPropertyName("metric_period_month_reset")]
    public ConditionMetricPeriodMonthReset? MetricPeriodMonthReset { get; set; }

    [JsonPropertyName("metric_value")]
    public int? MetricValue { get; set; }

    [JsonPropertyName("operator")]
    public required ConditionOperator Operator { get; set; }

    [JsonPropertyName("resource_ids")]
    public IEnumerable<string> ResourceIds { get; set; } = new List<string>();

    [JsonPropertyName("trait_definition")]
    public TraitDefinition? TraitDefinition { get; set; }

    [JsonPropertyName("trait_value")]
    public required string TraitValue { get; set; }

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
