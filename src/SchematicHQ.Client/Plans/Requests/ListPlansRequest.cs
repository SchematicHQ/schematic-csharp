using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record ListPlansRequest
{
    public string? CompanyId { get; init; }

    /// <summary>
    /// Filter out plans that do not have a billing product ID
    /// </summary>
    public bool? HasProductId { get; init; }

    public string? Ids { get; init; }

    /// <summary>
    /// Filter by plan type
    /// </summary>
    public ListPlansRequestPlanType? PlanType { get; init; }

    public string? Q { get; init; }

    /// <summary>
    /// Filter out plans that already have a plan entitlement for the specified feature ID
    /// </summary>
    public string? WithoutEntitlementFor { get; init; }

    /// <summary>
    /// Filter out plans that have a billing product ID
    /// </summary>
    public bool? WithoutProductId { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; init; }
}
