namespace SchematicHQ.Client;

public record CountPlansRequest
{
    public string? CompanyId { get; init; }

    public string? Ids { get; init; }

    public string? Q { get; init; }

    /// <summary>
    /// Filter out plans that already have a plan entitlement for the specified feature ID
    /// </summary>
    public string? WithoutEntitlementFor { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; init; }
}
