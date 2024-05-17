namespace SchematicHQ.Client;

public class ListFeatureUsageRequest
{
    public string? CompanyId { get; init; }

    public Dictionary<string, object>? CompanyKeys { get; init; }

    public string? FeatureIds { get; init; }

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
