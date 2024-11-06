using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record GetOrCreateCompanyMembershipRequestBody
{
    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    [JsonPropertyName("user_id")]
    public required string UserId { get; set; }
}
