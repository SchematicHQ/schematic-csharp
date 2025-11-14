using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdatePlanTraitRequestBody
{
    /// <summary>
    /// plan_trait_id
    /// </summary>
    [JsonIgnore]
    public required string PlanTraitId { get; set; }

    [JsonPropertyName("plan_id")]
    public required string PlanId { get; set; }

    [JsonPropertyName("trait_value")]
    public required string TraitValue { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
