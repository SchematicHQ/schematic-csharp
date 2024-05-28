using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class ListFeaturesParams
{
    [JsonPropertyName("ids")]
    public List<string>? Ids { get; init; }

    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    [JsonPropertyName("offset")]
    public int? Offset { get; init; }

    [JsonPropertyName("q")]
    public string? Q { get; init; }

    [JsonPropertyName("without_company_override_for")]
    public string? WithoutCompanyOverrideFor { get; init; }

    [JsonPropertyName("without_plan_entitlement_for")]
    public string? WithoutPlanEntitlementFor { get; init; }
}
