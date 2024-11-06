namespace SchematicHQ.Client;

public record ListCompanyOverridesRequest
{
    /// <summary>
    /// Filter company overrides by a single company ID (starting with comp\_)
    /// </summary>
    public string? CompanyId { get; set; }

    /// <summary>
    /// Filter company overrides by multiple company IDs (starting with comp\_)
    /// </summary>
    public IEnumerable<string> CompanyIds { get; set; } = new List<string>();

    /// <summary>
    /// Filter company overrides by a single feature ID (starting with feat\_)
    /// </summary>
    public string? FeatureId { get; set; }

    /// <summary>
    /// Filter company overrides by multiple feature IDs (starting with feat\_)
    /// </summary>
    public IEnumerable<string> FeatureIds { get; set; } = new List<string>();

    /// <summary>
    /// Filter company overrides by multiple company override IDs (starting with cmov\_)
    /// </summary>
    public IEnumerable<string> Ids { get; set; } = new List<string>();

    /// <summary>
    /// Search for company overrides by feature or company name
    /// </summary>
    public string? Q { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; set; }
}
