using System.Text.Json.Serialization;

namespace Schematic.Client;

public class CountFeatureUsageParams
{
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; init; }

    [JsonPropertyName("company_keys")]
    public Dictionary<string, object>? CompanyKeys { get; init; }

    [JsonPropertyName("feature_ids")]
    public List<string>? FeatureIds { get; init; }

    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    [JsonPropertyName("offset")]
    public int? Offset { get; init; }

    [JsonPropertyName("q")]
    public string? Q { get; init; }
}
