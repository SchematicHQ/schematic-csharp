using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PlanVersionDiffResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Set when the version's billing product changed; null when it is the same on both versions.
    /// </summary>
    [JsonPropertyName("billing_product")]
    public PlanVersionBillingProductChangeResponseData? BillingProduct { get; set; }

    /// <summary>
    /// Credit grants added, removed, or changed, keyed by credit.
    /// </summary>
    [JsonPropertyName("credit_grants")]
    public IEnumerable<PlanVersionCreditGrantChangeResponseData> CreditGrants { get; set; } =
        new List<PlanVersionCreditGrantChangeResponseData>();

    /// <summary>
    /// Entitlements added, removed, or changed, keyed by feature.
    /// </summary>
    [JsonPropertyName("entitlements")]
    public IEnumerable<PlanVersionEntitlementChangeResponseData> Entitlements { get; set; } =
        new List<PlanVersionEntitlementChangeResponseData>();

    /// <summary>
    /// The version compared against, or null when the plan has no published version.
    /// </summary>
    [JsonPropertyName("from")]
    public PlanVersionResponseData? From { get; set; }

    /// <summary>
    /// Whether moving a subscription from the compared version to this one changes its billing.
    /// </summary>
    [JsonPropertyName("has_billing_changes")]
    public required bool HasBillingChanges { get; set; }

    /// <summary>
    /// Plan-level fields that changed: name, description, icon, charge_type, billing_strategy, is_trialable, and trial_days.
    /// </summary>
    [JsonPropertyName("plan_fields")]
    public IEnumerable<PlanVersionFieldChangeResponseData> PlanFields { get; set; } =
        new List<PlanVersionFieldChangeResponseData>();

    /// <summary>
    /// Base prices that changed, one entry per currency and cadence.
    /// </summary>
    [JsonPropertyName("prices")]
    public IEnumerable<PlanVersionPriceChangeResponseData> Prices { get; set; } =
        new List<PlanVersionPriceChangeResponseData>();

    /// <summary>
    /// The version being compared.
    /// </summary>
    [JsonPropertyName("to")]
    public PlanVersionResponseData? To { get; set; }

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
