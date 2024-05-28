using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public class CompanyMembershipDetailResponseData
{
    [JsonPropertyName("company")]
    public CompanyResponseData? Company { get; init; }

    [JsonPropertyName("company_id")]
    public string CompanyId { get; init; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; init; }

    [JsonPropertyName("user_id")]
    public string UserId { get; init; }
}
