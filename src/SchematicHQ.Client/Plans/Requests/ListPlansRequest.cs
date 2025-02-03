using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record ListPlansRequest
{
    public string? CompanyId { get; set; }

    /// <summary>
    /// Filter out plans that do not have a billing product ID
    /// </summary>
    public bool? HasProductId { get; set; }

    public IEnumerable<string> Ids { get; set; } = new List<string>();

    /// <summary>
    /// Filter by plan type
    /// </summary>
    public ListPlansRequestPlanType? PlanType { get; set; }

    public string? Q { get; set; }

    /// <summary>
    /// Filter out plans that already have a plan entitlement for the specified feature ID
    /// </summary>
    public string? WithoutEntitlementFor { get; set; }

    /// <summary>
    /// Filter out plans that have a billing product ID
    /// </summary>
    public bool? WithoutProductId { get; set; }

    /// <summary>
    /// Filter out plans that have a paid billing product ID
    /// </summary>
    public bool? WithoutPaidProductId { get; set; }

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
