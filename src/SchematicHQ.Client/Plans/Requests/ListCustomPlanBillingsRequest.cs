using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record ListCustomPlanBillingsRequest
{
    /// <summary>
    /// Filter by company ID
    /// </summary>
    [JsonIgnore]
    public string? CompanyId { get; set; }

    /// <summary>
    /// Filter by plan ID
    /// </summary>
    [JsonIgnore]
    public string? PlanId { get; set; }

    /// <summary>
    /// Filter by billing status
    /// </summary>
    [JsonIgnore]
    public CustomPlanBillingStatus? Status { get; set; }

    /// <summary>
    /// Filter by multiple billing statuses
    /// </summary>
    [JsonIgnore]
    public IEnumerable<CustomPlanBillingStatus> Statuses { get; set; } =
        new List<CustomPlanBillingStatus>();

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
