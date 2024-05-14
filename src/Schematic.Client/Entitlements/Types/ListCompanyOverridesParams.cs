using System.Text.Json.Serialization;

namespace Schematic.Client;

public class ListCompanyOverridesParams
{
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; init; }

    [JsonPropertyName("company_ids")]
    public List<string>? CompanyIds { get; init; }

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
}
