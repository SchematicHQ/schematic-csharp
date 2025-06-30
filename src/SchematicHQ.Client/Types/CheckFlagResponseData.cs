using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

/// <summary>
/// The returned resource
/// </summary>
[Serializable]
public record CheckFlagResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// If company keys were provided and matched a company, its ID
    /// </summary>
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    /// <summary>
    /// If an error occurred while checking the flag, the error message
    /// </summary>
    [JsonPropertyName("error")]
    public string? Error { get; set; }

    /// <summary>
    /// If a numeric feature entitlement rule was matched, its allocation
    /// </summary>
    [JsonPropertyName("feature_allocation")]
    public int? FeatureAllocation { get; set; }

    /// <summary>
    /// If a numeric feature entitlement rule was matched, the company's usage
    /// </summary>
    [JsonPropertyName("feature_usage")]
    public int? FeatureUsage { get; set; }

    /// <summary>
    /// If an event-based numeric feature entitlement rule was matched, the event used to track its usage
    /// </summary>
    [JsonPropertyName("feature_usage_event")]
    public string? FeatureUsageEvent { get; set; }

    /// <summary>
    /// For event-based feature entitlement rules, the period over which usage is tracked (current_month, current_day, current_week, all_time)
    /// </summary>
    [JsonPropertyName("feature_usage_period")]
    public string? FeatureUsagePeriod { get; set; }

    /// <summary>
    /// For event-based feature entitlement rules, when the usage period will reset
    /// </summary>
    [JsonPropertyName("feature_usage_reset_at")]
    public DateTime? FeatureUsageResetAt { get; set; }

    /// <summary>
    /// The key used to check the flag
    /// </summary>
    [JsonPropertyName("flag")]
    public required string Flag { get; set; }

    /// <summary>
    /// If a flag was found, its ID
    /// </summary>
    [JsonPropertyName("flag_id")]
    public string? FlagId { get; set; }

    /// <summary>
    /// A human-readable explanation of the result
    /// </summary>
    [JsonPropertyName("reason")]
    public required string Reason { get; set; }

    /// <summary>
    /// If a rule was found, its ID
    /// </summary>
    [JsonPropertyName("rule_id")]
    public string? RuleId { get; set; }

    /// <summary>
    /// If a rule was found, its type
    /// </summary>
    [JsonPropertyName("rule_type")]
    public string? RuleType { get; set; }

    /// <summary>
    /// If user keys were provided and matched a user, its ID
    /// </summary>
    [JsonPropertyName("user_id")]
    public string? UserId { get; set; }

    /// <summary>
    /// A boolean flag check result; for feature entitlements, this represents whether further consumption of the feature is permitted
    /// </summary>
    [JsonPropertyName("value")]
    public required bool Value { get; set; }

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
