using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CountCompaniesForAdvancedFilterRequest
{
    /// <summary>
    /// Filter companies by multiple company IDs (starts with comp_)
    /// </summary>
    [JsonIgnore]
    public IEnumerable<string> Ids { get; set; } = new List<string>();

    /// <summary>
    /// Filter companies by one or more plan IDs (each ID starts with plan_)
    /// </summary>
    [JsonIgnore]
    public IEnumerable<string> PlanIds { get; set; } = new List<string>();

    /// <summary>
    /// Filter companies by one or more feature IDs (each ID starts with feat_)
    /// </summary>
    [JsonIgnore]
    public IEnumerable<string> FeatureIds { get; set; } = new List<string>();

    /// <summary>
    /// Filter companies by one or more subscription statuses (active, canceled, expired, incomplete, incomplete_expired, past_due, paused, trialing, unpaid)
    /// </summary>
    [JsonIgnore]
    public IEnumerable<string> SubscriptionStatuses { get; set; } = new List<string>();

    /// <summary>
    /// Filter companies by one or more subscription types (paid, free, trial)
    /// </summary>
    [JsonIgnore]
    public IEnumerable<string> SubscriptionTypes { get; set; } = new List<string>();

    /// <summary>
    /// Filter companies that have monetized subscriptions
    /// </summary>
    [JsonIgnore]
    public bool? MonetizedSubscriptions { get; set; }

    /// <summary>
    /// Search for companies by name, keys or string traits
    /// </summary>
    [JsonIgnore]
    public string? Q { get; set; }

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
    /// Column to sort by (e.g. name, created_at, last_seen_at)
    /// </summary>
    [JsonIgnore]
    public string? SortOrderColumn { get; set; }

    /// <summary>
    /// Direction to sort by (asc or desc)
    /// </summary>
    [JsonIgnore]
    public CountCompaniesForAdvancedFilterRequestSortOrderDirection? SortOrderDirection { get; set; }

    /// <summary>
    /// Select the display columns to return (e.g. plan, subscription, users, last_seen)
    /// </summary>
    [JsonIgnore]
    public IEnumerable<string> DisplayProperties { get; set; } = new List<string>();

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
