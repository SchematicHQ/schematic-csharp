using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record CountPlanEntitlementsRequest
{
    /// <summary>
    /// Filter plan entitlements by a single feature ID (starting with feat\_)
    /// </summary>
    public string? FeatureId { get; set; }

    /// <summary>
    /// Filter plan entitlements by multiple feature IDs (starting with feat\_)
    /// </summary>
    public IEnumerable<string> FeatureIds { get; set; } = new List<string>();

    /// <summary>
    /// Filter plan entitlements by multiple plan entitlement IDs (starting with pltl\_)
    /// </summary>
    public IEnumerable<string> Ids { get; set; } = new List<string>();

    /// <summary>
    /// Filter plan entitlements by a single plan ID (starting with plan\_)
    /// </summary>
    public string? PlanId { get; set; }

    /// <summary>
    /// Filter plan entitlements by multiple plan IDs (starting with plan\_)
    /// </summary>
    public IEnumerable<string> PlanIds { get; set; } = new List<string>();

    /// <summary>
    /// Search for plan entitlements by feature or company name
    /// </summary>
    public string? Q { get; set; }

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
