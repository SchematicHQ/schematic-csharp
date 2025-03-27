using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

/// <summary>
/// Input parameters
/// </summary>
public record ListPlanEntitlementsParams
{
    /// <summary>
    /// Filter plan entitlements by a single feature ID (starting with feat_)
    /// </summary>
    [JsonPropertyName("feature_id")]
    public string? FeatureId { get; set; }

    /// <summary>
    /// Filter plan entitlements by multiple feature IDs (starting with feat_)
    /// </summary>
    [JsonPropertyName("feature_ids")]
    public IEnumerable<string>? FeatureIds { get; set; }

    /// <summary>
    /// Filter plan entitlements by multiple plan entitlement IDs (starting with pltl_)
    /// </summary>
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

    /// <summary>
    /// Filter plan entitlements by a single plan ID (starting with plan_)
    /// </summary>
    [JsonPropertyName("plan_id")]
    public string? PlanId { get; set; }

    /// <summary>
    /// Filter plan entitlements by multiple plan IDs (starting with plan_)
    /// </summary>
    [JsonPropertyName("plan_ids")]
    public IEnumerable<string>? PlanIds { get; set; }

    /// <summary>
    /// Search for plan entitlements by feature or company name
    /// </summary>
    [JsonPropertyName("q")]
    public string? Q { get; set; }

    /// <summary>
    /// Filter plan entitlements only with metered products
    /// </summary>
    [JsonPropertyName("with_metered_products")]
    public bool? WithMeteredProducts { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
