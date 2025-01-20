namespace SchematicHQ.Client;

public record CountPlanEntitlementsRequest
{
    /// <summary>
    /// Filter plan entitlements by a single feature ID (starting with feat_)
    /// </summary>
    public string? FeatureId { get; init; }

    /// <summary>
    /// Filter plan entitlements by multiple feature IDs (starting with feat_)
    /// </summary>
    public string? FeatureIds { get; init; }

    /// <summary>
    /// Filter plan entitlements by multiple plan entitlement IDs (starting with pltl_)
    /// </summary>
    public string? Ids { get; init; }

    /// <summary>
    /// Filter plan entitlements by a single plan ID (starting with plan_)
    /// </summary>
    public string? PlanId { get; init; }

    /// <summary>
    /// Filter plan entitlements by multiple plan IDs (starting with plan_)
    /// </summary>
    public string? PlanIds { get; init; }

    /// <summary>
    /// Search for plan entitlements by feature or company name
    /// </summary>
    public string? Q { get; init; }

    /// <summary>
    /// Filter plan entitlements only with metered products
    /// </summary>
    public bool? WithMeteredProducts { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; init; }
}
