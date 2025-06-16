using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record ListCompaniesRequest
{
    /// <summary>
    /// Filter companies by multiple company IDs (starts with comp_)
    /// </summary>
    [JsonIgnore]
    public IEnumerable<string> Ids { get; set; } = new List<string>();

    /// <summary>
    /// Filter companies by plan ID (starts with plan_)
    /// </summary>
    [JsonIgnore]
    public string? PlanId { get; set; }

    /// <summary>
    /// Search for companies by name, keys or string traits
    /// </summary>
    [JsonIgnore]
    public string? Q { get; set; }

    /// <summary>
    /// Filter out companies that already have a company override for the specified feature ID
    /// </summary>
    [JsonIgnore]
    public string? WithoutFeatureOverrideFor { get; set; }

    /// <summary>
    /// Filter out companies that have a plan
    /// </summary>
    [JsonIgnore]
    public bool? WithoutPlan { get; set; }

    /// <summary>
    /// Filter companies that have a subscription
    /// </summary>
    [JsonIgnore]
    public bool? WithSubscription { get; set; }

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
