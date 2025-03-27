using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record CountPlansRequest
{
    [JsonIgnore]
    public string? CompanyId { get; set; }

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
    public CountPlansRequestPlanType? PlanType { get; set; }

    [JsonIgnore]
    public string? Q { get; set; }

    /// <summary>
    /// Filter out plans that already have a plan entitlement for the specified feature ID
    /// </summary>
    [JsonIgnore]
    public string? WithoutEntitlementFor { get; set; }

    /// <summary>
    /// Filter out plans that have a billing product ID
    /// </summary>
    [JsonIgnore]
    public bool? WithoutProductId { get; set; }

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
