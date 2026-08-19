using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record EntityTraitDefinitionUsage : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("checkout_field_config_count")]
    public required long CheckoutFieldConfigCount { get; set; }

    [JsonPropertyName("company_override_count")]
    public required long CompanyOverrideCount { get; set; }

    [JsonPropertyName("feature_count")]
    public required long FeatureCount { get; set; }

    [JsonPropertyName("plan_entitlement_count")]
    public required long PlanEntitlementCount { get; set; }

    [JsonPropertyName("plan_trait_count")]
    public required long PlanTraitCount { get; set; }

    [JsonPropertyName("rule_condition_count")]
    public required long RuleConditionCount { get; set; }

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
