namespace SchematicHQ.Client;

public record ListBillingProductsRequest
{
    public string? Ids { get; init; }

    public string? Name { get; init; }

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
