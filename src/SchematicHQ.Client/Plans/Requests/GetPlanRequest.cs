using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record GetPlanRequest
{
    /// <summary>
    /// plan_id
    /// </summary>
    [JsonIgnore]
    public required string PlanId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
