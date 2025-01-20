namespace SchematicHQ.Client;

public record CountCompaniesRequest
{
    /// <summary>
    /// Filter companies by multiple company IDs (starts with comp_)
    /// </summary>
    public string? Ids { get; init; }

    /// <summary>
    /// Filter companies by plan ID (starts with plan_)
    /// </summary>
    public string? PlanId { get; init; }

    /// <summary>
    /// Search for companies by name, keys or string traits
    /// </summary>
    public string? Q { get; init; }

    /// <summary>
    /// Filter out companies that already have a company override for the specified feature ID
    /// </summary>
    public string? WithoutFeatureOverrideFor { get; init; }

    /// <summary>
    /// Filter out companies that have a plan
    /// </summary>
    public bool? WithoutPlan { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; init; }
}
