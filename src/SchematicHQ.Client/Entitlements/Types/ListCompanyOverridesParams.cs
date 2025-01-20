using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListCompanyOverridesParams
{
    /// <summary>
    /// Filter company overrides by a single company ID (starting with comp_)
    /// </summary>
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; init; }

    /// <summary>
    /// Filter company overrides by multiple company IDs (starting with comp_)
    /// </summary>
    [JsonPropertyName("company_ids")]
    public IEnumerable<string>? CompanyIds { get; init; }

    /// <summary>
    /// Filter company overrides by a single feature ID (starting with feat_)
    /// </summary>
    [JsonPropertyName("feature_id")]
    public string? FeatureId { get; init; }

    /// <summary>
    /// Filter company overrides by multiple feature IDs (starting with feat_)
    /// </summary>
    [JsonPropertyName("feature_ids")]
    public IEnumerable<string>? FeatureIds { get; init; }

    /// <summary>
    /// Filter company overrides by multiple company override IDs (starting with cmov_)
    /// </summary>
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

    /// <summary>
    /// Search for company overrides by feature or company name
    /// </summary>
    [JsonPropertyName("q")]
    public string? Q { get; init; }

    /// <summary>
    /// Filter company overrides by whether they have not expired
    /// </summary>
    [JsonPropertyName("without_expired")]
    public bool? WithoutExpired { get; init; }
}
