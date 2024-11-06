using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CountUsersParams
{
    /// <summary>
    /// Filter users by company ID (starts with comp\_)
    /// </summary>
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    /// <summary>
    /// Filter users by multiple user IDs (starts with user\_)
    /// </summary>
    [JsonPropertyName("ids")]
    public IEnumerable<string>? Ids { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonPropertyName("offset")]
    public int? Offset { get; set; }

    /// <summary>
    /// Filter users by plan ID (starts with plan\_)
    /// </summary>
    [JsonPropertyName("plan_id")]
    public string? PlanId { get; set; }

    /// <summary>
    /// Search for users by name, keys or string traits
    /// </summary>
    [JsonPropertyName("q")]
    public string? Q { get; set; }
}
