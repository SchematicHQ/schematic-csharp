namespace Schematic.Client;

public class ListCompaniesRequest
{
    public string? Ids { get; init; }

    public string? PlanId { get; init; }

    /// <summary>
    /// Search filter
    /// </summary>
    public string? Q { get; init; }

    /// <summary>
    /// Filter out companies that already have a company override for the specified feature ID
    /// </summary>
    public string? WithoutFeatureOverrideFor { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; init; }
}
