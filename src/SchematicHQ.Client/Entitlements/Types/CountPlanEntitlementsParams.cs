using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class CountPlanEntitlementsParams
{
    [JsonPropertyName("feature_id")]
    public string? FeatureId { get; init; }

    [JsonPropertyName("feature_ids")]
    public IEnumerable<string>? FeatureIds { get; init; }

    [JsonPropertyName("ids")]
    public IEnumerable<string>? Ids { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonPropertyName("offset")]
    public int? Offset { get; init; }

    [JsonPropertyName("plan_id")]
    public string? PlanId { get; init; }

    [JsonPropertyName("plan_ids")]
    public IEnumerable<string>? PlanIds { get; init; }

    [JsonPropertyName("q")]
    public string? Q { get; init; }
}
