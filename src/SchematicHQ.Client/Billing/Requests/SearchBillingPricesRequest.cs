using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record SearchBillingPricesRequest
{
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
    public string? ProductId { get; set; }

    [JsonIgnore]
    public string? Interval { get; set; }

    [JsonIgnore]
    public int? Price { get; set; }

    [JsonIgnore]
    public string? Q { get; set; }

    /// <summary>
    /// Filter for prices that require a payment method (inverse of ForInitialPlan)
    /// </summary>
    [JsonIgnore]
    public bool? RequiresPaymentMethod { get; set; }

    [JsonIgnore]
    public SearchBillingPricesRequestTiersMode? TiersMode { get; set; }

    [JsonIgnore]
    public SearchBillingPricesRequestUsageType? UsageType { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonIgnore]
    public int? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonIgnore]
    public int? Offset { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
