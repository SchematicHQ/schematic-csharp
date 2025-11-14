using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record DeleteBillingPlanCreditGrantRequest
{
    /// <summary>
    /// plan_grant_id
    /// </summary>
    [JsonIgnore]
    public required string PlanGrantId { get; set; }

    [JsonIgnore]
    public bool? ApplyToExisting { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
