using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record RulesengineCheckFlagResult : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    [JsonPropertyName("err")]
    public string? Err { get; set; }

    [JsonPropertyName("feature_allocation")]
    public int? FeatureAllocation { get; set; }

    [JsonPropertyName("feature_usage")]
    public int? FeatureUsage { get; set; }

    [JsonPropertyName("feature_usage_event")]
    public string? FeatureUsageEvent { get; set; }

    [JsonPropertyName("feature_usage_period")]
    public RulesengineCheckFlagResultFeatureUsagePeriod? FeatureUsagePeriod { get; set; }

    [JsonPropertyName("feature_usage_reset_at")]
    public DateTime? FeatureUsageResetAt { get; set; }

    [JsonPropertyName("flag_id")]
    public string? FlagId { get; set; }

    [JsonPropertyName("flag_key")]
    public required string FlagKey { get; set; }

    [JsonPropertyName("reason")]
    public required string Reason { get; set; }

    [JsonPropertyName("rule_id")]
    public string? RuleId { get; set; }

    [JsonPropertyName("rule_type")]
    public RulesengineCheckFlagResultRuleType? RuleType { get; set; }

    [JsonPropertyName("user_id")]
    public string? UserId { get; set; }

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
