using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record ListBillingPricesRequest
{
    /// <summary>
    /// Filter for prices in a specific currency (e.g. usd, eur)
    /// </summary>
    [JsonIgnore]
    public string? Currency { get; set; }

    /// <summary>
    /// Filter for prices valid for initial plans (free prices only)
    /// </summary>
    [JsonIgnore]
    public bool? ForInitialPlan { get; set; }

    /// <summary>
    /// Filter for prices valid for trial expiry plans (free prices only)
    /// </summary>
    [JsonIgnore]
    public bool? ForTrialExpiryPlan { get; set; }

    [JsonIgnore]
    public IEnumerable<string> Ids { get; set; } = new List<string>();

    [JsonIgnore]
    public string? Interval { get; set; }

    /// <summary>
    /// Filter for active prices on active products (defaults to true if not specified)
    /// </summary>
    [JsonIgnore]
    public bool? IsActive { get; set; }

    [JsonIgnore]
    public long? Price { get; set; }

    [JsonIgnore]
    public string? ProductId { get; set; }

    [JsonIgnore]
    public IEnumerable<string> ProductIds { get; set; } = new List<string>();

    [JsonIgnore]
    public BillingProviderType? ProviderType { get; set; }

    [JsonIgnore]
    public string? Q { get; set; }

    [JsonIgnore]
    public BillingTiersMode? TiersMode { get; set; }

    [JsonIgnore]
    public BillingPriceUsageType? UsageType { get; set; }

    /// <summary>
    /// Filter for prices with a meter
    /// </summary>
    [JsonIgnore]
    public bool? WithMeter { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonIgnore]
    public long? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonIgnore]
    public long? Offset { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
