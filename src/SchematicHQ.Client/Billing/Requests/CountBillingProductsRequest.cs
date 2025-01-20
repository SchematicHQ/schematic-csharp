namespace SchematicHQ.Client;

public record CountBillingProductsRequest
{
    public string? Ids { get; init; }

    public string? Name { get; init; }

    public string? Q { get; init; }

    public string? PriceUsageType { get; init; }

    /// <summary>
    /// Filter products that are not linked to any plan
    /// </summary>
    public bool? WithoutLinkedToPlan { get; init; }

    /// <summary>
    /// Filter products that have zero price for free subscription type
    /// </summary>
    public bool? WithZeroPrice { get; init; }

    /// <summary>
    /// Filter products that have prices
    /// </summary>
    public bool? WithPricesOnly { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; init; }
}
