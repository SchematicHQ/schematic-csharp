using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CompanyMembershipResponseData
{
    [JsonPropertyName("company_id")]
    public required string CompanyId { get; init; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; init; }

    [JsonPropertyName("user_id")]
    public required string UserId { get; init; }
}
