namespace SchematicHQ.Client;

public record GetActiveCompanySubscriptionRequest
{
    public string? CompanyId { get; init; }

    public string? CompanyIds { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; init; }
}
