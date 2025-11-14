using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

/// <summary>
/// Input parameters
/// </summary>
[Serializable]
public record ListFeaturesResponseParams : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Only return boolean features if there is an associated event. Automatically includes boolean in the feature types filter.
    /// </summary>
    [JsonPropertyName("boolean_require_event")]
    public bool? BooleanRequireEvent { get; set; }

    /// <summary>
    /// Filter by one or more feature types (boolean, event, trait)
    /// </summary>
    [JsonPropertyName("feature_type")]
    public IEnumerable<string>? FeatureType { get; set; }

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
    /// Search by feature name or ID
    /// </summary>
    [JsonPropertyName("q")]
    public string? Q { get; set; }

    /// <summary>
    /// Filter out features that already have a company override for the specified company ID
    /// </summary>
    [JsonPropertyName("without_company_override_for")]
    public string? WithoutCompanyOverrideFor { get; set; }

    /// <summary>
    /// Filter out features that already have a plan entitlement for the specified plan ID
    /// </summary>
    [JsonPropertyName("without_plan_entitlement_for")]
    public string? WithoutPlanEntitlementFor { get; set; }

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
