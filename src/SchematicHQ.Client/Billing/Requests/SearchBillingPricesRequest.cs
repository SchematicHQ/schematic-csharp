using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record SearchBillingPricesRequest
{
    public IEnumerable<string> Ids { get; set; } = new List<string>();

    public string? Q { get; set; }

    public string? Interval { get; set; }

    public SearchBillingPricesRequestUsageType? UsageType { get; set; }

    public int? Price { get; set; }

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
