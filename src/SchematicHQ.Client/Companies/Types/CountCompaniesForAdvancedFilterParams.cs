using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

/// <summary>
/// Input parameters
/// </summary>
[Serializable]
public record CountCompaniesForAdvancedFilterParams : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Select the display columns to return (e.g. plan, subscription, users, last_seen)
    /// </summary>
    [JsonPropertyName("display_properties")]
    public IEnumerable<string>? DisplayProperties { get; set; }

    /// <summary>
    /// Filter companies by one or more feature IDs (each ID starts with feat_)
    /// </summary>
    [JsonPropertyName("feature_ids")]
    public IEnumerable<string>? FeatureIds { get; set; }

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
    /// Filter companies that have monetized subscriptions
    /// </summary>
    [JsonPropertyName("monetized_subscriptions")]
    public bool? MonetizedSubscriptions { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonPropertyName("offset")]
    public int? Offset { get; set; }

    /// <summary>
    /// Filter companies by one or more plan IDs (each ID starts with plan_)
    /// </summary>
    [JsonPropertyName("plan_ids")]
    public IEnumerable<string>? PlanIds { get; set; }

    /// <summary>
    /// Search for companies by name, keys or string traits
    /// </summary>
    [JsonPropertyName("q")]
    public string? Q { get; set; }

    /// <summary>
    /// Column to sort by (e.g. name, created_at, last_seen_at)
    /// </summary>
    [JsonPropertyName("sort_order_column")]
    public string? SortOrderColumn { get; set; }

    /// <summary>
    /// Direction to sort by (asc or desc)
    /// </summary>
    [JsonPropertyName("sort_order_direction")]
    public CountCompaniesForAdvancedFilterResponseParamsSortOrderDirection? SortOrderDirection { get; set; }

    /// <summary>
    /// Filter companies by one or more subscription statuses (active, canceled, expired, incomplete, incomplete_expired, past_due, paused, trialing, unpaid)
    /// </summary>
    [JsonPropertyName("subscription_statuses")]
    public IEnumerable<string>? SubscriptionStatuses { get; set; }

    /// <summary>
    /// Filter companies by one or more subscription types (paid, free, trial)
    /// </summary>
    [JsonPropertyName("subscription_types")]
    public IEnumerable<string>? SubscriptionTypes { get; set; }

    /// <summary>
    /// Filter out companies that have a plan
    /// </summary>
    [JsonPropertyName("without_plan")]
    public bool? WithoutPlan { get; set; }

    /// <summary>
    /// Filter out companies that have a subscription
    /// </summary>
    [JsonPropertyName("without_subscription")]
    public bool? WithoutSubscription { get; set; }

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
