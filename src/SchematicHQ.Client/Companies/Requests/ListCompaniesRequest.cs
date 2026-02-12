using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record ListCompaniesRequest
{
    /// <summary>
    /// Filter companies by one or more credit type IDs (each ID starts with bcrd_)
    /// </summary>
    [JsonIgnore]
    public IEnumerable<string> CreditTypeIds { get; set; } = new List<string>();

    /// <summary>
    /// Filter companies by multiple company IDs (starts with comp_)
    /// </summary>
    [JsonIgnore]
    public IEnumerable<string> Ids { get; set; } = new List<string>();

    /// <summary>
    /// Filter companies that have monetized subscriptions
    /// </summary>
    [JsonIgnore]
    public bool? MonetizedSubscriptions { get; set; }

    /// <summary>
    /// Filter companies by plan ID (starts with plan_)
    /// </summary>
    [JsonIgnore]
    public string? PlanId { get; set; }

    /// <summary>
    /// Filter companies by one or more plan IDs (each ID starts with plan_)
    /// </summary>
    [JsonIgnore]
    public IEnumerable<string> PlanIds { get; set; } = new List<string>();

    /// <summary>
    /// Search for companies by name, keys or string traits
    /// </summary>
    [JsonIgnore]
    public string? Q { get; set; }

    /// <summary>
    /// Column to sort by (e.g. name, created_at, last_seen_at)
    /// </summary>
    [JsonIgnore]
    public string? SortOrderColumn { get; set; }

    /// <summary>
    /// Direction to sort by (asc or desc)
    /// </summary>
    [JsonIgnore]
    public SortDirection? SortOrderDirection { get; set; }

    /// <summary>
    /// Filter companies by one or more subscription statuses
    /// </summary>
    [JsonIgnore]
    public IEnumerable<SubscriptionStatus> SubscriptionStatuses { get; set; } =
        new List<SubscriptionStatus>();

    /// <summary>
    /// Filter companies by one or more subscription types
    /// </summary>
    [JsonIgnore]
    public IEnumerable<SubscriptionType> SubscriptionTypes { get; set; } =
        new List<SubscriptionType>();

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
    /// Filter out companies that have a subscription
    /// </summary>
    [JsonIgnore]
    public bool? WithoutSubscription { get; set; }

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
