using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record CompanyMembershipDetailResponseData
{
    [JsonPropertyName("company")]
    public CompanyResponseData? Company { get; init; }

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
