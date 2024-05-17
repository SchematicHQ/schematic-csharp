using System.Text.Json.Serialization;

namespace SchematicHQ.Client;

public class ListPlansParams
{
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; init; }

    [JsonPropertyName("ids")]
    public List<string>? Ids { get; init; }

    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    [JsonPropertyName("offset")]
    public int? Offset { get; init; }

    [JsonPropertyName("q")]
    public string? Q { get; init; }

    [JsonPropertyName("without_entitlement_for")]
    public string? WithoutEntitlementFor { get; init; }
}
