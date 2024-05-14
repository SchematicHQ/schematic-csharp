using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class CreateOrUpdateConditionRequestBody
{
    /// <summary>
    /// Optionally provide a trait ID to compare a metric or trait value against instead of a value
    /// </summary>
    [JsonPropertyName("comparison_trait_id")]
    public string? ComparisonTraitId { get; init; }

    [JsonPropertyName("condition_type")]
    public CreateOrUpdateConditionRequestBodyConditionType ConditionType { get; init; }

    /// <summary>
    /// Name of track event type used to measure this condition
    /// </summary>
    [JsonPropertyName("event_subtype")]
    public string? EventSubtype { get; init; }

    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Period of time over which to measure the track event metric
    /// </summary>
    [JsonPropertyName("metric_period")]
    public CreateOrUpdateConditionRequestBodyMetricPeriod? MetricPeriod { get; init; }

    /// <summary>
    /// Value to compare the track event metric against
    /// </summary>
    [JsonPropertyName("metric_value")]
    public int MetricValue { get; init; }

    [JsonPropertyName("operator")]
    public CreateOrUpdateConditionRequestBodyOperator Operator { get; init; }

    /// <summary>
    /// List of resource IDs (companies, users, or plans) targeted by this condition
    /// </summary>
    [JsonPropertyName("resource_ids")]
    public List<string> ResourceIds { get; init; }

    /// <summary>
    /// ID of trait to use to measure this condition
    /// </summary>
    [JsonPropertyName("trait_id")]
    public string? TraitId { get; init; }

    /// <summary>
    /// Value to compare the trait value against
    /// </summary>
    [JsonPropertyName("trait_value")]
    public string? TraitValue { get; init; }
}
