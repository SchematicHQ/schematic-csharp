using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record ListCompanyOverridesRequest
{
    /// <summary>
    /// Filter company overrides by a single company ID (starting with comp_)
    /// </summary>
    public string? CompanyId { get; set; }

    /// <summary>
    /// Filter company overrides by multiple company IDs (starting with comp_)
    /// </summary>
    public IEnumerable<string> CompanyIds { get; set; } = new List<string>();

    /// <summary>
    /// Filter company overrides by a single feature ID (starting with feat_)
    /// </summary>
    public string? FeatureId { get; set; }

    /// <summary>
    /// Filter company overrides by multiple feature IDs (starting with feat_)
    /// </summary>
    public IEnumerable<string> FeatureIds { get; set; } = new List<string>();

    /// <summary>
    /// Filter company overrides by multiple company override IDs (starting with cmov_)
    /// </summary>
    public IEnumerable<string> Ids { get; set; } = new List<string>();

    /// <summary>
    /// Filter company overrides by whether they have not expired
    /// </summary>
    public bool? WithoutExpired { get; set; }

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

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
