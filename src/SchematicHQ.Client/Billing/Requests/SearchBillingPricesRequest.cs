namespace SchematicHQ.Client;

public record SearchBillingPricesRequest
{
    public string? Ids { get; init; }

    public string? Interval { get; init; }

    public string? UsageType { get; init; }

    public int? Price { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; init; }
}
