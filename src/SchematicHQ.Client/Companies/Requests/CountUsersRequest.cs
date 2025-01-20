using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record CountUsersRequest
{
    /// <summary>
    /// Filter users by company ID (starts with comp_)
    /// </summary>
    public string? CompanyId { get; set; }

    /// <summary>
    /// Filter users by multiple user IDs (starts with user_)
    /// </summary>
    public IEnumerable<string> Ids { get; set; } = new List<string>();

    /// <summary>
    /// Filter users by plan ID (starts with plan_)
    /// </summary>
    public string? PlanId { get; set; }

    /// <summary>
    /// Search for users by name, keys or string traits
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
