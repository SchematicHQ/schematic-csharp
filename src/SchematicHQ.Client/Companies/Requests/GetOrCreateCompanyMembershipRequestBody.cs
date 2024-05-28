using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class GetOrCreateCompanyMembershipRequestBody
{
    [JsonPropertyName("company_id")]
    public string CompanyId { get; init; }

    [JsonPropertyName("user_id")]
    public string UserId { get; init; }
}
