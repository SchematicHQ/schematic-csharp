using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

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
}
