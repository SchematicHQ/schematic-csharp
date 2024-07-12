using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record GetOrCreateCompanyMembershipRequestBody
{
    [JsonPropertyName("company_id")]
    public required string CompanyId { get; init; }

    [JsonPropertyName("user_id")]
    public required string UserId { get; init; }
}
