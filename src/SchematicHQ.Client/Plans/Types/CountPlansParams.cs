using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class CountPlansParams
{
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; init; }

    [JsonPropertyName("ids")]
    public IEnumerable<string>? Ids { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonPropertyName("offset")]
    public int? Offset { get; init; }

    [JsonPropertyName("q")]
    public string? Q { get; init; }

    /// <summary>
    /// Filter out plans that already have a plan entitlement for the specified feature ID
    /// </summary>
    [JsonPropertyName("without_entitlement_for")]
    public string? WithoutEntitlementFor { get; init; }
}
