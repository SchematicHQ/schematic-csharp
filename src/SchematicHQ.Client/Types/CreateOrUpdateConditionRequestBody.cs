using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record CreateOrUpdateConditionRequestBody
{
    /// <summary>
    /// Optionally provide a trait ID to compare a metric or trait value against instead of a value
    /// </summary>
    [JsonPropertyName("comparison_trait_id")]
    public string? ComparisonTraitId { get; set; }

    [JsonPropertyName("condition_type")]
    public required CreateOrUpdateConditionRequestBodyConditionType ConditionType { get; set; }

    /// <summary>
    /// Name of track event type used to measure this condition
    /// </summary>
    [JsonPropertyName("event_subtype")]
    public string? EventSubtype { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Period of time over which to measure the track event metric
    /// </summary>
    [JsonPropertyName("metric_period")]
    public CreateOrUpdateConditionRequestBodyMetricPeriod? MetricPeriod { get; set; }

    /// <summary>
    /// Value to compare the track event metric against
    /// </summary>
    [JsonPropertyName("metric_value")]
    public int? MetricValue { get; set; }

    [JsonPropertyName("operator")]
    public required CreateOrUpdateConditionRequestBodyOperator Operator { get; set; }

    /// <summary>
    /// List of resource IDs (companies, users, or plans) targeted by this condition
    /// </summary>
    [JsonPropertyName("resource_ids")]
    public IEnumerable<string> ResourceIds { get; set; } = new List<string>();

    /// <summary>
    /// ID of trait to use to measure this condition
    /// </summary>
    [JsonPropertyName("trait_id")]
    public string? TraitId { get; set; }

    /// <summary>
    /// Value to compare the trait value against
    /// </summary>
    [JsonPropertyName("trait_value")]
    public string? TraitValue { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
