using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class CountPlansParams
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
