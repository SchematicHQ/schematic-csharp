using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CountUsersRequest
{
    /// <summary>
    /// Filter users by company ID (starts with comp_)
    /// </summary>
    [JsonIgnore]
    public string? CompanyId { get; set; }

    /// <summary>
    /// Filter users by multiple user IDs (starts with user_)
    /// </summary>
    [JsonIgnore]
    public IEnumerable<string> Ids { get; set; } = new List<string>();

    /// <summary>
    /// Filter users by plan ID (starts with plan_)
    /// </summary>
    [JsonIgnore]
    public string? PlanId { get; set; }

    /// <summary>
    /// Search for users by name, keys or string traits
    /// </summary>
    [JsonIgnore]
    public string? Q { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonIgnore]
    public int? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonIgnore]
    public int? Offset { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
