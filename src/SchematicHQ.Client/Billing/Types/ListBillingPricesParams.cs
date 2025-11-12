using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

/// <summary>
/// Input parameters
/// </summary>
[Serializable]
public record ListBillingPricesParams : IJsonOnDeserialized
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
    /// Filter for active prices on active products (defaults to true if not specified)
    /// </summary>
    [JsonPropertyName("is_active")]
    public bool? IsActive { get; set; }

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

    [JsonPropertyName("product_ids")]
    public IEnumerable<string>? ProductIds { get; set; }

    [JsonPropertyName("q")]
    public string? Q { get; set; }

    [JsonPropertyName("tiers_mode")]
    public ListBillingPricesResponseParamsTiersMode? TiersMode { get; set; }

    [JsonPropertyName("usage_type")]
    public ListBillingPricesResponseParamsUsageType? UsageType { get; set; }

    /// <summary>
    /// Filter for prices with a meter
    /// </summary>
    [JsonPropertyName("with_meter")]
    public bool? WithMeter { get; set; }

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
