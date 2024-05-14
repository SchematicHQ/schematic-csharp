using System.Text.Json.Serialization;

namespace Schematic.Client;

public class CountCompaniesParams
{
    [JsonPropertyName("ids")]
    public List<string>? Ids { get; init; }

    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    [JsonPropertyName("offset")]
    public int? Offset { get; init; }

    [JsonPropertyName("plan_id")]
    public string? PlanId { get; init; }

    [JsonPropertyName("q")]
    public string? Q { get; init; }

    [JsonPropertyName("without_feature_override_for")]
    public string? WithoutFeatureOverrideFor { get; init; }
}
