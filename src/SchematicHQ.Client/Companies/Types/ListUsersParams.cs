using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListUsersParams
{
    /// <summary>
    /// Filter users by company ID (starts with comp_)
    /// </summary>
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; init; }

    /// <summary>
    /// Filter users by multiple user IDs (starts with user_)
    /// </summary>
    [JsonPropertyName("ids")]
    public IEnumerable<string>? Ids { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonPropertyName("offset")]
    public int? Offset { get; init; }

    /// <summary>
    /// Filter users by plan ID (starts with plan_)
    /// </summary>
    [JsonPropertyName("plan_id")]
    public string? PlanId { get; init; }

    /// <summary>
    /// Search for users by name, keys or string traits
    /// </summary>
    [JsonPropertyName("q")]
    public string? Q { get; init; }
}
