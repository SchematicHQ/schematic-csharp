namespace SchematicHQ.Client;

public record GetActiveDealsRequest
{
    public required string CompanyId { get; init; }

    public required string DealStage { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; init; }
}
