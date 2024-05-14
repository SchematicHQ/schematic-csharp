namespace Schematic.Client;

public class ListCompanyPlansRequest
{
    public string? CompanyId { get; init; }

    public string? PlanId { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; init; }
}
