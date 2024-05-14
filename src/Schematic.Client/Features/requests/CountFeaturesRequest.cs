namespace Schematic.Client;

public class CountFeaturesRequest
{
    public string? Ids { get; init; }

    public string? Q { get; init; }

    /// <summary>
    /// Filter out features that already have a company override for the specified company ID
    /// </summary>
    public string? WithoutCompanyOverrideFor { get; init; }

    /// <summary>
    /// Filter out features that already have a plan entitlement for the specified plan ID
    /// </summary>
    public string? WithoutPlanEntitlementFor { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; init; }
}
