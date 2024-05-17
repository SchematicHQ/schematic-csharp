using System.Text.Json.Serialization;

namespace SchematicHQ.Client;

public class ListPlanEntitlementsParams
{
    [JsonPropertyName("feature_id")]
    public string? FeatureId { get; init; }

    [JsonPropertyName("feature_ids")]
    public List<string>? FeatureIds { get; init; }

    [JsonPropertyName("ids")]
    public List<string>? Ids { get; init; }

    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    [JsonPropertyName("offset")]
    public int? Offset { get; init; }

    [JsonPropertyName("plan_id")]
    public string? PlanId { get; init; }

    [JsonPropertyName("plan_ids")]
    public List<string>? PlanIds { get; init; }
}
