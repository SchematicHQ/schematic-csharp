using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

/// <summary>
/// Input parameters
/// </summary>
[Serializable]
public record SearchBillingPricesParams : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Filter for prices valid for initial plans (free prices only)
    /// </summary>
    [JsonPropertyName("for_initial_plan")]
    public bool? ForInitialPlan { get; set; }

    /// <summary>
    /// Filter for prices valid for trial expiry plans (free prices only)
    /// </summary>
    [JsonPropertyName("for_trial_expiry_plan")]
    public bool? ForTrialExpiryPlan { get; set; }

    [JsonPropertyName("ids")]
    public IEnumerable<string>? Ids { get; set; }

    [JsonPropertyName("interval")]
    public string? Interval { get; set; }

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

    [JsonPropertyName("price")]
    public int? Price { get; set; }

    [JsonPropertyName("product_id")]
    public string? ProductId { get; set; }

    [JsonPropertyName("q")]
    public string? Q { get; set; }

    /// <summary>
    /// Filter for prices that require a payment method (inverse of ForInitialPlan)
    /// </summary>
    [JsonPropertyName("requires_payment_method")]
    public bool? RequiresPaymentMethod { get; set; }

    [JsonPropertyName("tiers_mode")]
    public SearchBillingPricesResponseParamsTiersMode? TiersMode { get; set; }

    [JsonPropertyName("usage_type")]
    public SearchBillingPricesResponseParamsUsageType? UsageType { get; set; }

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
