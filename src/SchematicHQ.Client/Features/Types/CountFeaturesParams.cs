using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CountFeaturesParams
{
    [JsonPropertyName("ids")]
    public IEnumerable<string>? Ids { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonPropertyName("offset")]
    public int? Offset { get; set; }

    [JsonPropertyName("q")]
    public string? Q { get; set; }

    /// <summary>
    /// Filter out features that already have a company override for the specified company ID
    /// </summary>
    [JsonPropertyName("without_company_override_for")]
    public string? WithoutCompanyOverrideFor { get; set; }

    /// <summary>
    /// Filter out features that already have a plan entitlement for the specified plan ID
    /// </summary>
    [JsonPropertyName("without_plan_entitlement_for")]
    public string? WithoutPlanEntitlementFor { get; set; }
}
