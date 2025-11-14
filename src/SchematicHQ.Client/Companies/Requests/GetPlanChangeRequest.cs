using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record GetPlanChangeRequest
{
    /// <summary>
    /// plan_change_id
    /// </summary>
    [JsonIgnore]
    public required string PlanChangeId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
