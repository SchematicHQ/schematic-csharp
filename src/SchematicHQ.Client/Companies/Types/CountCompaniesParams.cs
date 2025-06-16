using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

/// <summary>
/// Input parameters
/// </summary>
public record CountCompaniesParams
{
    /// <summary>
    /// Filter companies by multiple company IDs (starts with comp_)
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
    /// Filter companies by plan ID (starts with plan_)
    /// </summary>
    [JsonPropertyName("plan_id")]
    public string? PlanId { get; set; }

    /// <summary>
    /// Search for companies by name, keys or string traits
    /// </summary>
    [JsonPropertyName("q")]
    public string? Q { get; set; }

    /// <summary>
    /// Filter companies that have a subscription
    /// </summary>
    [JsonPropertyName("with_subscription")]
    public bool? WithSubscription { get; set; }

    /// <summary>
    /// Filter out companies that already have a company override for the specified feature ID
    /// </summary>
    [JsonPropertyName("without_feature_override_for")]
    public string? WithoutFeatureOverrideFor { get; set; }

    /// <summary>
    /// Filter out companies that have a plan
    /// </summary>
    [JsonPropertyName("without_plan")]
    public bool? WithoutPlan { get; set; }

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
