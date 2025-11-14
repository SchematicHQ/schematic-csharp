using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record DeletePlanTraitRequest
{
    /// <summary>
    /// plan_trait_id
    /// </summary>
    [JsonIgnore]
    public required string PlanTraitId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
