using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record ListBillingProductsRequest
{
    public IEnumerable<string> Ids { get; set; } = new List<string>();

    public string? Name { get; set; }

    public string? Q { get; set; }

    public string? PriceUsageType { get; set; }

    /// <summary>
    /// Filter products that are not linked to any plan
    /// </summary>
    public bool? WithoutLinkedToPlan { get; set; }

    /// <summary>
    /// Filter products that have zero price for free subscription type
    /// </summary>
    public bool? WithZeroPrice { get; set; }

    /// <summary>
    /// Filter products that have prices
    /// </summary>
    public bool? WithPricesOnly { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
