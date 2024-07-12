using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CountUsersParams
{
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; init; }

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

    [JsonPropertyName("plan_id")]
    public string? PlanId { get; init; }

    /// <summary>
    /// Search filter
    /// </summary>
    [JsonPropertyName("q")]
    public string? Q { get; init; }
}
