using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record ConditionResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("account_id")]
    public required string AccountId { get; set; }

    [JsonPropertyName("comparison_trait_id")]
    public string? ComparisonTraitId { get; set; }

    [JsonPropertyName("condition_group_id")]
    public string? ConditionGroupId { get; set; }

    [JsonPropertyName("condition_type")]
    public required string ConditionType { get; set; }

    [JsonPropertyName("consumption_rate")]
    public double? ConsumptionRate { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("credit_id")]
    public string? CreditId { get; set; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; set; }

    [JsonPropertyName("event_subtype")]
    public string? EventSubtype { get; set; }

    [JsonPropertyName("flag_id")]
    public string? FlagId { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("metric_period")]
    public string? MetricPeriod { get; set; }

    [JsonPropertyName("metric_period_month_reset")]
    public string? MetricPeriodMonthReset { get; set; }

    [JsonPropertyName("metric_value")]
    public int? MetricValue { get; set; }

    [JsonPropertyName("operator")]
    public required string Operator { get; set; }

    [JsonPropertyName("plan_version_id")]
    public string? PlanVersionId { get; set; }

    [JsonPropertyName("resource_unspecified_ids")]
    public IEnumerable<string> ResourceUnspecifiedIds { get; set; } = new List<string>();

    [JsonPropertyName("rule_id")]
    public required string RuleId { get; set; }

    [JsonPropertyName("trait_entity_type")]
    public EntityType? TraitEntityType { get; set; }

    [JsonPropertyName("trait_id")]
    public string? TraitId { get; set; }

    [JsonPropertyName("trait_value")]
    public required string TraitValue { get; set; }

    [JsonPropertyName("trait_value_bool")]
    public required bool TraitValueBool { get; set; }

    [JsonPropertyName("trait_value_date")]
    public DateTime? TraitValueDate { get; set; }

    [JsonPropertyName("trait_value_int")]
    public required int TraitValueInt { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }

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
