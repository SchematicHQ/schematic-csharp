using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

/// <summary>
/// Input parameters
/// </summary>
[Serializable]
public record CountPlansParams : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    /// <summary>
    /// Filter for plans valid as fallback plans (not linked to billing)
    /// </summary>
    [JsonPropertyName("for_fallback_plan")]
    public bool? ForFallbackPlan { get; set; }

    /// <summary>
    /// Filter for plans valid as initial plans (not linked to billing, free, or auto-cancelling trial)
    /// </summary>
    [JsonPropertyName("for_initial_plan")]
    public bool? ForInitialPlan { get; set; }

    /// <summary>
    /// Filter for plans valid as trial expiry plans (not linked to billing or free)
    /// </summary>
    [JsonPropertyName("for_trial_expiry_plan")]
    public bool? ForTrialExpiryPlan { get; set; }

    /// <summary>
    /// Filter out plans that do not have a billing product ID
    /// </summary>
    [JsonPropertyName("has_product_id")]
    public bool? HasProductId { get; set; }

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
    /// Filter by plan type
    /// </summary>
    [JsonPropertyName("plan_type")]
    public CountPlansResponseParamsPlanType? PlanType { get; set; }

    [JsonPropertyName("q")]
    public string? Q { get; set; }

    /// <summary>
    /// Filter out plans that already have a plan entitlement for the specified feature ID
    /// </summary>
    [JsonPropertyName("without_entitlement_for")]
    public string? WithoutEntitlementFor { get; set; }

    /// <summary>
    /// Filter out plans that have a paid billing product ID
    /// </summary>
    [JsonPropertyName("without_paid_product_id")]
    public bool? WithoutPaidProductId { get; set; }

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
