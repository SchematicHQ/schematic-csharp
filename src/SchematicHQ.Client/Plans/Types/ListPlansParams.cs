using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record ListPlansParams
{
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; init; }

    /// <summary>
    /// Filter out plans that do not have a billing product ID
    /// </summary>
    [JsonPropertyName("has_product_id")]
    public bool? HasProductId { get; init; }

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

    /// <summary>
    /// Filter by plan type
    /// </summary>
    [JsonPropertyName("plan_type")]
    public ListPlansResponseParamsPlanType? PlanType { get; init; }

    [JsonPropertyName("q")]
    public string? Q { get; init; }

    /// <summary>
    /// Filter out plans that already have a plan entitlement for the specified feature ID
    /// </summary>
    [JsonPropertyName("without_entitlement_for")]
    public string? WithoutEntitlementFor { get; init; }

    /// <summary>
    /// Filter out plans that have a billing product ID
    /// </summary>
    [JsonPropertyName("without_product_id")]
    public bool? WithoutProductId { get; init; }
}
