using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CompanyMembershipResponseData
{
    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }

    [JsonPropertyName("user_id")]
    public required string UserId { get; set; }
}
