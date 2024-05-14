namespace Schematic.Client;

public class ListMetricCountsRequest
{
    public DateTime? StartTime { get; init; }

    public DateTime? EndTime { get; init; }

    public string? EventSubtype { get; init; }

    public string? EventSubtypes { get; init; }

    public string? CompanyId { get; init; }

    public string? CompanyIds { get; init; }

    public string? UserId { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; init; }

    public string? Grouping { get; init; }
}
