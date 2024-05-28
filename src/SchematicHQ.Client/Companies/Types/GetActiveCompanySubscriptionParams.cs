using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class GetActiveCompanySubscriptionParams
{
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; init; }

    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    [JsonPropertyName("offset")]
    public int? Offset { get; init; }
}
