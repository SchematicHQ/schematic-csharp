using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record ListPlansRequest
{
    [JsonIgnore]
    public string? CompanyId { get; set; }

    /// <summary>
    /// Filter for plans valid as fallback plans (not linked to billing)
    /// </summary>
    [JsonIgnore]
    public bool? ForFallbackPlan { get; set; }

    /// <summary>
    /// Filter for plans valid as initial plans (not linked to billing, free, or auto-cancelling trial)
    /// </summary>
    [JsonIgnore]
    public bool? ForInitialPlan { get; set; }

    /// <summary>
    /// Filter for plans valid as trial expiry plans (not linked to billing or free)
    /// </summary>
    [JsonIgnore]
    public bool? ForTrialExpiryPlan { get; set; }

    /// <summary>
    /// Filter out plans that do not have a billing product ID
    /// </summary>
    [JsonIgnore]
    public bool? HasProductId { get; set; }

    [JsonIgnore]
    public IEnumerable<string> Ids { get; set; } = new List<string>();

    /// <summary>
    /// Filter by plan type
    /// </summary>
    [JsonIgnore]
    public PlanType? PlanType { get; set; }

    [JsonIgnore]
    public string? Q { get; set; }

    /// <summary>
    /// Filter out plans that already have a plan entitlement for the specified feature ID
    /// </summary>
    [JsonIgnore]
    public string? WithoutEntitlementFor { get; set; }

    /// <summary>
    /// Filter out plans that have a paid billing product ID
    /// </summary>
    [JsonIgnore]
    public bool? WithoutPaidProductId { get; set; }

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
