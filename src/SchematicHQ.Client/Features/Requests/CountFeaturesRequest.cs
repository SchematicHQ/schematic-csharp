using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record CountFeaturesRequest
{
    public IEnumerable<string> Ids { get; set; } = new List<string>();

    public string? Q { get; set; }

    /// <summary>
    /// Filter out features that already have a company override for the specified company ID
    /// </summary>
    public string? WithoutCompanyOverrideFor { get; set; }

    /// <summary>
    /// Filter out features that already have a plan entitlement for the specified plan ID
    /// </summary>
    public string? WithoutPlanEntitlementFor { get; set; }

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
