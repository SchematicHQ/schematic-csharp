namespace Schematic.Client;

public class ListCompanyOverridesRequest
{
    public string? CompanyId { get; init; }

    public string? CompanyIds { get; init; }

    public string? FeatureId { get; init; }

    public string? FeatureIds { get; init; }

    public string? Ids { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; init; }
}
