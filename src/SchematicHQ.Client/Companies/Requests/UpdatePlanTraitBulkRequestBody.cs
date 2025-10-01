using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdatePlanTraitBulkRequestBody
{
    [JsonPropertyName("plan_id")]
    public required string PlanId { get; set; }

    [JsonPropertyName("traits")]
    public IEnumerable<UpdatePlanTraitTraitRequestBody> Traits { get; set; } =
        new List<UpdatePlanTraitTraitRequestBody>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
