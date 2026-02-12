using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CountPlanEntitlementsRequest
{
    /// <summary>
    /// Filter plan entitlements by a single feature ID (starting with feat_)
    /// </summary>
    [JsonIgnore]
    public string? FeatureId { get; set; }

    /// <summary>
    /// Filter plan entitlements by multiple feature IDs (starting with feat_)
    /// </summary>
    [JsonIgnore]
    public IEnumerable<string> FeatureIds { get; set; } = new List<string>();

    /// <summary>
    /// Filter plan entitlements by multiple plan entitlement IDs (starting with pltl_)
    /// </summary>
    [JsonIgnore]
    public IEnumerable<string> Ids { get; set; } = new List<string>();

    /// <summary>
    /// Filter plan entitlements by a single plan ID (starting with plan_)
    /// </summary>
    [JsonIgnore]
    public string? PlanId { get; set; }

    /// <summary>
    /// Filter plan entitlements by multiple plan IDs (starting with plan_)
    /// </summary>
    [JsonIgnore]
    public IEnumerable<string> PlanIds { get; set; } = new List<string>();

    /// <summary>
    /// Filter plan entitlements by a single plan version ID (starting with plvr_)
    /// </summary>
    [JsonIgnore]
    public string? PlanVersionId { get; set; }

    /// <summary>
    /// Filter plan entitlements by multiple plan version IDs (starting with plvr_)
    /// </summary>
    [JsonIgnore]
    public IEnumerable<string> PlanVersionIds { get; set; } = new List<string>();

    /// <summary>
    /// Search for plan entitlements by feature or company name
    /// </summary>
    [JsonIgnore]
    public string? Q { get; set; }

    /// <summary>
    /// Filter plan entitlements only with metered products
    /// </summary>
    [JsonIgnore]
    public bool? WithMeteredProducts { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonIgnore]
    public int? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonIgnore]
    public int? Offset { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
