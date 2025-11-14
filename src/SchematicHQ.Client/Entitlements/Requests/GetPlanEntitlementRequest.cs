using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record GetPlanEntitlementRequest
{
    /// <summary>
    /// plan_entitlement_id
    /// </summary>
    [JsonIgnore]
    public required string PlanEntitlementId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
