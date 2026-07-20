using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CompanyFeatureUsageExportMetadata : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Restrict the export to these Schematic company IDs (starting with 'comp_')
    /// </summary>
    [JsonPropertyName("company_ids")]
    public IEnumerable<string>? CompanyIds { get; set; }

    /// <summary>
    /// Restrict the export to companies with these billing credit type IDs
    /// </summary>
    [JsonPropertyName("credit_type_ids")]
    public IEnumerable<string>? CreditTypeIds { get; set; }

    /// <summary>
    /// Schematic feature IDs (starting with 'feat_') to include as usage columns; at least one is required
    /// </summary>
    [JsonPropertyName("feature_ids")]
    public IEnumerable<string> FeatureIds { get; set; } = new List<string>();

    /// <summary>
    /// Restrict the export to companies that do (or do not) have a scheduled downgrade
    /// </summary>
    [JsonPropertyName("has_scheduled_downgrade")]
    public bool? HasScheduledDowngrade { get; set; }

    /// <summary>
    /// Restrict the export to companies with (or without) a monetized subscription
    /// </summary>
    [JsonPropertyName("monetized_subscriptions")]
    public bool? MonetizedSubscriptions { get; set; }

    /// <summary>
    /// Account member emails to notify when the export completes; empty means the artifact is only retrievable via the API
    /// </summary>
    [JsonPropertyName("notification_email_recipient_email_addresses")]
    public IEnumerable<string>? NotificationEmailRecipientEmailAddresses { get; set; }

    /// <summary>
    /// Restrict the export to companies on this plan ID (starting with 'plan_')
    /// </summary>
    [JsonPropertyName("plan_id")]
    public string? PlanId { get; set; }

    /// <summary>
    /// Restrict the export to companies on any of these plan IDs
    /// </summary>
    [JsonPropertyName("plan_ids")]
    public IEnumerable<string>? PlanIds { get; set; }

    /// <summary>
    /// Restrict the export to companies on this plan version ID
    /// </summary>
    [JsonPropertyName("plan_version_id")]
    public string? PlanVersionId { get; set; }

    /// <summary>
    /// Free-text search over company name and keys
    /// </summary>
    [JsonPropertyName("q")]
    public string? Q { get; set; }

    /// <summary>
    /// Restrict the export to companies whose subscription has one of these statuses
    /// </summary>
    [JsonPropertyName("subscription_statuses")]
    public IEnumerable<string>? SubscriptionStatuses { get; set; }

    /// <summary>
    /// Restrict the export to companies whose subscription has one of these types
    /// </summary>
    [JsonPropertyName("subscription_types")]
    public IEnumerable<string>? SubscriptionTypes { get; set; }

    /// <summary>
    /// Restrict the export to companies that have an entitlement for this feature ID
    /// </summary>
    [JsonPropertyName("with_entitlement_for")]
    public string? WithEntitlementFor { get; set; }

    /// <summary>
    /// Restrict the export to companies with a subscription
    /// </summary>
    [JsonPropertyName("with_subscription")]
    public bool? WithSubscription { get; set; }

    /// <summary>
    /// Restrict the export to companies without a company-level override for this feature ID
    /// </summary>
    [JsonPropertyName("without_feature_override_for")]
    public string? WithoutFeatureOverrideFor { get; set; }

    /// <summary>
    /// Restrict the export to companies without a plan
    /// </summary>
    [JsonPropertyName("without_plan")]
    public bool? WithoutPlan { get; set; }

    /// <summary>
    /// Restrict the export to companies without a subscription
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
