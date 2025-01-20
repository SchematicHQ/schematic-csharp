using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record GetOrCreateCompanyMembershipRequestBody
{
    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    [JsonPropertyName("user_id")]
    public required string UserId { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
