namespace SchematicHQ.Client;

public record CountUsersRequest
{
    /// <summary>
    /// Filter users by company ID (starts with comp_)
    /// </summary>
    public string? CompanyId { get; init; }

    /// <summary>
    /// Filter users by multiple user IDs (starts with user_)
    /// </summary>
    public string? Ids { get; init; }

    /// <summary>
    /// Filter users by plan ID (starts with plan_)
    /// </summary>
    public string? PlanId { get; init; }

    /// <summary>
    /// Search for users by name, keys or string traits
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
