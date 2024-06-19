using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class ListCompanyMembershipsParams
{
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; init; }

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

    [JsonPropertyName("user_id")]
    public string? UserId { get; init; }
}
