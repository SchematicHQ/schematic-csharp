using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

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
    /// If a feature entitlement rule was matched, its entitlement details
    /// </summary>
    [JsonPropertyName("entitlement")]
    public FeatureEntitlement? Entitlement { get; set; }

    /// <summary>
    /// If an error occurred while checking the flag, the error message
    /// </summary>
    [JsonPropertyName("error")]
    public string? Error { get; set; }

    /// <summary>
    /// Deprecated: Use Entitlement.Allocation instead.
    /// </summary>
    [JsonPropertyName("feature_allocation")]
    public long? FeatureAllocation { get; set; }

    /// <summary>
    /// Deprecated: Use Entitlement.Usage instead.
    /// </summary>
    [JsonPropertyName("feature_usage")]
    public long? FeatureUsage { get; set; }

    /// <summary>
    /// Deprecated: Use Entitlement.EventName instead.
    /// </summary>
    [JsonPropertyName("feature_usage_event")]
    public string? FeatureUsageEvent { get; set; }

    /// <summary>
    /// Deprecated: Use Entitlement.MetricPeriod instead.
    /// </summary>
    [JsonPropertyName("feature_usage_period")]
    public string? FeatureUsagePeriod { get; set; }

    /// <summary>
    /// Deprecated: Use Entitlement.MetricResetAt instead.
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
