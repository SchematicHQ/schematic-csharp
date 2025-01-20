using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CountPlanEntitlementsParams
{
    /// <summary>
    /// Filter plan entitlements by a single feature ID (starting with feat_)
    /// </summary>
    [JsonPropertyName("feature_id")]
    public string? FeatureId { get; init; }

    /// <summary>
    /// Filter plan entitlements by multiple feature IDs (starting with feat_)
    /// </summary>
    [JsonPropertyName("feature_ids")]
    public IEnumerable<string>? FeatureIds { get; init; }

    /// <summary>
    /// Filter plan entitlements by multiple plan entitlement IDs (starting with pltl_)
    /// </summary>
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
    /// Filter plan entitlements by a single plan ID (starting with plan_)
    /// </summary>
    [JsonPropertyName("plan_id")]
    public string? PlanId { get; init; }

    /// <summary>
    /// Filter plan entitlements by multiple plan IDs (starting with plan_)
    /// </summary>
    [JsonPropertyName("plan_ids")]
    public IEnumerable<string>? PlanIds { get; init; }

    /// <summary>
    /// Search for plan entitlements by feature or company name
    /// </summary>
    [JsonPropertyName("q")]
    public string? Q { get; init; }

    /// <summary>
    /// Filter plan entitlements only with metered products
    /// </summary>
    [JsonPropertyName("with_metered_products")]
    public bool? WithMeteredProducts { get; init; }
}
