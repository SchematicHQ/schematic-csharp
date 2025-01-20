namespace SchematicHQ.Client;

public record ListCompanyOverridesRequest
{
    /// <summary>
    /// Filter company overrides by a single company ID (starting with comp_)
    /// </summary>
    public string? CompanyId { get; init; }

    /// <summary>
    /// Filter company overrides by multiple company IDs (starting with comp_)
    /// </summary>
    public string? CompanyIds { get; init; }

    /// <summary>
    /// Filter company overrides by a single feature ID (starting with feat_)
    /// </summary>
    public string? FeatureId { get; init; }

    /// <summary>
    /// Filter company overrides by multiple feature IDs (starting with feat_)
    /// </summary>
    public string? FeatureIds { get; init; }

    /// <summary>
    /// Filter company overrides by multiple company override IDs (starting with cmov_)
    /// </summary>
    public string? Ids { get; init; }

    /// <summary>
    /// Filter company overrides by whether they have not expired
    /// </summary>
    public bool? WithoutExpired { get; init; }

    /// <summary>
    /// Search for company overrides by feature or company name
    /// </summary>
    public string? Q { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; init; }
}
