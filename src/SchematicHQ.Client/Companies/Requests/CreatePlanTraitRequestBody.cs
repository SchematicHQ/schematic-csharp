using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreatePlanTraitRequestBody
{
    [JsonPropertyName("plan_id")]
    public required string PlanId { get; set; }

    [JsonPropertyName("trait_id")]
    public required string TraitId { get; set; }

    [JsonPropertyName("trait_value")]
    public required string TraitValue { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
